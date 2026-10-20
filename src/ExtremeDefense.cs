using System;
namespace Downfield {
public partial class Simulation {
 void ExtremeCoverage(Player p,ref float tx,ref float tz){if(Difficulty!=3||playTime<.12f)return;
  if(p.Role=="S"){
   Player threat=null;float best=-999;foreach(int index in targets){Player r=Players[index];if(!r.Active||r.Z<Spot+2||(r.X<0)!=(p.StartX<0)&&Math.Abs(r.X)>4)continue;
    float support=12;foreach(Player d in Players)if(d!=p&&d.Team==1&&d.Active&&d.Role!="S"&&d.Role!="DL"&&d.KnockTime<=0)support=Math.Min(support,Distance(r.X,r.Z,d.X,d.Z));float score=r.Z-Spot+support*1.25f+Math.Max(0,r.VZ)*.45f;
    if(score>best){best=score;threat=r;}
   }
   bool vertical=false;foreach(int receiver in targets){Player route=Players[receiver];if(route.Active&&(route.Z>Spot+12||route.Z>Spot+8&&route.VZ>3))vertical=true;}if(threat!=null){tx=Clamp(threat.X+threat.VX*.3f,-25,25);tz=Math.Min(105,Math.Max(!vertical&&playTime>1?Spot+9:Spot+18,threat.Z+(!vertical&&playTime>1?1.7f:3.5f)+Math.Max(0,threat.Speed-p.Speed)*1.8f+Math.Max(0,threat.VZ)*.65f));}
  }else if(p.Mark>=0&&Players[p.Mark].Active){
   Player r=Players[p.Mark];if(r.Active){tx=r.X+r.VX*.2f;tz=r.Z+(r.Z>Spot+10?3.2f:.9f)+Math.Max(0,r.Speed-p.Speed)*1.5f;}
  }
 }
}
}
