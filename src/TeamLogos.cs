using System;
using System.Collections.Generic;
using System.Drawing;
namespace Downfield {
public static class TeamLogos {
 static readonly Dictionary<string,Bitmap> cache=new Dictionary<string,Bitmap>();
 public static Bitmap Get(LeagueTeam team){string key=team.Mascot+"/"+team.Primary+"/"+team.Secondary;Bitmap image;if(cache.TryGetValue(key,out image))return image;image=new Bitmap(40,40);using(Graphics g=Graphics.FromImage(image)){g.Clear(Color.Transparent);Color primary=Color.FromArgb(255,Color.FromArgb(Franchise.Palette[team.Primary])),secondary=Color.FromArgb(255,Color.FromArgb(Franchise.Palette[team.Secondary])),ink=Color.FromArgb(24,27,46),white=Color.FromArgb(246,235,203);Action<int,int,int,int,Color> r=(x,y,w,h,c)=>{using(SolidBrush b=new SolidBrush(c))g.FillRectangle(b,x,y,w,h);};
  r(8,1,24,3,ink);r(3,4,34,28,ink);r(7,32,26,4,ink);r(12,36,16,3,ink);r(7,6,26,24,primary);r(11,30,18,4,primary);int kind=Array.IndexOf(Franchise.Mascots,team.Mascot);if(kind<0)kind=team.Id%12;
  if(kind==0){for(int row=0;row<3;row++){r(9,11+row*7,8,3,secondary);r(14,9+row*7,10,3,secondary);r(22,11+row*7,9,3,secondary);}}
  else if(kind==1){for(int y=0;y<20;y++){int width=(int)(Math.Sqrt(Math.Max(0,100-(y-10)*(y-10))));r(20-width,9+y,width*2,1,secondary);}r(22,8,10,18,primary);r(18,9,8,5,primary);r(22,24,8,5,primary);r(28,12,3,3,white);}
  else if(kind==2){r(17,7,5,18,secondary);r(12,13,5,14,secondary);r(23,16,5,11,secondary);r(15,24,10,7,secondary);r(18,18,5,12,white);r(11,24,3,4,white);}
  else if(kind==3){r(11,13,18,12,secondary);r(8,12,7,5,secondary);r(25,9,6,9,secondary);r(29,7,3,8,white);r(25,23,4,8,secondary);r(12,23,4,8,secondary);r(29,15,2,2,ink);}
  else if(kind==4){r(9,18,22,10,secondary);r(15,10,12,13,secondary);r(22,10,7,5,white);r(24,15,6,7,primary);r(10,25,9,4,white);r(21,27,10,3,white);}
  else if(kind==5){for(int row=0;row<4;row++)r(18-row*3,8+row*5,4+row*6,5,secondary);r(18,28,5,6,white);}
  else if(kind==6){r(11,11,4,20,secondary);r(18,7,5,24,secondary);r(26,13,4,18,secondary);r(10,9,6,4,white);r(17,6,7,4,white);r(25,11,6,4,white);r(9,28,23,4,secondary);r(19,15,2,3,ink);r(19,21,2,3,ink);}
  else if(kind==7){for(int n=0;n<5;n++)r(8+n*3,27-n*3,7,3,secondary);r(23,8,4,14,white);r(18,13,14,4,white);r(21,11,8,8,secondary);}
  else if(kind==8){r(10,8,6,16,secondary);r(25,8,6,16,secondary);r(14,15,13,14,secondary);r(17,26,7,7,secondary);r(14,19,4,3,ink);r(24,19,4,3,ink);r(19,26,4,3,ink);r(12,8,2,6,white);r(27,8,2,6,white);}
  else if(kind==9){r(10,13,20,13,secondary);r(8,17,5,9,secondary);r(27,17,5,9,secondary);r(12,9,3,7,white);r(25,9,3,7,white);r(15,23,10,7,secondary);r(14,18,3,3,ink);r(23,18,3,3,ink);r(17,27,6,3,ink);}
  else if(kind==10){r(10,24,21,6,secondary);r(14,11,13,13,white);r(10,15,21,3,secondary);r(18,10,3,16,secondary);r(13,28,4,4,ink);r(25,28,4,4,ink);}
  else{r(19,7,10,5,secondary);r(15,12,10,5,secondary);r(11,17,17,5,secondary);r(19,22,5,5,secondary);r(15,27,5,6,secondary);r(12,11,4,3,white);r(28,25,3,3,white);}
 }cache[key]=image;return image;}
}
public partial class GameForm {
 LeagueTeam VenueClub {get{return game.Franchise!=null&&game.VenueTeam>=0&&game.VenueTeam<12?game.Franchise.Teams[game.VenueTeam]:VisualClub;}}
 Color VenuePrimary {get{return Color.FromArgb(255,Color.FromArgb(Franchise.Palette[VenueClub.Primary]));}}
 Color VenueSecondary {get{return Color.FromArgb(255,Color.FromArgb(Franchise.Palette[VenueClub.Secondary]));}}
 void DrawTeamLogo(LeagueTeam team,float x,float y,float size){g.DrawImage(TeamLogos.Get(team),new Rectangle((int)x,(int)y,(int)size,(int)size));}
 void TeamTile(LeagueTeam team,float x,float y,float w,float h,int index){Rect(x,y,w,h,selection==index?Color.FromArgb(67,93,100):Color.FromArgb(38,45,62));if(selection==index)Rect(x,y,w,4,Gold);DrawTeamLogo(team,x+w/2-39,y+12,78);TextAt(team.Mascot,x+w/2-PixelFont.Width(team.Mascot,14)/2,y+h-49,14,White);TextAt(team.City,x+w/2-PixelFont.Width(team.City,10)/2,y+h-24,10,Muted);HitArea(x,y,w,h,index);}
}
}
