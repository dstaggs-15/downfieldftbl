using System;
namespace Downfield {
public partial class Simulation {
 public Franchise Franchise;public int OpponentTeam,Conditioning;public bool Postseason;
 public bool SkillActive {get{return skillTime>0;}}
 public float SkillCooldown {get{return moveCooldown;}}
 public bool KickAnimating;public float KickProgress,KickBallX,KickBallZ,KickBallHeight;public bool KickGood;public int SimulatedDrives;
 float kickAccuracy,kickDistance;bool kickResolved;int kickPoints;
 public float HitFlash,HitX,HitZ;public string HitLabel="";public readonly System.Collections.Generic.List<int> MatchInjuries=new System.Collections.Generic.List<int>();
 void RegisterHit(Player defender){Cue("hit");float relative=Distance(Controlled.VX,Controlled.VZ,defender.VX,defender.VZ);bool truck=Controlled.Pose=="truck"||Controlled.KnockTime>0;bool big=relative>6.8f||truck||defender.LungeTime>0||DangerousLanding;HitFlash=Math.Max(HitFlash,big?.7f:.16f);HitX=Controlled.X;HitZ=Controlled.Z;if(truck){if(Controlled.KnockTime<=0)TruckImpact(defender,false);HitLabel="TRUCK STUFFED!";}else{HitLabel=DangerousLanding?"BAD LANDING!":big?"BIG HIT!":"TACKLE";if(big){BigHits++;Cue("crowd_big");ArcadeContact(Controlled,Controlled.X-defender.X,Controlled.Z-defender.Z,relative,DangerousLanding);Message=HitLabel;}}if(big)TryInjury(Controlled,DangerousLanding);}
 readonly int[] callsUsed=new int[24];
 public void Jump(){if(State!=Phase.Live||!Controlled.HasBall||HandoffPending||Controlled.Z<=Spot||Controlled.Stamina<.24f||moveCooldown>0)return;Controlled.Stamina-=.24f;moveCooldown=.95f;skillTime=HopDuration;HurdleRiskWindow=HopDuration+.22f;skillX=skillZ=0;Cue("jump");Controlled.Pose="hurdle";Controlled.PoseTime=HopDuration;Message="JUMP / PROTECT THE BALL";}
 public static void GroundStep(Player p,float dx,float dz,float speed,float dt){float magnitude=(float)Math.Sqrt(dx*dx+dz*dz);if(magnitude<.015f){p.VX=p.VZ=0;return;}if(magnitude>1){dx/=magnitude;dz/=magnitude;}
  float current=(float)Math.Sqrt(p.VX*p.VX+p.VZ*p.VZ);float desired=(float)Math.Sqrt(dx*dx+dz*dz);float dot=current>.1f?(p.VX*dx+p.VZ*dz)/(current*Math.Max(.01f,desired)):1;
  if(current>3.5f&&dot<.35f&&p.PlantTime<=0&&p.PoseTime<=0){p.PlantTime=.12f;p.Pose="cut";p.PoseTime=.12f;}
  float traction=p.PlantTime>0?.42f:1;float acceleration=24+(p.OVR-70)*.48f;float vx=dx*speed*traction,vz=dz*speed*traction;
  float ax=vx-p.VX,az=vz-p.VZ,change=(float)Math.Sqrt(ax*ax+az*az),limit=acceleration*dt;if(change>limit){ax*=limit/change;az*=limit/change;}p.VX+=ax;p.VZ+=az;
  if(p.VX*p.VX+p.VZ*p.VZ>.04f){p.FacingX=p.VX;p.FacingZ=p.VZ;}p.X+=p.VX*dt;p.Z+=p.VZ*dt;
 }
 void SeparatePlayers(){for(int i=0;i<Players.Count;i++)for(int j=i+1;j<Players.Count;j++){Player a=Players[i],b=Players[j];if(a.Team!=b.Team||!a.Active||!b.Active||a.Blocked>0||b.Blocked>0||a.KnockTime>0||b.KnockTime>0||a.BlockPartner>=0||b.BlockPartner>=0)continue;float dx=a.X-b.X,dz=a.Z-b.Z,d=(float)Math.Sqrt(dx*dx+dz*dz);if(d>=.68f)continue;if(d<.01f){dx=(i%2==0?1:-1)*.01f;d=.01f;}float push=(.68f-d)*.5f;a.X+=dx/d*push;b.X-=dx/d*push;a.Z+=dz/d*push;b.Z-=dz/d*push;}}
 void PursueRunner(Player p,float speed,float dt){RunPursuit(p,speed,dt);}
 bool Contact(Player d){float dx=d.X-Controlled.X,dz=d.Z-Controlled.Z;float px=d.OldX-Controlled.OldX,pz=d.OldZ-Controlled.OldZ;float vx=dx-px,vz=dz-pz,den=vx*vx+vz*vz;float t=den>.0001f?Clamp(-(px*vx+pz*vz)/den,0,1):1;float distance=Distance(px+vx*t,pz+vz*t,0,0);if(distance>1.12f||d.Blocked>0||d.BlockPartner>=0||d.KnockTime>0)return false;
  if(Controlled.Pose=="truck"&&skillTime>0&&d.Z>=Controlled.Z-.5f&&d.Pose!="stumble"){int nearby=0;foreach(Player other in Players)if(other.Team==1&&other!=d&&Distance(other.X,other.Z,Controlled.X,Controlled.Z)<2.4f)nearby++;float chance=Clamp(.42f+(Controlled.Power-d.Power)*.012f-(Sprint?.08f:0)-nearby*.25f,.08f,.7f);if(random.NextDouble()<chance){TruckImpact(d,true);return false;}TruckImpact(d,false);return true;}
  if(ClearHurdle(d))return false;DangerousLanding=HurdleRiskWindow>0;
  // A juke only works by creating separation. Spin can beat one isolated reach tackle.
  if(Controlled.Pose=="spin"&&skillTime>.08f&&random.NextDouble()<.36){d.Blocked=.35f;skillTime=0;return false;}
  return true;
 }
 void StartKickAnimation(){Cue("kick");kickDistance=kickPAT?33:117-Spot;kickAccuracy=(KickMeter-.5f)/Math.Max(.055f,KickTolerance);KickGood=Math.Abs(kickAccuracy)<1&&kickDistance<=64;kickPoints=kickPAT?1:3;KickAnimating=true;KickProgress=0;kickResolved=false;foreach(Player p in Players){p.HasBall=false;p.VX=p.VZ=0;}float origin=kickPAT?83:Spot-7;Controlled.X=-1;Controlled.Z=origin-1;Controlled.Role="K";Controlled.Pose="kick";Controlled.PoseTime=.5f;KickBallX=0;KickBallZ=origin;KickBallHeight=.2f;Message="KICK AWAY";}
 void AnimateKick(float dt){KickProgress+=dt;float t=Clamp((KickProgress-.35f)/1.65f,0,1);float origin=kickPAT?83:Spot-7;float length=kickDistance<=64?FieldGeometry.EndLine+3-origin:Math.Min(FieldGeometry.EndLine-3-origin,57);KickBallZ=origin+length*t;KickBallX=kickAccuracy*(KickGood?2.2f:4.5f)*t;KickBallHeight=kickDistance<=64?.2f+5f*t+(float)Math.Sin(t*Math.PI)*5:.2f+(float)Math.Sin(t*Math.PI)*7;BallX=KickBallX;BallZ=KickBallZ;BallHeight=KickBallHeight;
  if(KickProgress<.5f){Controlled.Pose="kick";Controlled.PoseTime=.15f;}if(KickProgress<2.15f||kickResolved)return;kickResolved=true;KickAnimating=false;if(!kickPAT)ApplyRunoff();if(KickGood){HomeScore+=kickPoints;Cue(kickPAT?"crowd_first":"score");}else Cue("crowd_groan");string result=KickGood?(kickPAT?"EXTRA POINT GOOD":"FIELD GOAL GOOD"):(kickPAT?"EXTRA POINT MISSED":"FIELD GOAL MISSED");if(kickPAT||KickGood)AfterUserScore(result);else BeginOpponent(result);
 }
 public bool SimDrive(){if(Practice||State!=Phase.PreSnap||conversion||OnsideAttempt||OnsideRulesPending){Message="SIM DRIVE: CHOOSE BETWEEN PLAYS, OUTSIDE A CONVERSION";return false;}SimulatedDrives++;Charging=false;int attack=Franchise==null?76:Franchise.Rating(Franchise.UserTeam,false);int defense=Franchise==null?76:Franchise.Rating(OpponentTeam,true);float chance=Clamp(.32f+(attack-defense)*.009f+(Spot-20)*.003f+Passing*.012f,.12f,.75f);double roll=random.NextDouble();Clock=Math.Max(0,Clock-(12+random.Next(18)));string result;
  if(roll<chance){HomeScore+=7;Touchdowns++;TotalYards+=100-Spot;result="SIM DRIVE / TOUCHDOWN + PAT";Cue("score");}else if(roll<chance+.22f){HomeScore+=3;TotalYards+=Math.Max(0,72-Spot);result="SIM DRIVE / FIELD GOAL";Cue("crowd_first");}else if(roll>.9){result="SIM DRIVE / TURNOVER";}else{TotalYards+=Math.Max(0,Math.Min(22,100-Spot));result="SIM DRIVE / PUNT";}if(result.Contains("TOUCHDOWN")||result.Contains("FIELD GOAL"))AfterUserScore(result);else BeginOpponent(result);return true;
 }
 public void SimRest(){if(Practice||State==Phase.GameOver)return;SimulatedDrives++;int possessions=Math.Max(1,(int)((4-Quarter)*60+Clock)/24);for(int i=0;i<possessions;i++){float home=.36f+Passing*.016f+Running*.01f;float away=Clamp(.4f+Difficulty*.035f-Defense*.025f,.12f,.65f);double a=random.NextDouble(),b=random.NextDouble();HomeScore+=a<home?7:a<home+.2?3:0;AwayScore+=b<away?7:b<away+.2?3:0;}if(Franchise!=null&&random.NextDouble()<.16-Conditioning*.025){ClubPlayer hurt=Franchise.Starter(Franchise.UserTeam,random.Next(22));if(hurt!=null&&hurt.InjuryWeeks==0&&!MatchInjuries.Contains(hurt.Id))MatchInjuries.Add(hurt.Id);}Quarter=4;Clock=0;Charging=false;KickAnimating=false;State=Phase.GameOver;Cue("whistle");Cue(HomeScore>AwayScore?"crowd_cheer":HomeScore<AwayScore?"crowd_groan":"crowd_first");Message="FINAL / SIMULATED REMAINDER";Log(Message+" "+HomeScore+" - "+AwayScore);}
}
}
