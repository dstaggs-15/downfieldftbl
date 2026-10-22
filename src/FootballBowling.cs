using System;
using System.Collections.Generic;
namespace Downfield {
public class BowlingFrame {public List<int> Rolls=new List<int>();}
public class BowlingCard {
 public List<BowlingFrame> Frames=new List<BowlingFrame>();public int Frame;public bool Complete;
 public BowlingCard(){for(int n=0;n<10;n++)Frames.Add(new BowlingFrame());}
 public int PinsLeft {get{if(Complete)return 0;List<int> r=Frames[Frame].Rolls;if(r.Count==0)return 10;if(Frame<9)return 10-r[0];if(r[0]==10)return r.Count==1||r[1]==10?10:10-r[1];return r.Count==1?10-r[0]:10;}}
 public void Roll(int pins){if(Complete)throw new InvalidOperationException("This scorecard is complete.");if(pins<0||pins>PinsLeft)throw new ArgumentOutOfRangeException("pins");List<int> r=Frames[Frame].Rolls;r.Add(pins);if(Frame<9){if(r[0]==10||r.Count==2)Frame++;}else if(r.Count==3||r.Count==2&&r[0]+r[1]<10)Complete=true;}
 public int?[] Cumulative(){List<int> rolls=new List<int>();foreach(BowlingFrame f in Frames)rolls.AddRange(f.Rolls);int?[] scores=new int?[10];int total=0,index=0;for(int f=0;f<10;f++){List<int> frame=Frames[f].Rolls;if(frame.Count==0)break;if(f==9){if(!Complete)break;foreach(int n in frame)total+=n;scores[f]=total;break;}if(frame[0]==10){if(index+2>=rolls.Count)break;total+=10+rolls[index+1]+rolls[index+2];index++;}else{if(frame.Count<2)break;int pins=frame[0]+frame[1];if(pins==10){if(index+2>=rolls.Count)break;total+=10+rolls[index+2];}else total+=pins;index+=2;}scores[f]=total;}return scores;}
 public int Score {get{int value=0;foreach(int? n in Cumulative())if(n.HasValue)value=n.Value;return value;}}
 public string Marks(int frame){List<int> r=Frames[frame].Rolls;string mark="";for(int n=0;n<r.Count;n++){bool spare=n>0&&r[n-1]!=10&&r[n-1]+r[n]==10&&(n==1||r[0]==10);string text=spare?"/":r[n]==10?"X":r[n]==0?"-":r[n].ToString();mark+=(n==0?"":" ")+text;}return mark;}
}
public class FootballBowling {
 public BowlingCard[] Cards={new BowlingCard(),new BowlingCard()};public int[] Teams=new int[2];public int Turn,RollCount;public bool TwoPlayers;public string LastResult="GET READY";
 public bool Complete {get{return Cards[0].Complete&&Cards[1].Complete;}}
 public BowlingCard Current {get{return Cards[Turn];}}
 public void Record(int pins){int side=Turn,frame=Current.Frame,roll=Current.Frames[frame].Rolls.Count;int left=Current.PinsLeft;Current.Roll(pins);RollCount++;LastResult=pins==left?(roll==0||frame==9&&left==10?"STRIKE!":"SPARE!"):pins==0?"GUTTER / NO YARDS":pins+" PINS";if(Current.Complete||Current.Frame!=frame){if(!Cards[1-side].Complete)Turn=1-side;}}
}
public partial class Simulation {
 public bool Bowling,BowlingRollFinished;public int BowlingPins;
 public void StartBowlingRoll(int pins){Bowling=true;BowlingRollFinished=false;BowlingPins=0;Spot=100-Math.Max(1,Math.Min(10,pins));FirstDown=100;Down=1;Clock=0;Timeouts=0;Setup();}
 void FinishBowlingRoll(float finish,bool touchdown,bool incomplete){if(BowlingRollFinished)return;BowlingRollFinished=true;BowlingPins=touchdown?(int)Math.Ceiling(100-Spot):incomplete?0:Math.Max(0,Math.Min((int)Math.Ceiling(100-Spot),(int)Math.Floor(finish-Spot+.0001f)));Charging=false;handoffTime=-1;State=Phase.Dead;Message=touchdown?"TOUCHDOWN / PINS DOWN!":BowlingPins+" YARDS / "+BowlingPins+" PINS";Log(Message);Cue(touchdown?"score":"whistle");if(touchdown){Controlled.Pose="celebrate";Controlled.PoseTime=.7f;}}
}
}
