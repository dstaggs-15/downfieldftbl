using System;
using System.Collections.Generic;
namespace Downfield {
public class RetiredPlayer {public int Id,Team,Age,Season,OVR;public string Name="",Role="";}
public class DraftSelection {public int Pick,Round,Team,PlayerId,OVR;public string Name="",Role="";}
public partial class Franchise {
 public int LeagueDevelopmentVersion;public bool DraftActive;public int DraftPick;
 public static int ContractSalary(int ovr){return 2+Math.Max(0,ovr-70)/4+Math.Max(0,ovr-85)/5;}
 public List<int> DraftOrder=new List<int>();
 public List<ClubPlayer> Prospects=new List<ClubPlayer>();
 public List<DraftSelection> DraftResults=new List<DraftSelection>();
 public List<RetiredPlayer> Retired=new List<RetiredPlayer>();
 public void EnsureLeagueDevelopment(){
  foreach(ClubPlayer p in Players){if(p.Potential<p.OVR)p.Potential=Math.Min(99,p.OVR+5+(p.Id*7%15));if(p.Team==-1)p.Salary=ContractSalary(p.OVR);}
  if(LeagueDevelopmentVersion>0)return;
  foreach(LeagueTeam t in Teams){t.BuildPlan=t.Id%3==0?"CONTEND":t.Id%3==1?"REBUILD":"BALANCED";if(t.Id!=UserTeam){t.CoachOff=t.Id%3;t.CoachDef=(t.Id+1)%3;t.CoachFit=(t.Id+2)%3;BusinessFor(t.Id).Cash=48+t.Id%4*12;}}
  LeagueDevelopmentVersion=1;
 }
 void BestLineup(int team){
  List<ClubPlayer> squad=Squad(team);foreach(ClubPlayer p in squad)p.Slot=-1;
  for(int slot=0;slot<22;slot++){ClubPlayer best=null;foreach(ClubPlayer p in squad)if(p.Role==Roles[slot]&&p.Slot<0&&(best==null||p.OVR>best.OVR))best=p;if(best!=null)best.Slot=slot;}
 }
 int WeakestSlot(int team,string role){int slot=-1,value=100;for(int n=0;n<22;n++)if(Roles[n]==role){ClubPlayer p=Starter(team,n);int ovr=p==null?0:p.OVR;if(ovr<value){value=ovr;slot=n;}}return slot;}
 void CpuRecruit(int team,Random r){
  ClubBusiness bank=BusinessFor(team);LeagueTeam club=Teams[team];ClubPlayer best=null;float score=0;
  foreach(ClubPlayer p in FreeAgents()){int slot=WeakestSlot(team,p.Role);ClubPlayer old=Starter(team,slot);int gain=p.OVR-(old==null?65:old.OVR);float value=gain+(club.BuildPlan=="REBUILD"?(27-p.Age)*.8f:0);
   if(p.InjuryWeeks>0||gain<2||value<=score||bank.Cash<p.SigningCost+8||Payroll(team)+p.Salary>Cap)continue;
   if(Squad(team).Count>=MaxRoster&&!Squad(team).Exists(x=>x.Slot<0&&x.OVR<p.OVR))continue;
   best=p;score=value;
  }
  if(best==null)return;
  if(Squad(team).Count>=MaxRoster){List<ClubPlayer> bench=Squad(team).FindAll(p=>p.Slot<0);bench.Sort((a,b)=>a.OVR.CompareTo(b.OVR));bench[0].Team=-1;}
  bank.Cash-=best.SigningCost;best.Team=team;best.Years=3;best.Slot=-1;BestLineup(team);
  Log(Teams[team].Mascot+" signed "+best.Name+" / "+best.Role+" "+best.OVR+" OVR.");
 }
 void CpuWeek(){string previousNotice=Notice;EnsureLeagueDevelopment();Random r=new Random(Season*9337+Round*313);
  // Each club has its own money, coaching and players. No scaling to the user's roster.
  List<int> order=new List<int>();for(int t=0;t<12;t++)if(t!=UserTeam)order.Add(t);for(int n=order.Count-1;n>0;n--){int j=r.Next(n+1),tmp=order[n];order[n]=order[j];order[j]=tmp;}
  foreach(int team in order){Fixture g=Games.Find(x=>x.Round==Round&&x.Involves(team));if(g==null)continue;LeagueTeam club=Teams[team];ClubBusiness b=BusinessFor(team);bool win=g.Winner==team;
   SettleClubAccount(g,team,g.Home==team?g.HomeScore:g.AwayScore,g.Home==team?g.AwayScore:g.HomeScore);
   b.Fans=Math.Max(20,Math.Min(95,b.Fans+(win?3:-3)));b.LossStreak=win?0:b.LossStreak+1;
   foreach(ClubPlayer p in Squad(team))if(p.InjuryWeeks==0){p.XP+=(p.Slot>=0?2:1)+(r.Next(4)<club.CoachFit?1:0);if(p.OVR<p.Potential)Develop(p);p.Morale=Math.Max(30,Math.Min(100,p.Morale+(win?2:-2)));}
   if(Round<9)CpuRecruit(team,r);BestLineup(team);
   if(Round==4&&club.MedicalStaff<3&&b.Cash>=40){club.MedicalStaff++;b.Cash-=MedicalHireCost(club.MedicalStaff);}
   if(Round==4&&b.Cash>=22){int unit=r.Next(3);if(unit==0&&club.CoachOff<3){club.CoachOff++;b.Cash-=15;}else if(unit==1&&club.CoachDef<3){club.CoachDef++;b.Cash-=15;}else if(club.CoachFit<3){club.CoachFit++;b.Cash-=15;}}
  }
  if(Round==5)CpuTrade(r);
  if(Round<9){string[] roles={"QB","RB","WR","TE","OL","DL","CB","LB","NB","S"};for(int n=0;n<2;n++)Players.Add(Generate(r,roles[r.Next(10)],72+r.Next(22),-1));}Notice=previousNotice;
 }
 void CpuTrade(Random r){foreach(LeagueTeam rebuild in Teams)if(rebuild.Id!=UserTeam&&rebuild.BuildPlan=="REBUILD"){
  ClubPlayer veteran=Squad(rebuild.Id).Find(p=>p.Age>=29&&p.OVR>=82);if(veteran==null)continue;
  foreach(LeagueTeam contender in Teams)if(contender.Id!=UserTeam&&contender.Id!=rebuild.Id&&contender.BuildPlan=="CONTEND"){
   ClubPlayer youth=Squad(contender.Id).Find(p=>p.Role==veteran.Role&&p.Age<=25&&p.OVR<veteran.OVR&&p.Potential>=veteran.OVR);
   if(youth==null||Payroll(contender.Id)-youth.Salary+veteran.Salary>Cap||Payroll(rebuild.Id)-veteran.Salary+youth.Salary>Cap)continue;
   int target=contender.Id;veteran.Team=target;youth.Team=rebuild.Id;BestLineup(target);BestLineup(rebuild.Id);Log(rebuild.Mascot+" traded veteran "+veteran.Name+" for "+youth.Name+".");break;
  }
 }}
 void PrepareLeagueOffseason(){EnsureLeagueDevelopment();Random r=new Random(Season*7927);List<Standing> table=Table();DraftOrder.Clear();for(int n=11;n>=0;n--)DraftOrder.Add(table[n].Team);Fixture final=Games.Find(g=>g.Round==13);if(final!=null){DraftOrder.Remove(final.Winner);DraftOrder.Add(final.Winner);}
  foreach(LeagueTeam t in Teams)if(t.Id!=UserTeam){int rank=table.FindIndex(s=>s.Team==t.Id);t.BuildPlan=rank<4?"CONTEND":rank>=8?"REBUILD":"BALANCED";ClubBusiness b=BusinessFor(t.Id);b.Cash+=24;
   if(rank>=8){t.CoachOff=r.Next(4);t.CoachDef=r.Next(4);t.CoachFit=r.Next(3);Log(t.Mascot+" reset their staff and are rebuilding.");}
   foreach(ClubPlayer p in Squad(t.Id))if(p.Years<=1&&p.OVR>=79&&p.Age<32&&b.Cash>=10&&Payroll(t.Id)-p.Salary+ContractSalary(p.OVR)<=Cap&&r.Next(100)<(t.BuildPlan=="CONTEND"?85:60)){b.Cash-=3+(p.OVR-70)/3;p.Salary=ContractSalary(p.OVR);p.Years+=2;}
  }
 }
 void AgeAndRetire(){Random r=new Random(Season*17923);foreach(ClubPlayer p in new List<ClubPlayer>(Players))if(p.Team>=0){p.Age++;p.Years--;p.XP=0;p.InjuryWeeks=0;p.InjuryType="";
   bool retire=p.Age>=38||p.Age>=32&&r.Next(100)<(p.Age-31)*13+(p.OVR<78?10:0);
   if(retire){Retired.Add(new RetiredPlayer{Id=p.Id,Team=p.Team,Name=p.Name,Role=p.Role,Age=p.Age,OVR=p.OVR,Season=Season-1});Log(p.Name+" retired at "+p.Age+" / "+Teams[p.Team].Mascot+".");Players.Remove(p);continue;}
   if(p.Age>29){int decline=p.Age>33?2:1;p.OVR=Math.Max(70,p.OVR-decline);p.Speed=Math.Max(70,p.Speed-decline);p.Hands=Math.Max(70,p.Hands-(p.Age>32?1:0));p.Power=Math.Max(70,p.Power-1);}
  }
 }
 void FillVacancies(){foreach(LeagueTeam team in Teams)for(int slot=0;slot<22;slot++)if(Starter(team.Id,slot)==null){List<ClubPlayer> reserves=Squad(team.Id).FindAll(reserve=>reserve.Role==Roles[slot]&&reserve.Slot<0);reserves.Sort((a,b)=>b.OVR.CompareTo(a.OVR));ClubPlayer p=reserves.Count>0?reserves[0]:Generate(new Random(Season*1171+team.Id*29+slot),Roles[slot],70,team.Id);if(reserves.Count==0)Players.Add(p);p.Slot=slot;}}
 void BeginDraft(){DraftActive=true;DraftPick=0;Prospects.Clear();DraftResults.Clear();Random r=new Random(Season*4613);string[] roles={"QB","RB","WR","TE","OL","DL","CB","LB","NB","S"};for(int n=0;n<60;n++){ClubPlayer p=Generate(r,roles[n%10],70+r.Next(19),-2);p.Age=20+r.Next(3);p.Years=3;p.Salary=ContractSalary(p.OVR);p.Potential=Math.Min(99,p.OVR+7+r.Next(13));Prospects.Add(p);}Prospects.Sort((a,b)=>b.OVR.CompareTo(a.OVR));CpuDraftUntilUser();}
 public int DraftTeam {get{return DraftActive&&DraftPick<36?DraftOrder[DraftPick%12]:-1;}}
 public int DraftRound {get{return DraftPick/12+1;}}
 public bool CanDraft(ClubPlayer p,int team){if(p==null||!Prospects.Contains(p))return false;int pay=Payroll(team)+p.Salary;List<ClubPlayer> bench=Squad(team).FindAll(x=>x.Slot<0);foreach(ClubPlayer reserve in bench)pay-=reserve.Salary;return pay<=Cap&&(Squad(team).Count<MaxRoster||bench.Count>0);}
 public string DraftRosterNote(ClubPlayer p){int count=Squad(UserTeam).Count,pay=Payroll(UserTeam)+p.Salary;List<ClubPlayer> bench=Squad(UserTeam).FindAll(x=>x.Slot<0);bench.Sort((a,b)=>a.OVR.CompareTo(b.OVR));string note="";foreach(ClubPlayer reserve in bench){if(count<MaxRoster&&pay<=Cap)break;note+=(note.Length==0?" / WAIVES ":", ")+reserve.Name;count--;pay-=reserve.Salary;}return note;}
 bool DraftOne(ClubPlayer p){int team=DraftTeam;if(team<0||!CanDraft(p,team))return false;
  // A full or capped club waives its lowest reserve to make a real roster spot.
  List<ClubPlayer> bench=Squad(team).FindAll(x=>x.Slot<0);bench.Sort((a,b)=>a.OVR.CompareTo(b.OVR));while((Squad(team).Count>=MaxRoster||Payroll(team)+p.Salary>Cap)&&bench.Count>0){ClubPlayer cut=bench[0];bench.RemoveAt(0);cut.Team=-1;cut.Slot=-1;if(team==UserTeam)Log("Draft roster space: waived "+cut.Name+".");}
  p.Team=team;p.Slot=-1;Players.Add(p);Prospects.Remove(p);DraftResults.Add(new DraftSelection{Pick=DraftPick+1,Round=DraftRound,Team=team,PlayerId=p.Id,Name=p.Name,Role=p.Role,OVR=p.OVR});DraftPick++;if(team!=UserTeam)BestLineup(team);if(DraftPick>=36){DraftActive=false;Log("Draft complete. Three new rookies per club. Set your starters in Roster.");}return true;
 }
 ClubPlayer BestProspect(int team){ClubPlayer best=null;float score=-999;foreach(ClubPlayer p in Prospects)if(CanDraft(p,team)){ClubPlayer old=Starter(team,WeakestSlot(team,p.Role));float value=p.OVR-(old==null?65:old.OVR)+(p.Potential-80)*.35f+p.OVR*.14f;if(value>score){score=value;best=p;}}return best;}
 public bool DraftPlayer(int id){if(!DraftActive||DraftTeam!=UserTeam)return false;ClubPlayer p=Prospects.Find(x=>x.Id==id);if(!DraftOne(p)){Notice="Not enough cap room. Release a reserve before this pick.";return false;}CpuDraftUntilUser();Save();return true;}
 void CpuDraftUntilUser(){while(DraftActive&&DraftTeam!=UserTeam){ClubPlayer p=BestProspect(DraftTeam);if(p==null){Notice="Draft paused: a club needs cap room.";return;}DraftOne(p);}}
 public void AutoDraft(){int guard=0;while(DraftActive&&guard++<36){ClubPlayer p=BestProspect(DraftTeam);if(p==null)break;DraftOne(p);}Save();}
}
}

