using System;
using System.Collections.Generic;
namespace Downfield {
public static class SimulationTests {
 static int checks,failures;static void Check(bool ok,string label){checks++;Console.WriteLine((ok?"PASS ":"FAIL ")+label);if(!ok)failures++;}
 static Simulation New(int seed,bool practice){Simulation s=new Simulation(seed);s.Practice=practice;s.NewGame();return s;}
 static void Advance(Simulation s,int frames,float x=0,float z=0){for(int i=0;i<frames;i++)s.Update(.05f,x,z);}
 static void Resolve(Simulation s){for(int i=0;i<100&&s.State==Phase.Flight;i++)s.Update(.05f,0,0);}
 public static int Run(){checks=failures=0;Simulation s=New(1,false);Check(s.Down==1&&s.Spot==20&&s.Players.Count==22,"eleven vs eleven first and ten");
  s=New(1,true);s.Snap();float z=s.Controlled.Z;s.Update(.05f,0,-1);Check(s.Controlled.Z<z,"analog backwards");float x=s.Controlled.X;s.Update(.05f,.25f,0);float small=s.Controlled.X-x;x=s.Controlled.X;s.Update(.05f,1,0);Check(s.Controlled.X-x>small*1.5f,"analog input accelerates toward requested speed");float step=s.Controlled.Step;s.Update(.05f,1,0);Check(s.Controlled.Step>step,"animation stride tracks actual movement");x=s.Controlled.X;s.Update(.05f,0,0);Check(s.Controlled.X==x&&s.Controlled.VX==0,"release stick plants feet without gliding");
  s=New(2,true);s.Snap();s.BeginPass(3);s.ReleasePass();Check(s.Target.Number==22&&s.State==Phase.Flight&&s.Charge==0,"RB icon throws lob checkdown");
  s=New(3,true);s.Snap();s.BeginPass(4);Advance(s,6);s.ReleasePass();Check(s.Target.Number==11&&s.State==Phase.Flight&&s.Charge==1,"fifth icon hold release bullet");
  HashSet<int> nums=new HashSet<int>();for(int i=0;i<5;i++){s=New(40+i,true);s.Snap();s.BeginPass(i);nums.Add(s.Target.Number);s.ReleasePass();Check(s.State==Phase.Flight,"receiver "+(i+1)+" throws independently");}Check(nums.Count==5,"five distinct eligible targets");
  s=New(3,true);s.RunPlay=true;s.Snap();Check(s.HandoffPending&&s.Controlled.Role=="QB","run call starts visible mesh");Advance(s,9);Check(!s.HandoffPending&&s.Controlled.Role=="RB"&&s.Controlled.HasBall&&s.Rushes==1,"automatic handoff completes to RB");
  s=New(3,true);s.Snap();s.Handoff();Advance(s,9);Check(s.Controlled.Role=="RB","manual handoff completes after snap");s.Controlled.Z=32;s.Dive();Check(s.State==Phase.Dead,"dive ends practice rep");
  s=New(4,false);s.Snap();s.Controlled.Z=32;s.Dive();Check(s.Down==1&&s.FirstDown>43&&s.State==Phase.Dead,"first-down marker advances");
  s=New(4,true);s.Snap();s.BeginPass();s.Controlled.Z=s.Spot+1;s.ReleasePass();Check(s.State==Phase.Live&&!s.Charging&&s.Attempts==0,"no forward pass beyond scrimmage");
  s=New(5,false);s.Snap();s.Controlled.Z=100;s.Update(.01f,0,0);Check(s.HomeScore==6&&s.State==Phase.Conversion,"touchdown six");s.ChooseConversion(false);Check(s.State==Phase.Kick,"PAT uses kick meter");s.KickMeter=.5f;s.StopKick();Advance(s,44);Check(s.HomeScore==7&&s.State==Phase.Opponent,"well timed PAT scores one");
  s=New(5,false);s.Snap();s.Controlled.Z=100;s.Update(.01f,0,0);s.ChooseConversion(true);s.Punt();s.FieldGoal();Check(s.IsConversion&&s.State==Phase.PreSnap&&s.HomeScore==6,"two-point try cannot kick or punt");
  s=New(5,false);s.Snap();s.Controlled.Z=100;s.Update(.01f,0,0);s.ChooseConversion(true);s.Snap();s.Controlled.Z=100;s.Update(.01f,0,0);Check(s.HomeScore==8,"two point conversion");
  s=New(6,false);s.Spot=80;s.FieldGoal();s.KickMeter=.5f;s.StopKick();Advance(s,44);Check(s.HomeScore==3,"accurate in-range field goal");s=New(6,false);s.Spot=80;s.FieldGoal();s.KickMeter=0;s.StopKick();Advance(s,44);Check(s.HomeScore==0,"mistimed field goal misses");
  s=New(7,false);for(int i=0;i<4;i++){s.Snap();s.Controlled.Z=s.Spot;s.Dive();if(i<3)s.Next();}Check(s.State==Phase.Opponent,"four failures turn over");
  s=New(8,false);s.Clock=.01f;s.Snap();s.Controlled.X=27;s.Update(.05f,0,0);Check(s.Quarter==2&&s.Clock==60,"expired quarter advances after play");
  s=New(9,false);s.Clock=0;s.Quarter=4;s.Snap();s.Controlled.Z=100;s.Update(.01f,0,0);s.ChooseConversion(false);s.KickMeter=.5f;s.StopKick();Advance(s,44);Check(s.State==Phase.GameOver&&s.HomeScore==7&&s.AwayScore==0,"final conversion no phantom opponent drive");
  s=New(10,false);HashSet<string> calls=new HashSet<string>();bool repeats=false;string last="";for(int i=0;i<60;i++){s.NewGame();calls.Add(s.Coverage);if(s.Coverage==last)repeats=true;last=s.Coverage;}Check(calls.Count==8&&!repeats,"eight defensive calls without consecutive repeats");
  int sacks=0;float pressureMax=0;for(int i=0;i<30;i++){s=New(90+i,false);s.Snap();for(int j=0;j<160&&s.State==Phase.Live;j++){s.Update(.05f,0,0);pressureMax=Math.Max(pressureMax,s.Pressure);}if(s.Sacks>0)sacks++;}Console.WriteLine("Pressure sample: "+sacks+" sacks / 30 stationary pockets; max "+pressureMax.ToString("F2"));Check(sacks>=20&&pressureMax>.6f,"finite pocket and meaningful QB pressure");
  int complete=0,drops=0,miss=0,picks=0;for(int i=0;i<250;i++){s=New(300+i,true);s.Snap();s.BeginPass(i%5);s.ReleasePass();Resolve(s);complete+=s.Completions;drops+=s.Drops;if(s.State==Phase.Dead&&s.Drops==0)miss++;}Console.WriteLine("Open pass sample: "+complete+" completions / "+drops+" drops / "+miss+" misses");Check(complete>100&&complete<245&&drops>=8,"open receivers can drop passes");
  for(int i=0;i<100;i++){s=New(900+i,false);s.Snap();s.BeginPass(i%5);s.ReleasePass();Resolve(s);picks+=s.Interceptions;}Console.WriteLine("Defended sample: "+picks+" interceptions / 100 throws");Check(picks>0,"defenders intercept real throws");
  s=New(42,true);s.Snap();s.LeadX=30;s.BeginPass();s.ReleasePass();Resolve(s);Check(s.Completions==0&&s.State==Phase.Dead,"badly led pass incomplete");
  s=New(42,true);s.Snap();s.Controlled.Z=35;s.Players[1].X=1;s.Players[1].Z=31;Check(s.Lateral()&&s.FlightTargetZ<35,"LB lateral strictly backwards");Resolve(s);Check(s.State==Phase.Live||s.State==Phase.Loose,"lateral can be caught or loose");
  s=New(43,true);s.Snap();s.Controlled.Z=45;foreach(Player p in s.Players)if(p!=s.Controlled)p.Z=80;s.Lateral();Resolve(s);Check(s.State==Phase.Loose&&s.Fumbles==1,"blind backward toss is live fumble");s.Players[1].X=s.BallX;s.Players[1].Z=s.BallZ;Advance(s,8);Check(s.State==Phase.Live&&s.Controlled==s.Players[1],"offense recovers loose ball");
  s=New(44,false);s.Snap();s.Controlled.Z=45;foreach(Player p in s.Players)if(p!=s.Controlled){p.X=100;p.Z=80;}s.Lateral();Resolve(s);Player defender=s.Players[11];defender.X=s.BallX;defender.Z=s.BallZ;Advance(s,8);Check(s.State==Phase.Opponent&&s.Fumbles==1,"defense recovers lateral for turnover");
  s=New(45,true);float c=s.Clock;s.Snap();Advance(s,80);Check(s.Clock==c,"practice clock stopped");
  s=New(46,false);s.Timeout();Check(s.Timeouts==2,"timeout budget decrements");
  s=New(47,false);Random r=new Random(4);bool finite=true;for(int i=0;i<24000;i++){if(s.State==Phase.GameOver)s.NewGame();if(s.State==Phase.Dead||s.State==Phase.Opponent)s.Next();if(s.State==Phase.PreSnap){s.CyclePlay(r.Next(6));s.RunPlay=r.Next(3)==0;s.Snap();}if(s.State==Phase.Conversion)s.ChooseConversion(r.Next(2)==0);if(s.State==Phase.Kick){s.KickMeter=.5f;s.StopKick();}if(s.State==Phase.Live&&s.Controlled.Role=="QB"&&!s.Charging)s.BeginPass(r.Next(5));if(s.Charging&&r.Next(15)==0)s.ReleasePass();s.Update(.05f,(float)r.NextDouble()*2-1,(float)r.NextDouble()*2-1);foreach(Player p in s.Players)if(float.IsNaN(p.X)||float.IsNaN(p.Z)||float.IsInfinity(p.X)||float.IsInfinity(p.Z))finite=false;}Check(finite,"24000 randomized frames finite");
  Console.WriteLine("Simulation: "+checks+" checks, "+failures+" failures");return failures;
 }
}
}
