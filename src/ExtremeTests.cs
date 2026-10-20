using System;
using System.IO;
namespace Downfield {
public static class ExtremeTests {
 public static int Run(){int checks=0,failures=0;Action<bool,string> test=(ok,n)=>{checks++;if(!ok){failures++;Console.WriteLine("FAIL Extreme: "+n);}};
  DefenseBalanceTests.Sample normal=DefenseBalanceTests.Trials(95,2,true,135,true,80),extreme=DefenseBalanceTests.Trials(95,2,true,135,true,80,3);test(extreme.Yards<normal.Yards*.85f&&extreme.TD<=normal.TD,"elite custom verticals face stronger coverage");test(extreme.First>3&&extreme.Catches>3,"late reads sometimes work while early timing remains preferable");DefenseBalanceTests.Sample early=DefenseBalanceTests.Trials(95,0,false,75,true,80,3);test(early.Catches>20&&early.First>10,"short timing routes still work");
  int points=0,peak=0,normalPoints=0;bool finished=true;for(int seed=0;seed<30;seed++){Simulation s=DefenseBalanceTests.WholeGame(95,seed,true,3),n=DefenseBalanceTests.WholeGame(95,seed,true);points+=s.HomeScore;normalPoints+=n.HomeScore;peak=Math.Max(peak,s.HomeScore);finished&=s.State==Phase.GameOver;}Console.WriteLine("Extreme elite full games: avg "+(points/30f).ToString("F1")+", peak "+peak+"; Normal avg "+(normalPoints/30f).ToString("F1"));test(finished&&points<normalPoints*.8f&&peak<70,"30 elite full games resist repeated vertical and onside farming");
  Franchise club=DefenseBalanceTests.Club(95);Simulation e=new Simulation(83);e.Difficulty=3;club.Apply(e,1);e.NewGame();ClubPlayer db=club.Starter(1,15);test(e.Players[15].OVR==db.OVR&&Math.Abs(e.Players[15].Speed-Simulation.SkillSpeed("CB",db.Speed))<.01,"extreme keeps real defender ratings and speed");
  string old=Profile.StorageOverride,dir=Path.Combine(Path.GetTempPath(),"Downfield-settings-"+Guid.NewGuid().ToString("N"));Profile.StorageOverride=Path.Combine(dir,"club.txt");try{test(GamePreferences.Load().Difficulty==1,"default difficulty stays Normal");new GamePreferences{Difficulty=3}.Save();test(GamePreferences.Load().Difficulty==3,"Extreme persists independently of franchise results");new GamePreferences{Difficulty=99}.Save();test(GamePreferences.Load().Difficulty==4,"invalid setting clamps to supported range");}finally{Profile.StorageOverride=old;if(Directory.Exists(dir))Directory.Delete(dir,true);}
  Console.WriteLine("Extreme: "+checks+" checks, "+failures+" failures");return failures;
 }
}
}
