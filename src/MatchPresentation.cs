using System;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 void DrawWeather(){if(game.Weather==MatchWeather.Clear)return;bool snow=game.Weather==MatchWeather.Snow;for(int n=0;n<(snow?105:80);n++){float x=(n*173.37f+time*(snow?13:95)+30*(float)Math.Sin(time+n))%1340-30,y=276+(n*53.71f+time*(snow?24:340))%416;if(snow){Rect(x,y,n%3==0?4:2,2,Color.FromArgb(220,242,246,246));if(n%3==0)Rect(x+1,y-1,2,4,White);}else Line(x,y,x-4,y+12,Color.FromArgb(100,155,197,215),1);}}
 void DrawMatchPresentation(){if(screen!="game")return;if(!input.Playmaker)Rect(840,222,420,35,Ink);string city=game.VenueCity.Length==0?game.HomeCity:game.VenueCity;if(!input.Playmaker)TextAt(Clip(city,20)+" / "+game.WeatherLabel+" "+game.Temperature+"F",850,234,11,game.Weather==MatchWeather.Snow?White:game.Weather==MatchWeather.Rain?Blue:Muted);
  DrawPlaymaker();if(game.State!=Phase.PreSnap||game.MatchIntroTime<=0||game.OnsideRulesPending)return;Rect(265,323,750,191,Ink);Rect(265,323,750,5,game.SuperExtreme?Red:Gold);DrawTeamLogo(VenueClub,295,365,100);LeagueTeam away=game.Franchise!=null&&game.VenueAwayTeam>=0?game.Franchise.Teams[game.VenueAwayTeam]:franchise.Teams[(franchise.UserTeam+1)%12];DrawTeamLogo(away,886,365,100);Center(game.Postseason?game.Championship?"CHAMPIONSHIP NIGHT":"PLAYOFF FOOTBALL":game.Exhibition?"DOWNFIELD EXHIBITION":"WEEK "+(game.VenueWeek+1)+" FOOTBALL",340,18,Gold);Center(Clip(game.VenueAwayName.Length==0?game.OpponentName:game.VenueAwayName,18)+" AT "+Clip(game.VenueHomeName.Length==0?game.HomeName:game.VenueHomeName,18),388,17,White);Center(GamePreferences.DifficultyNames[Math.Max(0,Math.Min(4,game.Difficulty))]+" / "+game.WeatherLabel,433,14,game.SuperExtreme?Red:Mint);Center(Hint("A SNAP TO START","SPACE SNAP TO START"),478,11,Muted);
 }
 void DrawKnockdown(Player p,Bitmap sprite,float x,float y,int w,int h){float t=1-p.KnockTime/Math.Max(.01f,p.KnockDuration),arc=(float)Math.Sin(Math.Min(1,t/.7f)*Math.PI),sign=p.KnockVX<0?-1:1,rotation=0,hop=0,sx=1,sy=1;int style=p.HitStyle;
  if(style==0){rotation=Math.Min(1,t/.46f)*270*sign;hop=arc*h*1.1f;}
  else if(style==1){rotation=Math.Min(1,t/.45f)*-450*sign;hop=arc*h*1.35f;sy=1-t*.2f;}
  else if(style==2){rotation=Math.Min(1,t/.22f)*92*sign;hop=arc*h*.38f;}
  else if(style==3){rotation=Math.Min(1,t/.65f)*450*sign;hop=arc*h*.8f;sx=.55f+.45f*(float)Math.Abs(Math.Cos(t*9));}
  else if(style==4){rotation=Math.Min(1,t/.24f)*90*sign;hop=(float)Math.Abs(Math.Sin(Math.Min(1,t/.8f)*Math.PI*2))*h*.34f;sy=.8f;}
  else{rotation=Math.Min(1,t/.4f)*-270*sign;hop=arc*h*.95f;sx=1.05f;}
  if(p.ImpactLevel<2){rotation*=.5f;hop*=.45f;}System.Drawing.Drawing2D.GraphicsState transform=g.Save();g.TranslateTransform(x,y-h*.3f-hop);g.RotateTransform(rotation);g.ScaleTransform(sx,sy);g.DrawImage(sprite,new Rectangle(-w/2,(int)(-h*.65f),w,h));g.Restore(transform);DrawDazedStars(p,x,y,w,h,t);
 }
}
}
