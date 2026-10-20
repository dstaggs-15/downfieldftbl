using System;
using System.Collections.Generic;
namespace Downfield {
public partial class Simulation {
 readonly Queue<int> offensiveHistory=new Queue<int>();
 int recentDeep,recentRuns,recentScrambles;bool offenseRemembered;
 void ResetDefensiveMemory(){offensiveHistory.Clear();recentDeep=recentRuns=recentScrambles=0;Array.Clear(callsUsed,0,callsUsed.Length);offenseRemembered=true;}
 void RememberOffense(){if(offenseRemembered||playTime<=0)return;offenseRemembered=true;int read=usedForwardPass?(toZ-fromZ>=18?2:1):Controlled!=null&&Controlled.Role=="QB"&&BallCrossedScrimmage?4:RunPlay?3:0;if(read==0)return;offensiveHistory.Enqueue(read);if(offensiveHistory.Count>10)offensiveHistory.Dequeue();recentDeep=recentRuns=recentScrambles=0;foreach(int call in offensiveHistory){if(call==2)recentDeep++;if(call==3)recentRuns++;if(call==4)recentScrambles++;}}
 int DefensiveCall(){int call=random.Next(Coverages.Length);if(recentDeep>=3&&random.Next(100)<60){int[] shells={1,2,5,6};call=shells[random.Next(shells.Length)];}else if(recentRuns+recentScrambles>=4&&random.Next(100)<45){int[] fronts={0,4,7};call=fronts[random.Next(fronts.Length)];}if(call==previousCoverage)call=(call+1+random.Next(Coverages.Length-1))%Coverages.Length;return call;}
 float CoverageCushion(Player defender,Player receiver){return 3.2f+Math.Max(0,receiver.Speed-defender.Speed)*1.5f;}
 // Help follows visible routes in a defender's part of the field, even outside the old landmark radius.
 void DeepHelp(Player p,float lane,bool single,out float tx,out float tz){lane=single?0:lane<0?-13.5f:13.5f;Player threat=null;float best=Spot+1;foreach(int index in targets){Player r=Players[index];if(!r.Active||r.Z<Spot+3||p.Role=="LB"&&Math.Abs(r.X)>12)continue;if(!single&&r.X*lane<0&&Math.Abs(r.X)>3)continue;float width=Math.Abs(r.X-lane),support=9;foreach(Player d in Players)if(d!=p&&d.Team==1&&d.Active&&d.Role!="S"&&d.Role!="DL"&&d.KnockTime<=0)support=Math.Min(support,Distance(r.X,r.Z,d.X,d.Z));float projection=r.Z+Math.Max(0,r.VZ)*.7f-width*.25f+Math.Min(6,support)*.6f;if(projection>best){best=projection;threat=r;}}tx=lane;tz=Math.Min(102,Spot+18+recentDeep*.45f);if(threat!=null){tx=Clamp(threat.X+threat.VX*.25f,-25,25);tz=Math.Min(105,Math.Max(tz,threat.Z+CoverageCushion(p,threat)+Math.Max(0,threat.VZ)*.55f));}}
 bool SpyQuarterback(Player p){if(p.Role!="LB"||State!=Phase.Live||Controlled.Role!="QB"||playTime<.9f)return false;bool rollout=Math.Abs(Controlled.X)>7||Controlled.Z<Spot-9;bool spy=p.Mark==1;return spy&&(rollout||recentScrambles>=2)&&Controlled.Z>Spot-13;}
 bool LastLine(Player p){if(p.Role!="S"&&p.Role!="CB"&&p.Role!="NB")return false;foreach(Player d in Players)if(d!=p&&d.Team==1&&d.Active&&d.KnockTime<=0&&d.BlockPartner<0&&Math.Abs(d.X-Controlled.X)<10&&d.Z>p.Z+1.5f)return false;return true;}
}
}
