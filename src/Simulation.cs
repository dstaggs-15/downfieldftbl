using System;
using System.Collections.Generic;
namespace Downfield {
public enum Phase { PreSnap, Live, Flight, Dead, Conversion, Opponent, Kick, Loose, Halftime, GameOver }
public class Player {
 public float X,Z,StartX,StartZ,Speed,VX,VZ,Step,Blocked,PoseTime,Stamina=1,OldX,OldZ,Engagement;
 public float TackleCooldown,LungeTime,LungeX,LungeZ;public int LastBlocker=-1;public int BlockPartner=-1,RushAssignment,RushStage;public float BlockLimit,ShedTime,ShedSide,KnockTime,KnockDuration,KnockVX,KnockVZ;public int ClubId=-1;public int Primary=-1,Secondary=-1;public float ThinkTime,AimX,AimZ;public float PlantTime,FacingX,FacingZ=1;public int Uniform;public int OVR=70,Hands=70,Power=70;public string Name="";public int Team,Number,Mark=-1;public string Role,Pose="idle";public bool HasBall,Active=true;
 public Player(float x,float z,int team,string role,int number,float speed){X=StartX=OldX=x;Z=StartZ=OldZ=z;Team=team;Role=role;Number=number;Speed=speed;}
}
public partial class Simulation {
 public Roster Roster;public List<Player> Players=new List<Player>();public Phase State;public Player Controlled;
 public float BallX,BallZ,BallHeight,Spot,FirstDown,Clock,Charge,LeadX,LeadZ,KickMeter,TotalYards;
 public int Down,Quarter,HomeScore,AwayScore,Playbook,Passing,Running,Defense,Difficulty=1,Completions,Attempts,Rushes,Touchdowns,Interceptions,Drops,Sacks,Fumbles,Timeouts=3;
 public bool Charging,Sprint,Practice,RunPlay;public string Message="",LastPlay="",Coverage="",OpponentName="IRONWORKS";
 public readonly List<string> DriveLog=new List<string>();public readonly string[] PlayNames={"SLANT ATTACK","SIDELINE OUTS","FOUR VERTICALS","FLOOD RIGHT","RB SCREEN","DEEP POSTS","HB DIVE","INSIDE ZONE","POWER RIGHT","COUNTER LEFT","SWEEP RIGHT","STRETCH LEFT","HB DRAW","OFF TACKLE","MESH CROSS","CURL FLATS","HB WHEEL","CORNER SHOT","PLAY ACTION","TEXAS ANGLE","HB TOSS","JET SWEEP","READ OPTION","QB DRAW"};
 readonly Random random;readonly int[] targets={2,3,4,1,10};int selected,coverageId,previousCoverage=-1;float playTime,deadTime,passTime,passDuration,fromX,fromZ,toX,toZ,opponentStart=20,handoffTime=-1,kickTime,catchGrace,burst,looseTime;bool conversion,kickPAT,passLob,lateral,usedForwardPass;Player target;
 public Simulation():this(Environment.TickCount){}public Simulation(int seed){random=new Random(seed);NewGame();}
 public bool IsConversion {get{return conversion;}}public bool HandoffPending {get{return handoffTime>=0;}}
 public bool CanThrow {get{return !OptionLive&&!DesignedQBRun&&!usedForwardPass&&State==Phase.Live&&!HandoffPending&&Controlled.Role=="QB"&&Controlled.HasBall&&Controlled.Z<=Spot;}}
 public float KickTolerance {get{float distance=kickPAT?33:117-Spot;return kickPAT?.26f:Clamp(.27f-(distance-25)*.0035f,.055f,.28f);}}
 public float FlightTargetX {get{return toX;}}public float FlightTargetZ {get{return toZ;}}
 public float Pressure {get{float n=0;if(Controlled==null)return 0;foreach(Player p in Players)if(p.Team==1&&p.Blocked<=0&&p.BlockPartner<0&&p.KnockTime<=0)n+=Math.Max(0,1-Distance(p.X,p.Z,Controlled.X,Controlled.Z)/6);return Math.Min(3,n);}}
 public int TargetIndex {get{return selected;}set{selected=((value%5)+5)%5;}}
 public Player Target {get{return Players.Count>10?Players[targets[selected]]:null;}}
 public Player Receiver(int n){return Players[targets[((n%5)+5)%5]];}
 public void CycleTarget(int delta){TargetIndex+=delta;}public void CycleReceiver(int delta){CycleTarget(delta);}
 public void CyclePlay(int delta){if(State==Phase.PreSnap){UseCustom=false;Playbook=(Playbook+delta+PlayNames.Length)%PlayNames.Length;RunPlay=IsRunCall(Playbook);}}
 public void NewGame(){MatchInjuries.Clear();InjuryReports.Clear();InjuryPatient=null;AmbulanceTime=0;HurdlesMade=BigHits=MissedTackles=0;OpenFieldPace=1;Box.Clear();Sounds.Clear();HalftimeCount=0;SimulatedDrives=0;HomeScore=AwayScore=0;Quarter=1;Clock=60;Spot=20;Down=1;FirstDown=30;Completions=Attempts=Rushes=Touchdowns=Interceptions=Drops=Sacks=Fumbles=0;TotalYards=0;Timeouts=3;conversion=false;DriveLog.Clear();Setup();}
 void Setup(){throwingAway=false;runoffCharged=false;LastRunoff=0;DangerousLanding=false;HurdleRiskWindow=0;InjuryPatient=null;caughtReceiver=null;OptionGiven=false;moveCooldown=skillTime=0;KickAnimating=false;
  Players.Clear();playTime=0;Charge=0;Charging=false;BallHeight=0;catchGrace=0;handoffTime=-1;burst=0;LeadX=LeadZ=0;lateral=false;usedForwardPass=false;State=Phase.PreSnap;
  Controlled=Add(0,Spot-4,0,"QB",7,6.8f);Controlled.HasBall=true;
  Add(-2,Spot-6.5f,0,"RB",22,8);Add(-19,Spot,0,"WR",81,8.2f);Add(19,Spot,0,"WR",88,8);Add(7,Spot-.5f,0,"TE",85,7.2f);
  for(int i=0;i<5;i++)Add((i-2)*2.7f,Spot-.5f,0,"OL",60+i,5.3f);
  Add(-10,Spot-.8f,0,"WR",11,7.8f);
  if(!Practice){for(int i=0;i<4;i++)Add((i-1.5f)*3.8f,Spot+1.3f,1,"DL",90+i,5.0f+Difficulty*.25f);Add(-19,Spot+5,1,"CB",24,7.6f);Add(19,Spot+5,1,"CB",28,7.6f);Add(5,Spot+5,1,"LB",54,6.9f);Add(-5,Spot+5,1,"LB",55,7);Add(-10,Spot+6,1,"NB",21,7.5f);Add(-9,Spot+16,1,"S",31,7.7f);Add(10,Spot+18,1,"S",32,7.7f);}
  DefensiveSetup();
  if(Roster!=null)foreach(Player p in Players)if(p.Team==0){p.Uniform=Roster.Team;Recruit c=Roster.Best(p.Role);if(c!=null){p.Name=c.Name;p.OVR=c.OVR;p.Hands=c.Hands;p.Power=c.Power;p.Speed+=(c.Speed-70)*.055f;}}if(Franchise!=null){Franchise.ActiveOpponent=OpponentTeam;Franchise.Dress(Players);}BallX=Controlled.X;BallZ=Controlled.Z;Message=conversion?"TWO POINT TRY":Practice?"PRACTICE - FIVE RECEIVER ICONS":"CHOOSE A PLAY AND SNAP";
 }
 Player Add(float x,float z,int t,string role,int number,float speed){Player p=new Player(x,z,t,role,number,speed);Players.Add(p);return p;}
 public void Snap(){if(State!=Phase.PreSnap)return;State=Phase.Live;Message="READ THE DEFENSE";Cue("snap");callsUsed[Math.Max(0,Math.Min(callsUsed.Length-1,Playbook))]++;if(RunPlay&&!OptionLive&&!DesignedQBRun)Handoff();if(OptionLive||DesignedQBRun)Rushes++;}
 public void BeginPass(){BeginPass(TargetIndex);}public void BeginPass(int index){if(CanThrow){TargetIndex=index;Charging=true;Charge=0;}}
 public void ReleasePass(){
  if(!Charging)return;Charging=false;if(State!=Phase.Live||Controlled.Role!="QB"||!Controlled.HasBall||Controlled.Z>Spot){Message="PAST THE LINE - RUN OR LATERAL";return;}
  target=Target;passLob=Charge<.22f;lateral=false;float range=Distance(Controlled.X,Controlled.Z,target.X,target.Z);passDuration=Clamp(range/((passLob?20:34)+Passing),.32f,2.1f);
  Player forecast=new Player(target.StartX,target.StartZ,0,target.Role,target.Number,target.Speed);forecast.X=target.X;forecast.Z=target.Z;for(float t=0;t<passDuration;t+=.02f)RouteMove(forecast,playTime+t,.02f);
  float spread=(.45f+range*.035f+Pressure*1.0f+(Math.Abs(Controlled.VX)+Math.Abs(Controlled.VZ))*.12f)/(1+Passing*.12f+(Controlled.Power-70)*.035f);
  toX=forecast.X+LeadX+Clamp(PlacementX,-1.75f,1.75f)+Signed()*spread;toZ=forecast.Z+LeadZ+Clamp(PlacementZ,-2,2)+Signed()*spread;usedForwardPass=true;StartFlight(passLob?"LOB":"BULLET");RecordAttempt();Cue("throw");Attempts++;Charge=passLob?0:1;
 }
 void StartFlight(string description){fromX=Controlled.X;fromZ=Controlled.Z;BallX=fromX;BallZ=fromZ;BallHeight=1.5f;passTime=0;Controlled.HasBall=false;Controlled.Pose=lateral?"lateral":"throw";Controlled.PoseTime=.35f;State=Phase.Flight;Message=description;}
 public void Handoff(){
  if(State==Phase.PreSnap){bool wasRun=RunPlay;RunPlay=false;Snap();RunPlay=wasRun;}if(State!=Phase.Live||Controlled.Role!="QB"||!Controlled.HasBall||HandoffPending)return;
  if(Controlled.Z>Spot+1){Message="PAST THE LINE - USE LB TO LATERAL";return;}Charging=false;handoffTime=0;Players[HandoffReceiver].X=Controlled.X-2;Players[HandoffReceiver].Z=Controlled.Z-2;Controlled.Pose="handoff";Controlled.PoseTime=.5f;Message="HANDOFF TO #"+Players[HandoffReceiver].Number;
 }
 public bool Lateral(){
  if(State!=Phase.Live||!Controlled.HasBall||HandoffPending)return false;
  target=null;float best=18;foreach(Player p in Players){if(p.Team!=0||p==Controlled||p.Role=="OL"||p.Z>=Controlled.Z-.6f||!p.Active)continue;float d=Distance(p.X,p.Z,Controlled.X,Controlled.Z);if(d<best){best=d;target=p;}}
  lateral=true;passLob=false;Charging=false;toX=target!=null?target.X+target.VX*.2f:Controlled.X+(Controlled.X>0?-3:3);toZ=target!=null?target.Z:Controlled.Z-4;
  toX+=Signed()*.7f;toZ=Math.Min(Controlled.Z-.75f,toZ+Signed()*.5f);passDuration=Clamp(Distance(Controlled.X,Controlled.Z,toX,toZ)/19,.25f,.7f);StartFlight(target==null?"BLIND BACKWARD TOSS!":"LATERAL TO #"+target.Number);return true;
 }
 public void Dive(){if(State!=Phase.Live||!Controlled.HasBall||HandoffPending)return;Controlled.Pose="dive";Controlled.PoseTime=.9f;Controlled.Z+=1.8f;if(Controlled.Z>=100)ScoreTouchdown();else EndPlay(Controlled.Z,"DIVE: "+Gain()+" YDS",false);}
 float moveCooldown,skillX,skillZ,skillTime;public void SkillMove(){SkillMove(0,-1);}public void SkillMove(float x,float z){if(State!=Phase.Live||!Controlled.HasBall||HandoffPending||Controlled.Stamina<.2f||moveCooldown>0)return;Controlled.Stamina-=.2f;moveCooldown=.82f;skillTime=MoveDuration;skillX=Math.Abs(x)>.4f?Math.Sign(x)*15:0;skillZ=Math.Abs(x)>.4f?2:z>.4f?7:3;Controlled.Pose=Math.Abs(x)>.4f?"juke":z>.4f?"truck":"spin";Controlled.PoseTime=MoveDuration;Message=Math.Abs(x)>.4f?(x<0?"JUKE LEFT":"JUKE RIGHT"):z>.4f?"TRUCK":"SPIN";}
 public void Timeout(){if(Practice||Timeouts==0||State!=Phase.PreSnap)return;Timeouts--;Message="TIMEOUT - "+Timeouts+" REMAINING";Log(Message);}
 public void Punt(){if(State!=Phase.PreSnap||conversion)return;ApplyRunoff();opponentStart=Clamp(100-(Spot+38+random.Next(12)),5,80);BeginOpponent("PUNT: OPPONENT AT "+(int)opponentStart,true);}
 public void FieldGoal(){if(State!=Phase.PreSnap||conversion)return;BeginKick(false);}
 void BeginKick(bool pat){kickPAT=pat;kickTime=0;KickMeter=0;State=Phase.Kick;Message=pat?"EXTRA POINT - STOP THE METER":"FIELD GOAL - STOP THE METER";}
 public void StopKick(){if(State!=Phase.Kick||KickAnimating)return;StartKickAnimation();}
 void UpdateStep(float dt,float mx,float mz){
  dt=Clamp(dt,0,.05f);HurdleRiskWindow=Math.Max(0,HurdleRiskWindow-dt);HitFlash=Math.Max(0,HitFlash-dt);moveCooldown=Math.Max(0,moveCooldown-dt);foreach(Player p in Players){p.OldX=p.X;p.OldZ=p.Z;p.PlantTime=Math.Max(0,p.PlantTime-dt);p.PoseTime=Math.Max(0,p.PoseTime-dt);if(p.PoseTime<=0)p.Pose="idle";p.Blocked=Math.Max(0,p.Blocked-dt);p.ShedTime=Math.Max(0,p.ShedTime-dt);}UpdateKnockbacks(dt);if(InjuryPatient!=null&&!AmbulanceLoaded){InjuryPatient.KnockTime=Math.Max(.08f,InjuryPatient.KnockTime);InjuryPatient.Pose="knockdown";}if(AmbulanceActive){UpdateAmbulance(dt);return;}
  if(State==Phase.Kick){if(KickAnimating){AnimateKick(dt);return;}kickTime+=dt;KickMeter=(float)((Math.Sin(kickTime*3.6f-1.57)+1)*.5);return;}
  if(State==Phase.Opponent){deadTime-=dt;if(deadTime<=0)ResolveOpponent();return;}
  if(State==Phase.Loose){UpdateLoose(dt,mx,mz);Animate(dt);return;}
  if(State!=Phase.Live&&State!=Phase.Flight)return;
  playTime+=dt;if(!Practice)Clock=Math.Max(0,Clock-dt/OpenFieldPace);if(Charging)Charge+=dt;catchGrace=Math.Max(0,catchGrace-dt);
  if(HandoffPending){handoffTime+=dt;Player rb=Players[HandoffReceiver];Move(rb,Controlled.X,Controlled.Z+1,8,dt);BallX=Controlled.X-.5f;BallZ=Controlled.Z;BallHeight=1;
   if(handoffTime>=(Playbook==12?.8f:.38f)){Controlled.HasBall=false;Controlled=rb;rb.HasBall=true;rb.Pose="catch";rb.PoseTime=.2f;handoffTime=-1;if(!OptionGiven)Rushes++;catchGrace=.25f;burst=.22f;Message="#"+rb.Number+" HAS IT - RUN";}
  }else if(State==Phase.Live)MoveControlled(dt,mx,mz);
  foreach(Player p in Players){if(p==Controlled||!p.Active||p.KnockTime>0)continue;if(p.Team==0){if(p.Role=="OL"){Block(p,dt);continue;}if(p.Role=="QB")continue;if(p==Players[HandoffReceiver]&&HandoffPending)continue;
    if(State==Phase.Live&&Controlled.HasBall&&(Controlled.Role!="QB"||Controlled.Z>Spot)){if(p.Z>Controlled.Z-2)Block(p,dt);else Move(p,Controlled.X-3,Controlled.Z-4,p.Speed*.8f,dt);continue;}
    if(State==Phase.Flight&&p==target&&passTime>.15f&&Distance(p.X,p.Z,toX,toZ)<4.2f)Move(p,toX,toZ,p.Speed,dt);else RouteMove(p,playTime,dt);
   }else Defend(p,dt);}
  ResolveLineContacts(dt);SeparatePlayers();Animate(dt);if(State==Phase.Flight){UpdateFlight(dt);return;}BallX=Controlled.X;BallZ=Controlled.Z;BallHeight=1.1f;
  if(Controlled.Z>=100){ScoreTouchdown();return;}if(Math.Abs(Controlled.X)>26.65f){EndPlay(Controlled.Z,"OUT: "+Gain()+" YDS",false);return;}
  if(!HandoffPending&&catchGrace<=0)foreach(Player d in Players)if(d.Team==1&&Contact(d)){RegisterHit(d);d.Pose="tackle";d.PoseTime=.7f;if(Controlled.KnockTime<=0){Controlled.Pose="tackle";Controlled.PoseTime=1;}
    if(Controlled.Role=="QB"&&Controlled.Z<=Spot)Sacks++;if(random.NextDouble()<(DangerousLanding?.38:Sprint?.045:.015)){MakeLoose("FUMBLE ON CONTACT!");return;}EndPlay(Controlled.Z,Controlled.Role=="QB"&&Controlled.Z<=Spot?"SACKED":"TACKLED: "+Gain()+" YDS",false);return;}
  if(playTime>22)EndPlay(Controlled.Z,"WHISTLE",false);
 }
 void MoveControlled(float dt,float mx,float mz){if(Controlled.KnockTime>0||!Controlled.Active)return;float l=(float)Math.Sqrt(mx*mx+mz*mz);if(l>1){mx/=l;mz/=l;}if(burst>0){burst-=dt;if(l<.05f){if(RunPlay&&Playbook>=6){float[] lane=RunPoint(1);mx=lane[0]/24;mz=.7f;}else mz=.7f;}}
  float speed=Controlled.Speed+Running*.2f;if(Charging)speed*=.76f;if(Sprint&&Controlled.Stamina>.08f){speed*=1.17f;Controlled.Stamina=Math.Max(0,Controlled.Stamina-dt/OpenFieldPace*(.17f-Conditioning*.025f));}else Controlled.Stamina=Math.Min(1,Controlled.Stamina+dt*.08f);
  if(mz<0)speed*=.67f;if(Controlled.Pose=="truck")speed*=.72f;
  float beforeX=Controlled.X,beforeZ=Controlled.Z;GroundStep(Controlled,mx,mz,speed,dt);
  if(skillTime>0){skillTime=Math.Max(0,skillTime-dt);if(Controlled.Pose!="hurdle"){float drive=(float)Math.Sin(Math.PI*(1-skillTime/MoveDuration));Controlled.VX=mx*speed*.35f+skillX*drive;Controlled.VZ=mz*speed*.8f+skillZ*drive;Controlled.X=beforeX+Controlled.VX*dt;Controlled.Z=beforeZ+Controlled.VZ*dt;}}
 } void Animate(float dt){foreach(Player p in Players){float dx=p.X-p.OldX,dz=p.Z-p.OldZ;p.Step+=(float)Math.Sqrt(dx*dx+dz*dz);if(p!=Controlled){p.VX=dt>0?dx/dt:0;p.VZ=dt>0?dz/dt:0;}if(Math.Abs(dx)+Math.Abs(dz)>.004f&&p.PoseTime<=0)p.Pose="run";}}
 void UpdateFlight(float dt){passTime+=dt;float t=Math.Min(1,passTime/passDuration);BallX=fromX+(toX-fromX)*t;BallZ=fromZ+(toZ-fromZ)*t;BallHeight=1.1f*(1-t)+(float)Math.Sin(t*Math.PI)*(lateral?1.7f:passLob?8:2.6f);
  if(throwingAway){if(t>=1){throwingAway=false;EndPlay(Spot,"PASS THROWN AWAY",true);}return;}
  if(t>.7f&&BallHeight<2.5f){Player c=null;float best=t>=1?1.12f:.85f;foreach(Player p in Players){if(p.KnockTime>0||p.Role=="OL"||p==Players[0]&&!lateral||!p.Active||Math.Abs(p.X)>26.65f)continue;float d=Distance(p.X,p.Z,BallX,BallZ);if(d<best){best=d;c=p;}}
   if(c!=null){if(c.Team==1){c.HasBall=true;c.Pose="catch";c.PoseTime=.4f;Spot=Clamp(c.Z,1,99);if(!lateral){Interceptions++;BoxLine q=Line(Players[0]);if(q!=null)q.Picks++;}BeginOpponent(lateral?"DEFENSE RECOVERS LATERAL":"INTERCEPTED BY #"+c.Number);return;}
    int contested=0;foreach(Player d in Players)if(d.Team==1&&Distance(d.X,d.Z,c.X,c.Z)<2.4f)contested++;
    float drop=Clamp(.19f+contested*.12f+(passLob?0:.035f)+Difficulty*.012f-Passing*.008f-(c.Hands-70)*.006f,.025f,.75f);
    if(random.NextDouble()<drop){c.Pose="drop";c.PoseTime=.7f;if(lateral)MakeLoose("DROPPED LATERAL!");else{Drops++;EndPlay(Spot,"DROPPED BY #"+c.Number,true);}return;}
    Controlled=c;c.HasBall=true;c.Pose="catch";c.PoseTime=.3f;catchGrace=.17f;State=Phase.Live;if(!lateral){Completions++;RecordCatch(c);}Message=lateral?"LATERAL COMPLETE - RUN":"CAUGHT BY #"+c.Number+" - RUN";lateral=false;if(c.Z>=100)ScoreTouchdown();return;
   }
  }
  if(t>=1){if(lateral)MakeLoose("LATERAL LOOSE - LIVE BALL!");else EndPlay(Spot,Math.Abs(toX)>26.65f?"PASS OUT OF BOUNDS":"INCOMPLETE PASS",true);}
 }
 void MakeLoose(string why){DangerousLanding=false;HurdleRiskWindow=0;Cue("crowd_groan");Fumbles++;foreach(Player p in Players)p.HasBall=false;State=Phase.Loose;BallHeight=.4f;BallX+=Signed();BallZ-=1;looseTime=0;Charging=false;Message=why;}
 void UpdateLoose(float dt,float mx,float mz){looseTime+=dt;if(!Practice)Clock=Math.Max(0,Clock-dt/OpenFieldPace);BallHeight=Math.Abs((float)Math.Sin(looseTime*13))*.55f*(float)Math.Exp(-looseTime);MoveControlled(dt,mx,mz);
  foreach(Player p in Players)if(p!=Controlled)Move(p,BallX,BallZ,p.Speed,dt);
  if(Math.Abs(BallX)>26.65f){EndPlay(BallZ,"FUMBLE OUT OF BOUNDS",false);return;}
  if(looseTime>.3f){Player best=null;float radius=1.1f;foreach(Player p in Players){if(!p.Active||p.KnockTime>0)continue;float d=Distance(p.X,p.Z,BallX,BallZ);if(d<radius){best=p;radius=d;}}if(best!=null){if(best.Team==1){Spot=Clamp(BallZ,1,99);BeginOpponent("FUMBLE LOST - DEFENSE RECOVERS");}else{Controlled=best;best.HasBall=true;State=Phase.Live;catchGrace=.2f;Message="FUMBLE RECOVERED BY #"+best.Number;}return;}}
  if(looseTime>5)EndPlay(BallZ,"LOOSE BALL WHISTLED DEAD",false);
 }
 void Block(Player p,float dt){ApproachBlock(p,dt);}
 void Defend(Player p,float dt){ReadDefense(p,dt);}
 void RouteMove(Player p,float t,float dt){float[] point=GetRoutePoint(p,t);Move(p,point[0],point[1],p.Speed,dt);}
 public float[] GetRoutePoint(Player p,float t){if(!RunPlay&&State!=Phase.PreSnap&&(t>4.2f||t>1.7f&&(Math.Abs(Players[0].X)>6||Players[0].Z<Spot-8)))return ScrambleRoute(p,t);float[] custom=CustomPoint(p,t);if(custom!=null)return custom;if(OptionLive&&p.Role=="RB")return new float[]{Controlled.X+5,Controlled.Z-2};if(Playbook>=14&&Playbook<=19){float[] extra=ExtraRoute(p,t);if(extra!=null)return extra;}if(RunPlay&&IsRunCall(Playbook)&&p.Role=="RB")return RunPoint(t);if(p.Role=="RB"){if(t<.6f)return new float[]{p.StartX,p.StartZ};return new float[]{Playbook==4?-15:-11,Spot+Math.Min(7,(t-.6f)*3)};}
  float x=p.StartX,z=p.StartZ+Math.Min(45,t*p.Speed);if(Playbook==0&&t>1)x=p.StartX>0?-5:8;if(Playbook==1&&t>1.2f){x=p.StartX>0?25:-25;z=p.StartZ+12+Math.Max(0,t-2.7f)*2;}
  if(Playbook==3&&t>1){x=p.Role=="TE"?20:Clamp(p.StartX+14,-22,24);z=p.StartZ+Math.Min(p.Role=="TE"?12:34,t*p.Speed);}if(Playbook==4){x=p.StartX+(p.StartX<0?2:-2);z=p.StartZ+Math.Min(18,t*p.Speed);}if(Playbook==5&&t>1.8f)x=p.StartX>0?-7:7;return new float[]{x,z};}
 public float[] RunPoint(float t){float[] lanes={0,-3,7,-7,19,-19,2,10};int n=Math.Max(0,Math.Min(7,Playbook-6));if(Playbook>=20)return new float[]{Playbook==20?22:Playbook==21?-19:Playbook==22?9:0,Spot+Math.Min(35,t*7)};return new float[]{lanes[n]*Math.Min(1,t/1.6f),Spot+Math.Min(35,t*7)};}static void Move(Player p,float x,float z,float speed,float dt){if(p.KnockTime>0||p.BlockPartner>=0||p.Blocked>0&&p.Team==0){p.VX=p.VZ=0;return;}float dx=x-p.X,dz=z-p.Z,d=(float)Math.Sqrt(dx*dx+dz*dz);if(d<.08f){p.VX=p.VZ=0;return;}GroundStep(p,dx/d,dz/d,Math.Min(speed,d/Math.Max(.001f,dt)),dt);} float Signed(){return (float)(random.NextDouble()*2-1);}public static float Distance(float x,float z,float a,float b){return (float)Math.Sqrt((x-a)*(x-a)+(z-b)*(z-b));}static float Clamp(float n,float lo,float hi){return Math.Max(lo,Math.Min(hi,n));}string Gain(){return Math.Round(Controlled.Z-Spot).ToString("+0;-0;0");}
 void EndPlay(float z,string why,bool incomplete){if(!incomplete){RecordProduction(z,false);if(!why.StartsWith("OUT:")&&!why.StartsWith("FUMBLE OUT"))ApplyRunoff();}Cue("whistle");Charging=false;handoffTime=-1;BallHeight=0;LastPlay=why;Log(why);if(Practice){Message=why+" - PRACTICE REP COMPLETE";State=Phase.Dead;return;}if(conversion){conversion=false;BeginOpponent(why+" - TWO POINT TRY FAILED");return;}
  if(!incomplete){TotalYards+=z-Spot;Spot=Clamp(z,0,99.5f);}if(z<=0&&!incomplete){AwayScore+=2;BeginOpponent("SAFETY");return;}if(Spot>=FirstDown){Cue("crowd_first");Down=1;FirstDown=Math.Min(100,Spot+10);Message=why+" / FIRST DOWN";}else{Down++;Message=why;}if(Down>4){BeginOpponent("TURNOVER ON DOWNS");return;}State=Phase.Dead;CheckQuarter();}
 void ScoreTouchdown(){if(Practice){EndPlay(100,"TOUCHDOWN",false);return;}if(conversion){HomeScore+=2;Cue("crowd_first");conversion=false;BeginOpponent("TWO POINT CONVERSION GOOD");return;}RecordProduction(100,true);ApplyRunoff();Cue("score");HomeScore+=6;Touchdowns++;State=Phase.Conversion;Charging=false;Message="TOUCHDOWN!";Log("TOUCHDOWN");}
 public void ChooseConversion(bool two){if(State!=Phase.Conversion)return;if(two){conversion=true;Spot=98;Setup();}else BeginKick(true);}
 void Log(string s){DriveLog.Add(s);if(DriveLog.Count>8)DriveLog.RemoveAt(0);}void BeginOpponent(string why){BeginOpponent(why,false);}
 void BeginOpponent(string why,bool retainField){if(why.StartsWith("INTERCEPTED")||why.StartsWith("FUMBLE LOST")||why=="TURNOVER ON DOWNS")ApplyRunoff();if(why.StartsWith("INTERCEPTED")||why.StartsWith("FUMBLE LOST")||why=="TURNOVER ON DOWNS")Cue("crowd_groan");LastPlay=why;Log(why);Charging=false;handoffTime=-1;if(Practice){State=Phase.Dead;Message=why;return;}if(!retainField)opponentStart=why.StartsWith("INTERCEPTED")||why.StartsWith("FUMBLE LOST")||why=="TURNOVER ON DOWNS"?Clamp(100-Spot,5,95):20;
  Message=why;if(Clock<=0){CheckQuarter();if(State!=Phase.GameOver&&State!=Phase.Halftime){State=Phase.Opponent;deadTime=2.8f;}return;}State=Phase.Opponent;deadTime=2.8f;}
 void ResolveOpponent(){int roll=random.Next(100);float field=(opponentStart-20)*.4f,strength=Difficulty*5-Defense*4;if(roll<35+strength+field){AwayScore+=7;Message=OpponentName+" TOUCHDOWN";Cue("crowd_groan");}else if(roll<57+strength+field){AwayScore+=3;Message=OpponentName+" FIELD GOAL";Cue("crowd_groan");}else{Message="YOUR DEFENSE FORCES A PUNT";Cue("crowd_first");}Clock=Math.Max(0,Clock-(22+random.Next(24)));Spot=20;Down=1;FirstDown=30;State=Phase.Dead;Log(Message);CheckQuarter();}
 void CheckQuarter(){if(Clock>0)return;if(Quarter==2){StartHalftime();return;}if(Quarter<4){Quarter++;Clock=60;if(Quarter==3)Timeouts=3;Message+=" / QUARTER "+Quarter;}else{Cue("whistle");Cue(HomeScore>AwayScore?"crowd_cheer":HomeScore<AwayScore?"crowd_groan":"crowd_first");State=Phase.GameOver;Message=HomeScore>AwayScore?"FINAL - YOU WIN":HomeScore<AwayScore?"FINAL - OPPONENT WINS":"FINAL - DRAW";}}
 public void Next(){if(AmbulanceActive){SkipAmbulance();return;}if(State==Phase.Halftime){BeginSecondHalf();}else if(State==Phase.Opponent){ResolveOpponent();}else if(State==Phase.Dead){if(Practice){Spot=20;Down=1;FirstDown=30;}Setup();}else if(State==Phase.GameOver)NewGame();}
}
}
