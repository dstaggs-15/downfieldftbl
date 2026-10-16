using System;
using System.IO;
namespace Downfield {
public static class ProfileTests {
 static int checks,failures;
 static void Check(bool ok,string name){checks++;if(!ok){failures++;Console.WriteLine("FAIL Profile: "+name);}}
 public static int Run(){checks=failures=0;string old=Profile.StorageOverride;string root=Path.Combine(Path.GetTempPath(),"Downfield-profile-test-"+Guid.NewGuid().ToString("N"));Profile.StorageOverride=Path.Combine(root,"club.txt");try{
  Profile p=Profile.Load();Check(p.Credits==12&&p.Week==1,"missing file defaults");p.Buy(0);Check(p.Passing==1&&p.Credits==8,"upgrade purchase cost");Profile q=Profile.Load();Check(q.Passing==1&&q.Credits==8,"saved profile roundtrip");p.Credits=1000;for(int i=0;i<8;i++)p.Buy(0);int credits=p.Credits;p.Buy(0);Check(p.Passing==5&&p.Credits==credits,"level cap does not spend credits");p.Credits=0;p.Buy(1);Check(p.Running==0&&p.Credits==0,"insufficient funds preserves level");p.Finish(0);Check(p.Draws==1&&p.Wins==0&&p.Losses==0&&p.Week==2&&p.Credits==4,"draw records draw and reward");p.Finish(1);p.Finish(-1);Check(p.Wins==1&&p.Losses==1&&p.Draws==1&&p.Week==4&&p.Credits==16,"wins losses rewards");q=Profile.Load();Check(q.Wins==1&&q.Losses==1&&q.Draws==1&&q.Passing==5,"complete profile roundtrip");File.WriteAllText(Profile.SavePath,"10,1,2,3,4,5,6,1");q=Profile.Load();Check(q.Draws==0&&q.Wins==5&&q.Defense==3,"legacy reserved field ignored");File.WriteAllText(Profile.SavePath,"-9,99,99,99,0,-5,999999,2,2");q=Profile.Load();Check(q.Credits==0&&q.Passing==5&&q.Running==5&&q.Defense==5&&q.Week==1&&q.Wins==0&&q.Losses==99999,"save values clamped");File.WriteAllText(Profile.SavePath,"corrupted file");q=Profile.Load();Check(q.Credits==12&&q.Week==1,"corrupt file defaults");
 }finally{Profile.StorageOverride=old;if(Directory.Exists(root))Directory.Delete(root,true);}Console.WriteLine("Profile: "+checks+" checks, "+failures+" failures; isolated temporary storage.");return failures;}
}
}
