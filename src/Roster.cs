using System;
using System.IO;
namespace Downfield {
public class Recruit {
 public string Name,Role; public int OVR,Speed,Hands,Power;
 public Recruit(string n,string r,int o,int s,int h,int p){Name=n;Role=r;OVR=o;Speed=s;Hands=h;Power=p;}
 public int Cost {get{return 3+(OVR-70)/2;}}
}
public class Roster {
 public static readonly string[] Teams={"CHICAGO CURRENT","NEW ORLEANS CRESCENT","ATLANTA FIRE","DALLAS STAMPEDE","MIAMI TIDES","SEATTLE EVERGREENS","NEW YORK EMPIRE","LOS ANGELES COMETS"};
 public static readonly Recruit[] Market={new Recruit("COLE","QB",70,74,70,72),new Recruit("BROOKS","RB",75,82,70,76),new Recruit("RIVERS","WR",80,88,82,71),new Recruit("JAMES","TE",85,80,90,88),new Recruit("CHASE","RB",90,96,82,91),new Recruit("KING","QB",95,86,94,98),new Recruit("BLAZE","WR",99,99,98,91),new Recruit("WALL","OL",88,76,70,96),new Recruit("LOCKE","DEF",92,93,90,95)};
 public int Team; public int[] Signed=new int[9];
 public Roster(){for(int i=0;i<Signed.Length;i++)Signed[i]=-1;}
 string PathName {get{return Profile.SavePath+".roster";}}
 public static Roster Load(){Roster r=new Roster();try{string[] a=File.ReadAllLines(r.PathName);r.Team=Math.Max(0,Math.Min(Teams.Length-1,int.Parse(a[0])));for(int i=1;i<a.Length&&i<=9;i++){int n=int.Parse(a[i]);if(n>=0&&n<Market.Length)r.Signed[i-1]=n;}}catch{}return r;}
 public void Save(){try{Directory.CreateDirectory(Path.GetDirectoryName(PathName));string[] a=new string[10];a[0]=Team.ToString();for(int i=0;i<9;i++)a[i+1]=Signed[i].ToString();File.WriteAllLines(PathName+".tmp",a);if(File.Exists(PathName))File.Replace(PathName+".tmp",PathName,null);else File.Move(PathName+".tmp",PathName);}catch{}}
 public void Sign(int index,Profile p){Recruit c=Market[index];if(Signed[index]==index){p.Notice="Already signed: "+c.Name;return;}if(p.Credits<c.Cost){p.Notice="Need "+c.Cost+" credits to sign "+c.Name;return;}p.Credits-=c.Cost;Signed[index]=index;p.Notice="Signed "+c.Name+" / "+c.OVR+" OVR. Available next match.";p.Save();Save();}
 public Recruit Best(string role){Recruit best=null;for(int i=0;i<9;i++)if(Signed[i]==i&&Market[i].Role==role&&(best==null||Market[i].OVR>best.OVR))best=Market[i];return best;}
 public void Apply(Simulation s){s.Roster=this;Recruit d=Best("DEF");if(d!=null)s.Defense+=(d.OVR-70)/6;}
}
}
