using System;
namespace Downfield {
public partial class Simulation {
 public const float HopDuration=.58f,MoveDuration=.30f;
 public float OpenFieldPace=1,HurdleRiskWindow;public bool DangerousLanding;public int HurdlesMade,BigHits,MissedTackles;
 public void Update(float dt,float mx,float mz){if(OnsideRulesPending)return;dt=CatchSimulationDelta(Clamp(dt,0,.05f));float targetPace=1;
  if(!Practice&&State==Phase.Live&&Controlled!=null&&Controlled.HasBall&&Controlled.Z>Spot+12&&Sprint&&mz>.65f&&Math.Abs(mx)<.45f&&skillTime<=0){bool clear=true;foreach(Player d in Players)if(d.Team==1&&d.Active&&d.KnockTime<=0&&d.Blocked<=0){float distance=Distance(d.X,d.Z,Controlled.X,Controlled.Z);if(distance<9||d.Z>Controlled.Z-2){clear=false;break;}}if(clear)targetPace=1.45f;}
  if(targetPace==1)OpenFieldPace=1;else OpenFieldPace=Math.Min(targetPace,OpenFieldPace+dt*.75f);
  float left=dt*OpenFieldPace;while(left>.00001f){float step=Math.Min(.025f,left);UpdateStep(step,mx,mz);left-=step;}
 }
 float InterceptTime(Player defender,float speed){float x=Controlled.X-defender.X,z=Controlled.Z-defender.Z;float a=Controlled.VX*Controlled.VX+Controlled.VZ*Controlled.VZ-speed*speed,b=2*(x*Controlled.VX+z*Controlled.VZ),c=x*x+z*z,disc=b*b-4*a*c;float time=Distance(0,0,x,z)/Math.Max(1,speed);
  if(Math.Abs(a)>.01f&&disc>=0){float root=(float)Math.Sqrt(disc),one=(-b-root)/(2*a),two=(-b+root)/(2*a);if(one>.01f)time=one;if(two>.01f&&(one<.01f||two<one))time=two;}return Clamp(time,.08f,2.5f);
 }
 void RunPursuit(Player p,float speed,float dt){float distance=Distance(p.X,p.Z,Controlled.X,Controlled.Z);p.TackleCooldown=Math.Max(0,p.TackleCooldown-dt);
  if(p.LungeTime>0){p.LungeTime=Math.Max(0,p.LungeTime-dt);GroundStep(p,p.LungeX,p.LungeZ,speed*1.15f,dt);p.Pose="dive";p.PoseTime=.12f;if(p.LungeTime<=0&&Distance(p.X,p.Z,Controlled.X,Controlled.Z)>1.6f){p.PlantTime=.17f;MissedTackles++;}return;}
  if(p.ShedTime>0&&p.LastBlocker>=0&&p.LastBlocker<Players.Count){Player b=Players[p.LastBlocker];if(Distance(p.X,p.Z,b.X,b.Z)<2.5f&&p.Z>b.Z-1){Move(p,b.X+p.ShedSide*1.8f,b.Z-.7f,speed,dt);return;}}
  float lead=InterceptTime(p,speed);float tx=Controlled.X+Controlled.VX*lead,tz=Controlled.Z+Controlled.VZ*lead;bool ahead=p.Z>Controlled.Z+1,last=LastLine(p);HardRunSupport(p,distance,ref tx,ref tz);
  if(ahead&&(p.Role=="S"||p.Role=="CB"||p.Role=="NB"||p.Role=="LB")){float side=Controlled.X>=0?1:-1;if(Math.Abs(Controlled.X)>12&&p.X*side>Controlled.X*side){tx+=side*.65f;tz=Math.Max(Controlled.Z+1,tz);}if(last&&distance>5){tz=Math.Max(Controlled.Z+3,tz);tx=Clamp(tx,Controlled.X-4,Controlled.X+4);}}
  // A real committed tackle can be dodged or hurdled; defenders cannot steer during it.
  bool closing=Controlled.VX*(p.X-Controlled.X)+Controlled.VZ*(p.Z-Controlled.Z)>1;
  if(distance>1.35f&&distance<(last?1.9f:Difficulty>=3?1.9f:2.65f)&&p.TackleCooldown<=0&&p.PlantTime<=0&&closing){float aimX=Controlled.X+Controlled.VX*.12f-p.X,aimZ=Controlled.Z+Controlled.VZ*.12f-p.Z,length=Distance(0,0,aimX,aimZ);p.LungeX=aimX/Math.Max(.01f,length);p.LungeZ=aimZ/Math.Max(.01f,length);p.LungeTime=Difficulty>=3?.10f:.16f;p.TackleCooldown=Difficulty>=3?.5f:.75f;}
  Move(p,Clamp(tx,-26,26),tz,speed,dt);
 }
 bool ClearHurdle(Player defender){if(Controlled.Pose!="hurdle"||skillTime<=.12f||skillTime>=.5f||defender.Z<Controlled.Z-.6f)return false;int support=0;foreach(Player d in Players)if(d!=defender&&d.Team==1&&d.Active&&d.KnockTime<=0&&Distance(d.X,d.Z,Controlled.X,Controlled.Z)<2.2f)support++;
  float chance=Clamp(.78f+(Controlled.OVR-defender.OVR)*.006f-(support*.22f),.35f,.9f);if(random.NextDouble()>chance)return false;
  KnockDown(defender,Controlled.VX,Controlled.VZ,7);HurdlesMade++;HitX=Controlled.X;HitZ=Controlled.Z;HitFlash=.55f;HitLabel="HURDLED!";Cue("crowd_big");return true;
 }
}
}
