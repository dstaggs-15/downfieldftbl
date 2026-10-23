using System;
namespace Downfield {
public partial class Franchise {
 public int EconomyVersion;
 // Salary is an annual cap charge. Cash pays a small weekly operating installment.
 public void EnsureEconomy(){if(EconomyVersion>=1)return;
  foreach(ClubPlayer p in Players){p.Salary=ContractSalary(p.OVR);p.InjuryWeeks=Math.Min(2,p.InjuryWeeks);}
  foreach(LeagueTeam t in Teams){int payroll=Payroll(t.Id);if(payroll>Cap-12){double scale=(Cap-12.0)/payroll;foreach(ClubPlayer p in Squad(t.Id))p.Salary=Math.Max(1,(int)Math.Floor(p.Salary*scale));}ClubBusiness b=BusinessFor(t.Id);b.Cash=Math.Max(40,t.Id==UserTeam?Credits:b.Cash);if(t.Id==UserTeam)Credits=b.Cash;}
  if(Fired&&DismissalReason=="The club cannot cover its running costs."){Fired=false;DismissalReason="";Business.Security=Math.Max(50,Business.Security);Business.UnhappyGames=0;}
  EconomyVersion=1;Log("Economy updated: contracts rebalanced, $40 minimum cash reserve, injury backlog reduced. Your career is preserved.");
 }
 public int OperatingCosts(int team,bool home){LeagueTeam t=Teams[team];return 4+(int)Math.Ceiling(Payroll(team)/24.0)+(int)Math.Ceiling((t.CoachOff+t.CoachDef+t.CoachFit)*.4)+(team==UserTeam?(Passing+Running+Defense)/5:0)+(home?2:0)+MedicalWeeklyCost(t.MedicalStaff);}
 int AttendanceFor(int team){ClubBusiness b=BusinessFor(team);return (int)Math.Round(Math.Min(1,.42+b.Fans*.0065)*Math.Max(.35,1-Math.Max(0,b.FoodPrice-8)*.014-Math.Max(0,b.ParkingPrice-12)*.006)*100);}
 public int ProjectedIncome(int team,bool home){ClubBusiness b=BusinessFor(team);return 18+(home?(int)Math.Round(AttendanceFor(team)/100.0*(6+b.FoodPrice*.20+b.ParkingPrice*.18)):0);}
 void SettleClubAccount(Fixture fixture,int team,int own,int other){ClubBusiness b=BusinessFor(team);if(b.LastSeason==Season&&b.LastRound==fixture.Round)return;bool home=fixture.Home==team;int income=ProjectedIncome(team,home)+(own>other?1:0),expenses=OperatingCosts(team,home);if(team==UserTeam){Credits+=income-expenses;b.Cash=Credits;}else b.Cash+=income-expenses;b.LastIncome=income;b.LastExpenses=expenses;b.LastAttendance=home?AttendanceFor(team):0;b.LastSeason=Season;b.LastRound=fixture.Round;b.LastReport="Broadcast $18 + gate $"+(income-18-(own>other?1:0))+" / costs $"+expenses+" / net "+(income>=expenses?"+$":"-$")+Math.Abs(income-expenses)+".";}
}
}
