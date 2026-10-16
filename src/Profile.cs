using System;
using System.IO;
using System.Globalization;
namespace Downfield {
public class Profile {
    public int Credits=12,Passing=0,Running=0,Defense=0,Week=1,Wins=0,Losses=0,Draws=0;
    public string Notice="Build your club. Every upgrade affects the field.";
    internal static string StorageOverride;
    public static string SavePath {get{return StorageOverride??Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Downfield","club.txt");}}
    public static Profile Load(){Profile p=new Profile();try{string[] v=File.ReadAllText(SavePath).Split(',');if(v.Length==8||v.Length==9){int[] a=new int[v.Length];for(int i=0;i<v.Length;i++)a[i]=Math.Max(0,Math.Min(99999,int.Parse(v[i],CultureInfo.InvariantCulture)));p.Credits=a[0];p.Passing=Math.Min(5,a[1]);p.Running=Math.Min(5,a[2]);p.Defense=Math.Min(5,a[3]);p.Week=Math.Max(1,a[4]);p.Wins=a[5];p.Losses=a[6];if(v.Length==9)p.Draws=a[7];}}catch(Exception){ }return p;}
    public void Save(){try{Directory.CreateDirectory(Path.GetDirectoryName(SavePath));string tmp=SavePath+".tmp";File.WriteAllText(tmp,string.Join(",",new int[]{Credits,Passing,Running,Defense,Week,Wins,Losses,Draws,2}));if(File.Exists(SavePath))File.Replace(tmp,SavePath,null);else File.Move(tmp,SavePath);}catch(Exception){Notice="Could not save club. Check access to LocalAppData.";}}
    public void Buy(int item){int level=item==0?Passing:item==1?Running:Defense;int cost=4+level*3;if(level>=5){Notice="This unit is fully developed.";return;}if(Credits<cost){Notice="Need "+cost+" credits. Finish a game to earn more.";return;}Credits-=cost;if(item==0)Passing++;else if(item==1)Running++;else Defense++;Notice="Training complete. Upgrade applies to your next game.";Save();}
    public void Finish(int result){Week++;if(result>0)Wins++;else if(result<0)Losses++;else Draws++;Credits+=result>0?8:4;Notice=result>0?"Victory! Eight credits added to the club.":result==0?"Draw. Four credits earned.":"Four credits earned. Prepare for the next opponent.";Save();}
}
}
