using System;
namespace Downfield {
public enum MatchWeather {Clear,Rain,Snow}
public partial class Simulation {
 public int WeatherSelection=1,VenueTeam=-1,VenueAwayTeam=-1,VenueWeek,VenueSeason=1,VenueFixture;public string VenueCity="",VenueHomeName="",VenueAwayName="";
 public MatchWeather Weather;public int Temperature=70;public bool Indoor;public float Wind,MatchIntroTime;
 public bool PlayingAway {get{return Franchise!=null&&VenueTeam>=0&&VenueTeam!=Franchise.UserTeam;}}public string WeatherLabel {get{return Indoor?"DOME":Weather==MatchWeather.Snow?"SNOW":Weather==MatchWeather.Rain?"RAIN":"CLEAR";}}
 public float SurfaceGrip {get{return Weather==MatchWeather.Snow?.88f:Weather==MatchWeather.Rain?.94f:1;}}
 void PrepareMatchEnvironment(){ReceiverCommands=0;ReceiverCommand="";PlaymakerIndex=0;hitSerial=0;MatchIntroTime=2.8f;string city=(VenueCity??"").ToUpperInvariant();int hash=0;foreach(char c in city)hash=(hash*31+c)&0x7fffffff;int seed=unchecked(hash+VenueSeason*7187+VenueWeek*311+VenueFixture*31);Random weather=new Random(seed);
  Indoor=city=="NEW ORLEANS"||city=="DALLAS"||city=="ATLANTA"||city=="LOS ANGELES";int month=9+Math.Min(3,VenueWeek/4),offset=city=="MIAMI"?23:city=="NEW ORLEANS"?18:city=="DALLAS"?12:city=="ATLANTA"?10:city=="LOS ANGELES"?15:city=="BUFFALO"?-10:city=="DENVER"?-8:city=="NEW YORK"?-4:city=="SEATTLE"||city=="PORTLAND"?-4:0;
  Temperature=new int[]{74,62,49,37}[month-9]+offset+weather.Next(-7,8);int wet=city=="SEATTLE"||city=="PORTLAND"?62:city=="MIAMI"||city=="BUFFALO"||city=="NEW YORK"?44:city=="LOS ANGELES"?12:32;Weather=!Indoor&&weather.Next(100)<wet?(Temperature<=35?MatchWeather.Snow:MatchWeather.Rain):MatchWeather.Clear;
  if(WeatherSelection>0){Weather=(MatchWeather)Math.Max(0,Math.Min(2,WeatherSelection-1));if(Weather!=MatchWeather.Clear)Indoor=false;}
  if(Weather==MatchWeather.Snow)Temperature=Math.Min(32,Temperature);if(Indoor)Temperature=72;Wind=Indoor?0:(float)(weather.NextDouble()*2-1)*(Weather==MatchWeather.Clear?.3f:.8f);
 }
 public void VenueFromLeague(Franchise league,int opponent){Fixture fixture=Exhibition?null:league.UserGame;VenueTeam=fixture==null?league.UserTeam:fixture.Home;VenueAwayTeam=fixture==null?opponent:fixture.Away;VenueCity=league.Teams[VenueTeam].City;VenueHomeName=league.Teams[VenueTeam].Mascot;VenueAwayName=league.Teams[VenueAwayTeam].Mascot;VenueWeek=league.Round;VenueSeason=league.Season;VenueFixture=fixture==null?0:fixture.Id;}
}
}
