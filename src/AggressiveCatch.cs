using System;
namespace Downfield {
public partial class Simulation {
 public bool CatchMeterActive,CatchMeterLocked;public float CatchMeterValue;public string CatchGrade="";float catchMeterTime;
 public Player CatchReceiver {get{return State==Phase.Flight&&!lateral&&!throwingAway?target:null;}}
 public static float CatchChance(float value){float center=Math.Abs(value-.5f);return center<=.065f?1:center<=.22f?.75f:.04f;}
 public void AggressiveCatch(){if(CatchReceiver==null||!CatchReceiver.Active||CatchMeterLocked)return;if(!CatchMeterActive){CatchMeterActive=true;catchMeterTime=0;CatchMeterValue=0;CatchGrade="";}else{CatchMeterLocked=true;float chance=CatchChance(CatchMeterValue);CatchGrade=chance==1?"GREEN":chance==.75f?"YELLOW":"RED";CatchReceiver.Pose="highcatch";CatchReceiver.PoseTime=.7f;}}
 void TickCatchMeter(float dt){if(State!=Phase.Flight||!CatchMeterActive||CatchMeterLocked)return;catchMeterTime+=dt;CatchMeterValue=(float)((Math.Sin(catchMeterTime*8-Math.PI/2)+1)*.5);}
 void ResetCatchMeter(){CatchMeterActive=CatchMeterLocked=false;CatchMeterValue=0;CatchGrade="";catchMeterTime=0;}
 bool MeterCatchReady(float t){return CatchMeterActive&&CatchMeterLocked&&target!=null&&target.Active&&t>=.88f;}
 bool ResolveMeterCatch(){Player c=target;float chance=CatchChance(CatchMeterValue);bool caught=chance>=1||random.NextDouble()<chance;c.Pose=caught?"highcatch":"drop";c.PoseTime=.65f;if(!caught){Drops++;EndPlay(Spot,CatchGrade+" / AGGRESSIVE CATCH MISSED",true);return false;}Controlled=c;c.HasBall=true;State=Phase.Live;catchGrace=SuperExtreme?.035f:.12f;Completions++;RecordCatch(c);Message=CatchGrade+" / AGGRESSIVE CATCH!";Cue("crowd_big");if(c.Z>=100)ScoreTouchdown();return true;}
 public bool CallTimeout(){if(Bowling||Practice||Timeouts<=0||State!=Phase.PreSnap&&State!=Phase.Dead||AmbulanceActive){Message=Timeouts<=0?"NO TIMEOUTS LEFT":"TIMEOUT AVAILABLE BETWEEN PLAYS";return false;}Timeouts--;Message="TIMEOUT / "+Timeouts+" LEFT THIS HALF";Cue("confirm");Log(Message);return true;}
}
}
