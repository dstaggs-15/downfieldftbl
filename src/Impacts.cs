using System;
namespace Downfield {
public partial class Simulation {
 void KnockDown(Player player,float x,float z,float force){ReleaseBlock(player);float length=(float)Math.Sqrt(x*x+z*z);if(length<.1f){x=0;z=player.Team==1?1:-1;length=1;}player.KnockVX=x/length*force;player.KnockVZ=z/length*force;player.KnockTime=player.KnockDuration=.95f;player.Blocked=1.1f;player.Pose="knockdown";player.PoseTime=.95f;player.VX=player.KnockVX;player.VZ=player.KnockVZ;}
 void UpdateKnockbacks(float dt){foreach(Player p in Players)if(p.KnockTime>0){p.KnockTime=Math.Max(0,p.KnockTime-dt);p.X+=p.KnockVX*dt;p.Z+=p.KnockVZ*dt;float damping=(float)Math.Exp(-dt*5);p.KnockVX*=damping;p.KnockVZ*=damping;p.Pose="knockdown";p.PoseTime=Math.Max(p.PoseTime,p.KnockTime);if(p.KnockTime<=0){p.KnockVX=p.KnockVZ=p.VX=p.VZ=0;p.Pose="idle";}}}
 void TruckImpact(Player defender,bool won){HitX=Controlled.X;HitZ=Controlled.Z;HitFlash=.95f;HitLabel=won?"TRUCKED!":"TRUCK STUFFED!";if(won){float dx=defender.X-Controlled.X,dz=defender.Z-Controlled.Z;if(Distance(0,0,Controlled.VX,Controlled.VZ)>1){dx=Controlled.VX;dz=Controlled.VZ;}KnockDown(defender,dx,dz,13.5f);Controlled.VX*=.65f;Controlled.VZ*=.65f;Message="TRUCKED / KEEP RUNNING";}else{KnockDown(Controlled,Controlled.X-defender.X,Controlled.Z-defender.Z,12.5f);Message="TRUCK STUFFED";}skillTime=0;}
}
}
