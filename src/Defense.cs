using System;
namespace Downfield {
public partial class Simulation {
 public bool Disguised;public string RushPlan="";int stunt,previousStunt,stuntSide;
 public readonly string[] Coverages={"MAN FREE","COVER TWO","COVER THREE","ZONE BLITZ","PRESS MAN","QUARTERS","TAMPA TWO","ROBBER"};
 void DefensiveSetup(){coverageId=DefensiveCall();previousCoverage=coverageId;Coverage=Coverages[coverageId];stunt=previousStunt==0&&random.Next(100)<(Down>=3?38:25)?1+random.Next(3):0;previousStunt=stunt;stuntSide=random.Next(2);RushPlan=new string[]{"STRAIGHT","TACKLE-END LOOP","END-TACKLE LOOP","CROSS FIRE"}[stunt];Disguised=random.Next(100)<65;
  for(int i=11;i<Players.Count;i++){Player p=Players[i];p.X=p.StartX+Signed()*.65f;p.Z=p.StartZ+Signed()*.6f;p.Mark=p.Role=="CB"?(p.StartX<0?2:3):p.Role=="NB"?10:p.Role=="LB"?(p.StartX>0?4:1):-1;
   if(Disguised){if(p.Role=="S"){p.X=p.StartX<0?-10:10;p.Z=Spot+15;}if(p.Role=="CB")p.Z=Spot+5.5f;if(p.Role=="LB")p.Z=Spot+5;}
   else{if(coverageId==4&&p.Role=="CB")p.Z=Spot+1.8f;if(coverageId==3&&p.Role=="LB")p.Z=Spot+2;}
   if(p.Role=="CB"&&coverageId!=4){Player receiver=Players[p.Mark];p.Z=Spot+5+Math.Max(0,receiver.Speed-p.Speed)*1.25f;}if(p.Role=="S")p.Z=Math.Min(102,Spot+17+recentDeep*.45f);
   p.StartX=p.X;p.StartZ=p.Z;p.ThinkTime=0;p.RushStage=0;p.RushAssignment=0;if(p.Role=="DL"&&stunt>0){int index=i-11;int end=stuntSide==0?0:3,inside=stuntSide==0?1:2;if(stunt==3){p.RushAssignment=index==(stuntSide==0?1:2)?2:index==(stuntSide==0?2:1)?1:0;}else{p.RushAssignment=index==(stunt==1?end:inside)?2:index==(stunt==1?inside:end)?1:0;}}
  }
 }
 void ReadDefense(Player p,float dt){if(!p.Active||p.KnockTime>0)return;if(p.BlockPartner>=0){p.VX=p.VZ=0;p.Pose="block";p.PoseTime=.1f;return;}if(p.Blocked>0){if(p.Pose!="stumble")p.Pose="block";p.VX=p.VZ=0;return;}float speed=p.Speed*(.95f+Difficulty*.035f);bool runner=State==Phase.Live&&(Controlled.Role!="QB"||Controlled.Z>Spot);
  if(runner){PursueRunner(p,p.Speed*(1.12f+Difficulty*.025f),dt);return;}
  if(SpyQuarterback(p)){Move(p,Controlled.X,Math.Min(Spot+1,Controlled.Z+2),p.Speed*1.1f,dt);return;}
  bool blitz=p.Role=="DL"||coverageId==3&&p.Role=="LB"&&playTime>.6f;
  if(blitz){Rush(p,speed,dt);return;}
  float tx=p.StartX,tz=Spot+12;
  if(Disguised&&playTime<.35f){tz=p.StartZ;tx=p.StartX;}
  else if(coverageId==0||coverageId==4||coverageId==7){if(p.Mark>=0&&Players[p.Mark].Active){Player r=Players[p.Mark];float cushion=coverageId==4?.6f:1.0f;
    tx=r.X+r.VX*.10f;tz=r.Z+cushion;
    // Coverage tracks a route after a reaction interval, not every rendered frame.
    if(playTime<.65f&&coverageId==4&&Distance(p.X,p.Z,r.X,r.Z)<1.7f){float release=(r.OVR-p.OVR)*.006f+(r.Power-p.Power)*.004f;if(release<0)r.Blocked=Math.Min(.14f,-release);p.Pose="block";}
   }else{bool robber=coverageId==7&&p.StartX<0;if(robber){tx=Controlled.X*.5f;tz=Spot+9;for(int n=0;n<5;n++){Player r=Receiver(n);if(r.Active&&r.Z>Spot+14&&r.X<3){DeepHelp(p,-10,false,out tx,out tz);break;}}}else DeepHelp(p,p.StartX<0?-12:12,coverageId==7,out tx,out tz);}
  }else{
   bool deep=p.Role=="S"||coverageId==2&&p.Role=="CB"||coverageId==5&&p.Role=="CB";
   float zx=p.StartX;if(p.Role=="S")zx=p.StartX<0?-10:10;
   float zz=Spot+(deep?22:p.Role=="LB"?7:p.Role=="CB"?8:10);
   if(coverageId==6&&p.Role=="LB"&&p.StartX>0){zz=Spot+19;deep=true;zx=0;}
   tx=zx;tz=zz;Player near=null;float best=deep?13:9;
   for(int n=0;n<5;n++){Player r=Receiver(n);if(!r.Active)continue;float score=Distance(r.X,r.Z,zx,zz);if(deep&&r.Z>zz)score*=.6f;if(score<best){best=score;near=r;}}
   if(near!=null){tx=near.X;tz=near.Z+(deep?2.5f:1.3f);if(!deep){tx=Clamp(tx,zx-8,zx+8);tz=Clamp(tz,Spot+2,Spot+15);}}
   if(deep){if(p.Role=="CB"){Player r=Players[p.Mark];if(r.Active){tx=r.X+r.VX*.2f;tz=Math.Min(105,r.Z+CoverageCushion(p,r));}}else DeepHelp(p,zx,p.Role=="LB",out tx,out tz);}
   else if(p.Mark>=0){Player seam=Players[p.Mark];if(seam.Active&&seam.Z>Spot+9&&seam.VZ>1&&Math.Abs(seam.X-p.StartX)<15){tx=seam.X+seam.VX*.15f;tz=seam.Z+1.5f;}}
  }
  float familiarity=Math.Min(.12f,callsUsed[Math.Max(0,Math.Min(callsUsed.Length-1,Playbook))]*.015f);
  if(State==Phase.Flight&&passTime>.38f-familiarity+(p.OVR<80?.06f:0)&&Distance(p.X,p.Z,toX,toZ)<13){tx=toX;tz=toZ;}
  if(p.Role!="DL"&&(State==Phase.Flight||tz>Spot+9))speed=p.Speed*(1.10f+Difficulty*.025f);
  p.ThinkTime-=dt;if(p.ThinkTime<=0){p.AimX=tx;p.AimZ=tz;p.ThinkTime=Math.Max(.11f,.30f-(p.OVR-70)*.006f)+(p.Number%3)*.015f;}
  Move(p,p.AimX,p.AimZ,speed,dt);
 }
 void Rush(Player p,float speed,float dt){float tx=Controlled.X,tz=Controlled.Z;int index=Players.IndexOf(p)-11;float side=stuntSide==0?-1:1;
  if(p.Role=="DL"&&p.RushAssignment==2){
   // The looper first travels behind his partner, then turns upfield only after reaching the landmark.
   float lane=stunt==3?-side*1.5f:stunt==1?-side*.7f:side*6.8f;
   if(p.RushStage==0){tx=p.StartX;tz=Spot+2.2f;if(playTime>=.35f&&Distance(p.X,p.Z,tx,tz)<.8f)p.RushStage=1;}
   else if(p.RushStage==1){tx=lane;tz=Spot+2.2f;if(Distance(p.X,p.Z,tx,tz)<.65f)p.RushStage=2;}
   else if(p.RushStage==2){tx=lane;tz=Spot-2;if(p.Z<Spot-1)p.RushStage=3;}
  }else if(p.Role=="DL"&&p.RushAssignment==1&&p.RushStage==0){tx=p.StartX+side*1.8f;tz=Spot-1.6f;if(p.Z<Spot-.8f)p.RushStage=1;}
  else if(p.Role=="DL"&&playTime<.45f){tx=p.StartX;tz=Spot-1;}
  if(p.Role=="DL"&&p.RushAssignment==0&&(index==0||index==3)&&playTime>1.2f&&Controlled.Z<Spot-4){float distance=Distance(p.X,p.Z,Controlled.X,Controlled.Z);tx=Controlled.X+(index==0?-1.4f:1.4f)*Math.Min(1,distance/4);tz=Controlled.Z+.3f;}
  if(p.ShedTime>0){tx=p.X+p.ShedSide*1.3f;tz=p.Z-1.5f;}
  if(State==Phase.Flight&&Distance(p.X,p.Z,BallX,BallZ)>8){tx=Players[0].X;tz=Players[0].Z;}Move(p,tx,tz,speed,dt);
 } float[] ScrambleRoute(Player p,float t){float side=p.StartX<0?-1:1;float depth=p.Role=="RB"?5:p.Role=="TE"?10:Math.Abs(p.StartX)>15?16:22;float cycle=(float)Math.Sin((t-3.8f)*1.25f+(p.Number%5));float x=Clamp(Controlled.X*.65f+side*(p.Role=="RB"?7:14)+cycle*4,-24,24);float z=Math.Max(Spot+3,Controlled.Z+depth+cycle*2.5f);return new float[]{x,z};}
}
}
