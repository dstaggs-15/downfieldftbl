using System;
using System.Collections.Generic;
namespace Downfield {
public class ClubBusiness {
 public int Team,Cash=12,FoodPrice=8,ParkingPrice=12,Fans=65,Security=75,JobGames,LossStreak,UnhappyGames;
 public int LastIncome,LastExpenses,LastAttendance,LastSeason=-1,LastRound=-1;public string LastReport="Play a game to see income and running costs.";
}
public partial class Franchise {
 public List<ClubBusiness> Businesses=new List<ClubBusiness>();public bool Fired;public string DismissalReason="";
 public ClubBusiness Business {get{return BusinessFor(UserTeam);}}
 public ClubBusiness BusinessFor(int team){if(Businesses==null)Businesses=new List<ClubBusiness>();ClubBusiness b=Businesses.Find(x=>x.Team==team);if(b==null){b=new ClubBusiness();b.Team=team;b.Cash=team==UserTeam?Credits:12;Businesses.Add(b);}return b;}
 public bool Employed {get{return !Fired;}}
 public int PriceMood {get{ClubBusiness b=Business;return -Math.Max(0,b.FoodPrice-8)-Math.Max(0,(b.ParkingPrice-12)/2)+(b.FoodPrice<=6?2:0)+(b.ParkingPrice<=8?2:0);}}
 public int ExpectedAttendance {get{ClubBusiness b=Business;double demand=.42+b.Fans*.0065,price=Math.Max(.35,1-Math.Max(0,b.FoodPrice-8)*.014-Math.Max(0,b.ParkingPrice-12)*.006);return (int)Math.Round(Math.Min(1,demand)*price*100);}}
 int GameCosts(){return OperatingCosts(UserTeam,false);}
 public bool SetStadiumPrice(bool food,int change){if(Fired){Notice="Find a new coaching job before managing a stadium.";return false;}ClubBusiness b=Business;int before=food?b.FoodPrice:b.ParkingPrice,after=Math.Max(0,Math.Min(food?20:40,before+change));if(before==after)return false;if(food)b.FoodPrice=after;else b.ParkingPrice=after;Log((food?"Food":"Parking")+" price set to $"+after+". Fans react at the next home game.");Save();return true;}
 void SettleBusiness(Fixture fixture,int own,int other){ClubBusiness b=Business;if(b.LastSeason==Season&&b.LastRound==fixture.Round)return;bool home=fixture.Home==UserTeam,win=own>other;SettleClubAccount(fixture,UserTeam,own,other);int income=b.LastIncome,expenses=b.LastExpenses;b.JobGames++;b.LossStreak=own<other?b.LossStreak+1:0;int reaction=win?4:own<other?-5:0;if(other-own>=21)reaction-=2;b.Fans=Math.Max(0,Math.Min(100,b.Fans+reaction+(home?PriceMood:0)));b.UnhappyGames=b.Fans<25?b.UnhappyGames+1:0;b.Security=Math.Max(0,Math.Min(100,b.Security+(win?5:own<other?-7:0)-(other-own>=21?3:0)-(b.Fans<35?6:0)-(Credits<0?5:0)));b.LastReport="Income $"+income+" / costs $"+expenses+" / net "+(income>=expenses?"+$":"-$")+Math.Abs(income-expenses)+".";Log(b.LastReport);if(b.JobGames>=4&&(b.Security<=15||b.UnhappyGames>=3||Credits<=-25)){Fired=true;DismissalReason=b.UnhappyGames>=3?"Fans have lost faith in the club.":Credits<=-25?"The club cannot cover its running costs.":"Results and board confidence fell too far.";Log("FIRED: "+DismissalReason+" Your career record stays. Open Coaching Jobs.");}}
 void IdleWeekCosts(){/* Match revenue and costs settle only when a fixture is played. No phantom playoff/bye bills. */}
 public bool JobAvailable(int team){if(team<0||team>=Teams.Count)return false;if(!Fired)return true;if(team==UserTeam)return false;List<Standing> table=Table();return table.FindIndex(s=>s.Team==team)>=8;}
 bool CanManageClub(){if(!Fired)return true;Notice="You were fired. Open Coaching Jobs to join a new club.";return false;}
}
}

