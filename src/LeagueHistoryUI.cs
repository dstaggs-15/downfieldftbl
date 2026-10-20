using System;
using System.Collections.Generic;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 int historySeason;
 bool HistoryScreen(){return screen=="history"||screen=="historyseason"||screen=="historyteam";}
 List<int> HistoryYears(){List<int> years=new List<int>();foreach(TeamSeason h in franchise.History)if(!years.Contains(h.Season))years.Add(h.Season);years.Sort((a,b)=>b.CompareTo(a));return years;}
 List<TeamSeason> SeasonHistory(){List<TeamSeason> rows=franchise.History.FindAll(h=>h.Season==historySeason);rows.Sort((a,b)=>{int n=(b.W*2+b.T).CompareTo(a.W*2+a.T);if(n==0)n=(b.PF-b.PA).CompareTo(a.PF-a.PA);return n==0?b.PF.CompareTo(a.PF):n;});return rows;}
 List<TeamSeason> ClubHistory(){List<TeamSeason> rows=franchise.History.FindAll(h=>h.Team==historyTeam);rows.Sort((a,b)=>b.Season.CompareTo(a.Season));return rows;}
 int HistoryCount(){return (screen=="history"?HistoryYears().Count:screen=="historyseason"?SeasonHistory().Count:ClubHistory().Count)+1;}
 void HistoryBack(){Open(screen=="historyteam"?"historyseason":screen=="historyseason"?"history":"franchisehub");}
 void ActivateHistory(){if(selection==HistoryCount()-1){HistoryBack();return;}if(screen=="history"){historySeason=HistoryYears()[selection];Open("historyseason");}else if(screen=="historyseason"){historyTeam=SeasonHistory()[selection].Team;Open("historyteam");}}
 LeagueTeam HistoricalClub(TeamSeason h){LeagueTeam current=franchise.Teams[h.Team];return new LeagueTeam{Id=h.Team,City=current.City,Mascot=h.LogoMascot.Length==0?current.Mascot:h.LogoMascot,Primary=h.LogoPrimary<0?current.Primary:h.LogoPrimary,Secondary=h.LogoSecondary<0?current.Secondary:h.LogoSecondary};}
 string SeasonFinish(TeamSeason h){return h.Champion?"CHAMPION":h.Seed>0?"PLAYOFF SEED #"+h.Seed:"MISSED PLAYOFFS";}
 void HistoryBackButton(int index){Rect(40,711,1200,38,selection==index?TeamPrimary:Color.FromArgb(38,46,60));TextAt("BACK",60,723,14,Gold);HitArea(40,711,1200,38,index);}
 void DrawLeagueHistory(){
  if(screen=="history"){FrontHeader("LEAGUE HISTORY");List<int> years=HistoryYears();if(years.Count==0){TextAt("YOUR LEAGUE'S STORY STARTS HERE",70,250,24,Gold);TextAt("Finish a season to save its champion, awards and standings.",70,310,14,White);}int first=selection<years.Count?selection/6*6:0;for(int n=first;n<Math.Min(first+6,years.Count);n++){int year=years[n];TeamSeason champ=franchise.History.Find(h=>h.Season==year&&h.Champion);float y=177+(n-first)*85;Rect(40,y,1200,75,selection==n?Color.FromArgb(63,89,96):Color.FromArgb(36,44,58));if(champ!=null)DrawTeamLogo(HistoricalClub(champ),58,y+7,60);TextAt("SEASON "+year,144,y+12,18,Gold);TextAt(champ==null?"SEASON RESULTS":Clip(champ.Name,31)+" / CHAMPION",144,y+44,13,White);TextAt("VIEW RESULTS >",1020,y+29,12,Mint);HitArea(40,y,1200,75,n);}HistoryBackButton(years.Count);return;}
  if(screen=="historyseason"){List<TeamSeason> rows=SeasonHistory();FrontHeader("SEASON "+historySeason+" / FINAL RESULTS");TeamSeason champ=rows.Find(h=>h.Champion);if(champ!=null){DrawTeamLogo(HistoricalClub(champ),42,166,62);TextAt("CHAMPION",122,171,12,Gold);TextAt(champ.Name,122,195,22,White);}TextAt("CLUB",140,245,11,Muted);TextAt("W - L - T",570,245,11,Muted);TextAt("POINTS FOR / AGAINST",755,245,11,Muted);TextAt("FINISH",1040,245,11,Muted);
   for(int n=0;n<rows.Count;n++){TeamSeason h=rows[n];float y=264+n*31;Rect(40,y,1200,28,selection==n?Color.FromArgb(63,89,96):Color.FromArgb(36,44,58));TextAt((n+1).ToString("00"),54,y+8,11,Muted);DrawTeamLogo(HistoricalClub(h),93,y,28);TextAt(Clip(h.Name,32),140,y+8,11,White);TextAt(h.W+" - "+h.L+" - "+h.T,570,y+8,12,White);TextAt(h.PF+" / "+h.PA,780,y+8,12,Mint);TextAt(SeasonFinish(h),1010,y+8,10,h.Champion?Gold:Muted);HitArea(40,y,1200,28,n);}
   SeasonAward mvp=franchise.Awards.Find(a=>a.Season==historySeason&&a.Type=="MVP"),op=franchise.Awards.Find(a=>a.Season==historySeason&&a.Type=="OPOTY");TextAt("MVP: "+(mvp==null?"NO AWARD":mvp.Winner+" / "+Clip(mvp.Club,26)),40,652,12,Gold);TextAt("OPOTY: "+(op==null?"NO AWARD":op.Winner+" / "+Clip(op.Club,26)),650,652,12,Mint);TextAt("SELECT A CLUB TO SEE ITS SEASONS. RECORDS ARE REGULAR SEASON.",40,686,11,Muted);HistoryBackButton(rows.Count);return;
  }
  List<TeamSeason> seasons=ClubHistory();FrontHeader(franchise.Teams[historyTeam].Mascot+" / TEAM HISTORY");int wins=0,losses=0,ties=0,titles=0;foreach(TeamSeason h in seasons){wins+=h.W;losses+=h.L;ties+=h.T;if(h.Champion)titles++;}TextAt(wins+" W / "+losses+" L / "+ties+" T     "+titles+" TITLES",40,178,20,Gold);TextAt("YEAR",58,224,11,Muted);TextAt("CLUB THAT SEASON",220,224,11,Muted);TextAt("RECORD",620,224,11,Muted);TextAt("POINTS FOR / AGAINST",790,224,11,Muted);TextAt("FINISH",1080,224,11,Muted);int start=selection<seasons.Count?selection/12*12:0;
  for(int n=start;n<Math.Min(start+12,seasons.Count);n++){TeamSeason h=seasons[n];float y=246+(n-start)*36;Rect(40,y,1200,32,selection==n?Color.FromArgb(63,89,96):Color.FromArgb(36,44,58));TextAt("S"+h.Season,58,y+10,12,Gold);DrawTeamLogo(HistoricalClub(h),158,y,32);TextAt(Clip(h.Name,28),220,y+10,11,White);TextAt(h.W+"-"+h.L+"-"+h.T,620,y+10,12,White);TextAt(h.PF+" / "+h.PA,820,y+10,12,Mint);TextAt(SeasonFinish(h),1045,y+10,10,h.Champion?Gold:Muted);HitArea(40,y,1200,32,n);}HistoryBackButton(seasons.Count);
 }
}
}
