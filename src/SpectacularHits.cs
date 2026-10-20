using System;
using System.Drawing;
namespace Downfield {
public partial class Simulation {
 public int SpectacularHits;public float ImpactPower;
 void ArcadeContact(Player victim,float x,float z,float closing,bool landing){bool huge=SuperExtreme||landing||closing>=14||closing>=9&&random.NextDouble()<.32;KnockDown(victim,x,z,SuperExtreme?25:huge?21:11);if(huge){SpectacularHits++;ImpactPower=2;HitFlash=1.15f;HitLabel=landing?"CRASH LANDING!":closing>=14?"ABSOLUTELY LEVELED!":"HIT STICK!";Cue("slam");Cue("crowd_cheer");}else ImpactPower=1;if(huge&&!landing)HitLabel=new string[]{"ABSOLUTELY LEVELED!","UPENDED!","CLOTHESLINED!","CORKSCREW!","TURF SLAM!","CARTWHEELED!"}[victim.HitStyle];}
}
public partial class GameForm {
 void DrawImpactBurst(){if(game.HitFlash<=0)return;PointF hit=Project(game.HitX,game.HitZ,1.0f);bool huge=game.ImpactPower>=2;float progress=Math.Max(0,1-game.HitFlash/(huge?1.2f:.95f)),radius=(huge?32:18)+progress*(huge?105:24);for(int ray=0;ray<(huge?16:8);ray++){double angle=ray*Math.PI/(huge?8:4);float inner=radius*.65f,outer=radius+(huge?16:8);Line(hit.X+(float)Math.Cos(angle)*inner,hit.Y+(float)Math.Sin(angle)*inner*.65f,hit.X+(float)Math.Cos(angle)*outer,hit.Y+(float)Math.Sin(angle)*outer*.65f,ray%2==0?Gold:White,huge?6:4);}if(huge){for(int i=0;i<18;i++){double angle=i*2.39996;float speed=40+i%5*14,dx=(float)Math.Cos(angle)*progress*speed,dy=(float)Math.Sin(angle)*progress*speed*.4f-progress*(1-progress)*130;Rect(hit.X+dx,hit.Y+dy,4+i%3*2,4,i%3==0?Color.FromArgb(158,101,59):Mint);}if(game.HitFlash>1.02f)Rect(0,276,1280,418,Color.FromArgb(28,255,231,156));}float width=Math.Max(360,PixelFont.Width(game.HitLabel,16)+40);Rect(640-width/2,163,width,38,Ink);Center(game.HitLabel,174,16,Gold);}
 void DrawDazedStars(Player player,float x,float y,float w,float h,float progress){if(player.ImpactLevel<2||progress<.48f)return;for(int i=0;i<3;i++){double angle=time*6+i*Math.PI*2/3;float sx=x+(float)Math.Cos(angle)*w*.6f,sy=y-h*.3f+(float)Math.Sin(angle)*7;Rect(sx-4,sy,10,3,Gold);Rect(sx,sy-4,3,10,Gold);}}
}
}
