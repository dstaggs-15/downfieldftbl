using System;
namespace Downfield {
public partial class Simulation {
 public const float CatchReadyDelay=.18f,CatchSweepDuration=1.05f;
 public bool CatchMeterActive,CatchMeterLocked;public float CatchMeterValue;public string CatchGrade="";float catchMeterTime,catchResultTime;Player catchDisplayReceiver;
 public const int CatchMeterLimit=5;public int CatchMeterUses;public int CatchMeterRemaining {get{return Math.Max(0,CatchMeterLimit-CatchMeterUses);}}
 public int CatchMeterSequence;public bool CatchMeterReady {get{return catchMeterTime>=CatchReadyDelay;}}
 public Player CatchDisplayReceiver {get{return CatchMeterActive&&(State==Phase.Flight||catchResultTime>0)?catchDisplayReceiver:null;}}
 public Player CatchReceiver {get{return State==Phase.Flight&&!lateral&&!throwingAway?target:null;}}
 public static float CatchChance(float value){float center=Math.Abs(value-.5f);return center<=.045f?1:center<=.16f?.75f:.04f;}
 public void AggressiveCatch(){if(CatchReceiver==null||!CatchReceiver.Active||CatchMeterLocked||CatchMeterActive)return;if(CatchMeterRemaining==0){Message="NO CATCH METER USES LEFT / NORMAL CATCHING ACTIVE";return;}if(!CatchMeterActive){CatchMeterUses++;CatchMeterActive=true;catchDisplayReceiver=CatchReceiver;catchMeterTime=0;CatchMeterValue=0;CatchGrade="";}}
 public void ReleaseCatch(){if(CatchMeterActive&&!CatchMeterLocked&&State==Phase.Flight)LockCatchMeter();}
 void LockCatchMeter(){if(CatchMeterLocked)return;CatchMeterLocked=true;catchResultTime=.9f;float chance=CatchChance(CatchMeterValue);CatchGrade=chance==1?"GREEN":chance==.75f?"YELLOW":"RED";if(catchDisplayReceiver!=null){catchDisplayReceiver.Pose="highcatch";catchDisplayReceiver.PoseTime=.7f;}}
 // The meter uses real time. The optional catch window slows the whole play together,
 // then holds just before arrival so even a late activation has a readable sweep.
 float CatchSimulationDelta(float realDelta){if(CatchMeterActive&&CatchMeterLocked&&State!=Phase.Flight)catchResultTime=Math.Max(0,catchResultTime-realDelta);if(State!=Phase.Flight||!CatchMeterActive||CatchMeterLocked)return realDelta;catchMeterTime+=realDelta;CatchMeterValue=Clamp((catchMeterTime-CatchReadyDelay)/CatchSweepDuration,0,1);if(CatchMeterValue>=1){LockCatchMeter();return realDelta;}return Math.Min(realDelta*.2f,Math.Max(0,passDuration*.84f-passTime));}
 void ResetCatchMeter(){CatchMeterActive=CatchMeterLocked=false;CatchMeterValue=0;CatchGrade="";catchMeterTime=catchResultTime=0;catchDisplayReceiver=null;CatchMeterSequence++;}
 bool MeterCatchReady(float t){return CatchMeterActive&&CatchMeterLocked&&target!=null&&target.Active&&t>=.88f;}
 bool ResolveMeterCatch(){Player c=target;float chance=CatchChance(CatchMeterValue);bool caught=chance>=1||random.NextDouble()<chance;c.Pose=caught?"highcatch":"drop";c.PoseTime=.65f;if(!caught){Drops++;EndPlay(Spot,CatchGrade+" / AGGRESSIVE CATCH MISSED",true);return false;}Controlled=c;c.HasBall=true;State=Phase.Live;catchGrace=SuperExtreme?.035f:.12f;Completions++;RecordCatch(c);Message=CatchGrade+" / AGGRESSIVE CATCH!";Cue("crowd_big");if(c.Z>=100)ScoreTouchdown();return true;}
 public bool CallTimeout(){if(Bowling||Practice||Timeouts<=0||State!=Phase.PreSnap&&State!=Phase.Dead||AmbulanceActive){Message=Timeouts<=0?"NO TIMEOUTS LEFT":"TIMEOUT AVAILABLE BETWEEN PLAYS";return false;}Timeouts--;Message="TIMEOUT / "+Timeouts+" LEFT THIS HALF";Cue("confirm");Log(Message);return true;}
}
}

