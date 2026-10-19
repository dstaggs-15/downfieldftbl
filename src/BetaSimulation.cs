using System;
using System.Collections.Generic;
namespace Downfield {
public class BoxLine {public int PlayerId,PassYards,RushYards,RecYards,PassTD,RushTD,RecTD,Catches,Attempts,Completions,Picks;}
public partial class Simulation {
 public float PlacementX,PlacementZ;public bool Championship,Exhibition;public string HomeName="",HomeCity="",BowlName="";
 public bool SecondHalfOpponent=true;public int HalftimeCount;public bool OptionGiven;
 public readonly Queue<string> Sounds=new Queue<string>();public readonly List<BoxLine> Box=new List<BoxLine>();Player caughtReceiver;
 public bool OptionLive {get{return Playbook==22&&RunPlay&&!OptionGiven;}}
 public bool DesignedQBRun {get{return RunPlay&&Playbook==23;}}
 public int HandoffReceiver {get{return Playbook==21?10:1;}}
 public static bool IsRunCall(int play){return play>=6&&play<=13||play>=20;}
 public void Cue(string sound){if(Sounds.Count<12)Sounds.Enqueue(sound);}
 BoxLine Line(Player p){if(p==null||p.ClubId<0)return null;BoxLine b=Box.Find(x=>x.PlayerId==p.ClubId);if(b==null){b=new BoxLine();b.PlayerId=p.ClubId;Box.Add(b);}return b;}
 void RecordAttempt(){if(conversion||Practice)return;BoxLine b=Line(Players[0]);if(b!=null)b.Attempts++;}
 void RecordCatch(Player receiver){if(conversion||Practice){Cue("catch");return;}caughtReceiver=receiver;BoxLine qb=Line(Players[0]),r=Line(receiver);if(qb!=null)qb.Completions++;if(r!=null)r.Catches++;Cue("catch");}
 void RecordProduction(float finish,bool touchdown){if(conversion||Practice)return;int gain=(int)Math.Round(finish-Spot);if(caughtReceiver!=null){BoxLine q=Line(Players[0]),r=Line(caughtReceiver);if(q!=null){q.PassYards+=gain;if(touchdown)q.PassTD++;}if(r!=null){r.RecYards+=gain;if(touchdown)r.RecTD++;}}else if(Controlled!=null&&(Controlled.Role!="QB"||finish>Spot)){BoxLine r=Line(Controlled);if(r!=null){r.RushYards+=gain;if(touchdown)r.RushTD++;}}caughtReceiver=null;}
 public void GiveOption(){if(OptionLive&&State==Phase.Live&&Controlled.Role=="QB"&&Controlled.Z<=Spot){OptionGiven=true;Handoff();}}
 void StartHalftime(){OnsideAttempt=OnsideRulesPending=false;HalftimeCount++;Quarter=3;Clock=60;Timeouts=3;conversion=false;Charging=false;handoffTime=-1;KickAnimating=false;Spot=20;Down=1;FirstDown=30;State=Phase.Halftime;Message=SecondHalfOpponent?"HALFTIME / OPPONENT RECEIVES":"HALFTIME / YOU RECEIVE";Log(Message);Cue("halftime");}
 public void BeginSecondHalf(){if(State!=Phase.Halftime)return;Spot=20;Down=1;FirstDown=30;if(SecondHalfOpponent){opponentStart=20;State=Phase.Opponent;deadTime=2.8f;Message="SECOND HALF / OPPONENT AT 20";}else Setup();}
 public static float SkillSpeed(string role,int rating){float baseSpeed=role=="OL"?4.1f:role=="DL"?4.25f:role=="QB"?5.2f:role=="TE"?5.6f:role=="LB"?5.8f:6.1f;return baseSpeed+(Math.Max(70,Math.Min(99,rating))-70)*.115f;}
 float[] ExtraRoute(Player p,float t){float side=p.StartX<0?-1:1;float z=p.StartZ;
  if(Playbook==14)return new float[]{t<.7f?p.StartX:-side*18,z+Math.Min(7,t*p.Speed)};
  if(Playbook==15)return new float[]{p.Role=="TE"?side*23:p.StartX,z+(t<1.5f?Math.Min(12,t*p.Speed):9)};
  if(Playbook==16&&p.Role=="RB")return new float[]{20,Spot+Math.Max(1,(t-1.1f)*p.Speed)};
  if(Playbook==17)return new float[]{t<1.4f?p.StartX:side*25,z+Math.Min(45,t*p.Speed)};
  if(Playbook==18)return new float[]{t<1.8f?p.StartX:-side*9,z+Math.Min(45,Math.Max(0,t-.4f)*p.Speed)};
  if(Playbook==19&&p.Role=="RB")return new float[]{t<.9f?12:-6,Spot+Math.Min(15,t*p.Speed)};
  return null;
 }
}
}
