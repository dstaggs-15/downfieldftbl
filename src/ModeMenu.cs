using System;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 void DrawModeMenu(){Rect(0,0,1280,800,Ink);Rect(0,0,1280,7,Gold);TextAt("DOWNFIELD 2",40,44,56,White);TextAt("PICK YOUR GAME",44,141,16,Mint);string[] names={"EXHIBITION","FRANCHISE","BOWLING"},details={"QUICK FOOTBALL","BUILD YOUR TEAM","10 YARDS / 2 PLAYS"};for(int n=0;n<3;n++){float x=40+n*407;Tile(names[n],n,x,212,386,382,n);Rect(x+18,220,350,264,n==1?Color.FromArgb(72,75,87):Color.FromArgb(32,80,55));if(n==0){for(int i=0;i<6;i++)Line(x+28,255+i*40,x+357,255+i*40,Color.FromArgb(98,143,91),2);for(int i=0;i<3;i++){Player player=new Player(0,0,i==2?1:0,"WR",i==2?24:81,8);player.Primary=Franchise.Palette[i==2?2:franchise.Club.Primary];player.Secondary=Franchise.Palette[franchise.Club.Secondary];player.Pose="run";player.Step=time*3+i;player.VZ=3;g.DrawImage(PixelPlayers.Get(player),new Rectangle((int)x+56+i*100,310+i%2*36,48,64));}}
   else if(n==1){Rect(x+45,250,295,65,TeamPrimary);DrawTeamLogo(franchise.Club,x+61,260,43);TextAt(Clip(franchise.Club.Mascot,16),x+120,279,12,TeamSecondary);CoachPortrait(x+178,328,4.5f);Rect(x+72,378,250,28,Color.FromArgb(148,96,58));Rect(x+86,406,17,47,Color.FromArgb(92,63,45));Rect(x+290,406,17,47,Color.FromArgb(92,63,45));Trophy(x+89,334,2,0,true);}
   else{for(int row=0;row<4;row++)for(int col=0;col<=row;col++){float px=x+193+(col-row*.5f)*48,py=267+row*44;Ell(px-6,py,12,13,White);Rect(px-4,py+10,8,11,White);Rect(px-8,py+21,16,16,White);Rect(px-4,py+12,8,4,Red);}Ell(x+153,432,68,36,Color.FromArgb(156,88,47));Line(x+168,450,x+205,450,White,3);TextAt("X   /   300",x+106,498,18,Gold);}TextAt(details[n],x+193-PixelFont.Width(details[n],12)/2,620,12,Muted);}
  Tile("OPTIONS",5,40,681,590,63,3);Tile("QUIT",4,650,681,590,63,4);}
 void ModeNavigation(ref int direction){if(screen!="home")return;direction=0;if(navTime>0)return;bool down=input.Held(Input.Down)||input.MoveZ<-.55f,up=input.Held(Input.Up)||input.MoveZ>.55f;if(down){selection=selection==0?3:selection<3?4:selection;navTime=.19f;}else if(up){selection=selection==3?0:selection==4?2:selection;navTime=.19f;}}
}
}
