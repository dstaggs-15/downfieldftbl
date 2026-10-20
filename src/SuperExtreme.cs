using System;
namespace Downfield {
public partial class Simulation {
 public void SetDifficulty(int level){Difficulty=Math.Max(0,Math.Min(4,level));if(State==Phase.PreSnap)Setup();}public bool SuperExtreme {get{return Difficulty>=4;}}
 void SetupSuperDefense(){if(!SuperExtreme||Practice)return;foreach(Player p in Players)if(p.Team==1){p.OVR=p.Hands=p.Power=99;p.Speed=Math.Max(p.Speed,SkillSpeed(p.Role,99)+1.2f);}}
 void SuperCoverage(Player p,float dt){float x=p.StartX,z=Spot+17;if(p.Mark>=0&&Players[p.Mark].Active){Player r=Players[p.Mark];x=r.X+r.VX*.24f;z=r.Z+1.0f+Math.Max(0,r.VZ)*.16f;}else DeepHelp(p,p.StartX<0?-12:12,false,out x,out z);
  if(State==Phase.Flight&&passTime>.045f&&Distance(p.X,p.Z,toX,toZ)<22){x=toX;z=toZ;}p.AimX=x;p.AimZ=z;Move(p,x,z,p.Speed*1.2f,dt);
 }
 void HardRunSupport(Player p,float distance,ref float x,ref float z){if(Difficulty<3)return;float lead=Math.Min(InterceptTime(p,p.Speed*1.28f),distance<9?.35f:.9f);x=Controlled.X+Controlled.VX*lead;z=Controlled.Z+Controlled.VZ*lead;
  // The next defender protects the escape lane instead of following the first tackler.
  if(p.Z>Controlled.Z+2&&distance>4){foreach(Player d in Players)if(d!=p&&d.Team==1&&d.Active&&d.KnockTime<=0&&d.BlockPartner<0&&Distance(d.X,d.Z,Controlled.X,Controlled.Z)<distance-2){float side=p.X>=Controlled.X?1:-1;x=Controlled.X+side*2.2f;z=Controlled.Z+Math.Max(3,Controlled.VZ*.5f);break;}}
 }
}
}
