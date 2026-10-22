using System;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 void MenuPicture(string label,float x,float y,float size){float u=size/24;Color paper=Color.FromArgb(244,231,193),wood=Color.FromArgb(155,100,59),sky=Color.FromArgb(102,174,210);Action<float,float,float,float,Color> r=(a,b,w,h,c)=>Rect(x+a*u,y+b*u,w*u,h*u,c);string name=label.ToUpperInvariant();if(name=="MAIN MENU")name="BACK";else if(name=="LEAGUE")name="STANDINGS";else if(name=="TRANSFERS"||name=="TRADE PLAYERS")name="TRADE";else if(name=="STAFF + TRAINING")name="COACHES";else if(name=="MONEY + STADIUM")name="STADIUM + MONEY";else if(name=="TEAM IDENTITY")name="EDIT TEAM";
  if(name=="MY COACH"||name=="COACH"||name=="COACHES"||name=="STAFF"){CoachPortrait(x+size*.2f,y,size/25);if(name=="COACHES"||name=="STAFF"){r(17,4,3,9,Ink);r(13,12,6,2,Gold);}return;}
  if(name=="TROPHIES"||name=="CHAMPIONS"||name=="FRANCHISE"){Trophy(x+2*u,y,Math.Max(1,u),0,true);return;}
  if(name=="CLUBHOUSE"){r(2,15,21,3,wood);r(3,18,2,6,wood);r(19,18,2,6,wood);r(5,5,13,9,Ink);r(6,6,11,7,sky);r(10,14,3,2,paper);r(18,11,4,4,paper);return;}
  if(name=="ROSTER"||name=="SIGN PLAYERS"||name=="SIGN"||name=="TRADE"){for(int i=0;i<(name=="ROSTER"?3:2);i++){float a=1+i*8,b=name=="ROSTER"&&i==1?0:3;r(a+1,b,5,4,paper);r(a+1,b+4,5,4,Color.FromArgb(185,126,87));r(a,b+8,7,9,i%2==0?TeamPrimary:TeamSecondary);r(a+2,b+10,3,2,paper);}if(name=="SIGN PLAYERS"||name=="SIGN"){r(16,14,8,9,paper);r(18,16,5,1,Ink);r(18,19,4,1,Ink);}if(name=="TRADE"){r(3,20,17,2,Mint);r(18,18,2,2,Mint);r(3,22,2,2,Gold);}return;}
  if(name=="PLAYOFFS"){r(1,2,7,4,paper);r(1,17,7,4,paper);r(8,3,2,17,Mint);r(10,11,5,2,Mint);r(15,8,8,8,Gold);return;}
  if(name=="STANDINGS"||name=="TABLE"||name=="STAT LEADERS"||name=="LEADERS"){r(2,1,20,22,wood);r(3,3,18,18,paper);for(int i=0;i<3;i++){r(5,5+i*5,2,3,i==0?Gold:Blue);r(9,6+i*5,10-i*2,2,Ink);}return;}
  if(name=="SCHEDULE"){r(2,3,20,20,paper);r(2,3,20,5,Red);r(6,0,2,6,Muted);r(16,0,2,6,Muted);for(int i=0;i<6;i++)r(5+i%3*6,11+i/3*6,3,3,i==3?Gold:Blue);return;}
  if(name=="TRAINING"||name=="TRAIN"){r(1,6,4,12,Muted);r(5,9,2,6,paper);r(7,11,10,2,White);r(17,9,2,6,paper);r(19,6,4,12,Muted);return;}
  if(name=="FACILITIES"||name=="UPGRADES"||name=="STADIUM + MONEY"){r(1,10,22,12,Color.FromArgb(124,141,156));r(3,7,18,3,White);r(5,4,14,3,Muted);r(4,14,16,7,Color.FromArgb(47,100,61));for(int i=0;i<4;i++)r(5+i*4,15,1,5,paper);if(name=="STADIUM + MONEY"){r(17,1,6,7,Gold);r(19,2,2,5,wood);}return;}
  if(name=="MOVE TEAM"||name=="MOVE"){r(1,4,14,13,paper);r(15,9,7,8,TeamPrimary);r(16,10,4,4,sky);r(3,17,5,5,Ink);r(17,17,5,5,Ink);r(4,18,3,3,Muted);r(18,18,3,3,Muted);return;}
  if(name=="EDIT TEAM"||name=="TEAM"||name=="OUTFIT"){r(6,2,12,3,TeamSecondary);r(2,5,20,7,TeamPrimary);r(6,9,12,13,TeamPrimary);r(10,7,4,9,TeamSecondary);r(10,16,4,2,White);return;}
  if(name=="COACHING JOBS"||name=="JOBS"){r(3,7,19,15,wood);r(8,3,9,4,wood);r(10,4,5,3,Ink);r(3,12,19,2,Gold);r(11,12,3,5,paper);return;}
  if(name=="INJURIES"||name=="HEALTH"){r(3,7,19,15,paper);r(8,3,9,4,Muted);r(11,10,3,9,Red);r(8,13,9,3,Red);return;}
  if(name=="NEWS"){r(2,2,20,21,paper);r(4,4,16,3,Ink);r(4,10,7,7,Blue);r(13,10,7,2,Muted);r(13,14,7,2,Muted);r(4,19,16,1,Ink);return;}
  if(name=="HISTORY"){r(2,2,20,21,wood);r(4,3,7,17,paper);r(13,3,7,17,paper);r(11,2,2,21,Gold);for(int i=0;i<3;i++){r(5,6+i*4,5,1,Muted);r(14,6+i*4,5,1,Muted);}return;}
  if(name=="BACK"||name=="QUIT"){r(13,1,9,22,wood);r(15,3,5,18,Ink);r(1,10,15,3,Mint);r(1,8,3,7,Mint);r(4,6,3,3,Mint);r(4,14,3,3,Mint);return;}
  if(name=="FOOD"){r(4,5,16,4,Gold);r(2,9,20,3,Color.FromArgb(213,132,53));r(2,12,20,2,Mint);r(3,14,18,4,wood);r(3,18,18,3,Gold);return;}
  if(name=="PARKING"){r(2,11,21,9,Blue);r(5,7,14,5,Blue);r(6,8,11,4,sky);r(4,19,4,4,Ink);r(18,19,4,4,Ink);r(2,14,3,2,Gold);r(21,14,2,2,Red);return;}
  if(name=="PLAY GAME"||name=="KICK OFF"||name=="EXHIBITION"){r(2,8,3,8,wood);r(5,5,14,14,wood);r(19,8,3,8,wood);r(10,7,2,10,paper);r(8,9,6,1,paper);r(8,13,6,1,paper);return;}
  DrawIcon(MenuIcon(name,5),x,y,size,Mint);
 }
 void OfficeInterior(){Color wall=Color.FromArgb(70,84,89),wood=Color.FromArgb(130,87,56),darkWood=Color.FromArgb(78,52,43);Rect(26,168,1228,530,wall);Rect(26,410,1228,12,TeamSecondary);Rect(26,422,1228,276,darkWood);for(int row=0;row<9;row++)for(int column=0;column<17;column++){float x=28+column*76-(row%2)*38,y=425+row*31;Rect(Math.Max(28,x),y,Math.Min(72,x+72-28),27,(column+row)%3==0?Color.FromArgb(116,79,54):Color.FromArgb(102,70,50));}
  Rect(52,198,180,161,darkWood);Rect(60,206,164,145,Color.FromArgb(100,175,204));Rect(60,280,164,71,Color.FromArgb(65,108,68));for(int n=0;n<4;n++)Rect(63,275+n*10,158,5,Color.FromArgb(121+n*13,135+n*7,130));Rect(60,327,164,24,Color.FromArgb(39,107,58));Line(66,341,216,341,White,2);Rect(137,206,7,145,paperColor);Rect(60,268,164,7,paperColor);
  Rect(258,198,200,83,TeamPrimary);Rect(266,206,184,67,Ink);TextAt(Clip(franchise.Club.Mascot,14),275,221,16,TeamSecondary);TextAt("FOOTBALL OFFICE",275,250,10,White);Rect(258,299,200,94,darkWood);Rect(266,307,184,78,Color.FromArgb(33,78,54));for(int i=0;i<4;i++){Ell(282+i*39,356,10,10,paperColor);Line(287+i*39,356,287+i*39,326,paperColor,2);Line(287+i*39,326,304+i*34,316,Gold,2);}Trophy(402,321,2,0,true);
  CoachPortrait(240,370,4);Rect(67,442,378,56,wood);Rect(67,439,378,9,Color.FromArgb(179,121,71));Rect(79,498,26,49,darkWood);Rect(408,498,26,49,darkWood);Rect(100,392,104,61,Ink);Rect(107,399,90,45,Color.FromArgb(42,108,81));TextAt("DF2",128,415,14,Mint);Rect(143,453,18,10,Muted);Rect(114,464,81,6,Muted);Rect(293,454,56,6,paperColor);Rect(300,463,49,4,Muted);Rect(378,450,20,19,paperColor);Rect(396,452,7,12,paperColor);Rect(72,510,366,32,Ink);TextAt(Clip(franchise.Coach.Name,20)+" / HEAD COACH",84,521,10,White);
  Rect(53,569,394,104,TeamPrimary);Rect(59,575,382,92,Color.FromArgb(41,53,65));for(int i=0;i<3;i++){ClubPlayer p=franchise.Starter(franchise.UserTeam,i);if(p!=null){PlayerPortrait(p,91+i*128,576,57);TextAt(Clip(p.Name,10),66+i*128,644,10,paperColor);}}}
 Color paperColor {get{return Color.FromArgb(244,231,193);}}
 void DrawOfficeRoom(){FrontHeader("CLUBHOUSE / COACH'S OFFICE");OfficeInterior();for(int i=0;i<OfficeTitles.Length;i++){float x=486+i%3*256,y=175+i/3*66;bool chosen=selection==i;Rect(x+3,y+4,246,56,Color.FromArgb(35,34,36));Rect(x,y,246,56,chosen?Color.FromArgb(72,101,100):Color.FromArgb(39,47,57));if(chosen){Rect(x,y,4,56,Gold);Rect(x,y,246,3,Gold);}MenuPicture(OfficeTitles[i],x+9,y+8,36);TextAt(OfficeTitles[i],x+55,y+21,10,chosen?Gold:White);HitArea(x,y,246,56,i);}TextAt(Clip(franchise.Notice,94),40,722,11,franchise.Fired?Red:Mint);}
}
}
