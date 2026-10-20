using System;
namespace Downfield {
public partial class Simulation {
 public int PlaymakerIndex;public int ReceiverCommands;public string ReceiverCommand="";
 public void SelectPlaymaker(int index){PlaymakerIndex=Math.Max(0,Math.Min(4,index));}
 public void NearestPlaymaker(){float nearest=999;for(int i=0;i<5;i++){Player p=Receiver(i);if(!p.Active)continue;float distance=Distance(p.X,p.Z,Controlled.X,Controlled.Z);if(distance<nearest){nearest=distance;PlaymakerIndex=i;}}}
 public bool DirectReceiver(int index,float x,float z){if(!CanThrow||Charging||Math.Abs(x)+Math.Abs(z)<.5f)return false;Player p=Receiver(index);if(!p.Active||p.KnockTime>0)return false;float length=Distance(x,z,0,0);p.CommandX=Clamp(p.X+x/length*12,-24.5f,24.5f);p.CommandZ=Clamp(p.Z+z/length*12,Spot+1,105);p.CommandUntil=playTime+4;PlaymakerIndex=((index%5)+5)%5;ReceiverCommands++;ReceiverCommand="#"+p.Number+" "+(Math.Abs(x)>Math.Abs(z)?x<0?"BREAK LEFT":"BREAK RIGHT":z>0?"GO DEEP":"COME BACK");Message=ReceiverCommand;return true;}
}
public partial class GameForm {
 bool playmakerHeld,playmakerReady=true;
 bool PlaymakerInput(){if(!input.Playmaker){playmakerHeld=false;playmakerReady=true;return false;}if(!playmakerHeld){game.NearestPlaymaker();game.Charging=false;pendingThrow=0;playmakerHeld=true;}for(int i=0;i<passButtons.Length;i++)if(input.Pressed(passButtons[i]))game.SelectPlaymaker(i);if(Math.Abs(input.AimX)+Math.Abs(input.AimZ)<.25f)playmakerReady=true;if(playmakerReady&&(Math.Abs(input.AimX)>.65f||Math.Abs(input.AimZ)>.65f)){game.DirectReceiver(game.PlaymakerIndex,input.AimX,input.AimZ);playmakerReady=false;}return true;}
 void DrawPlaymaker(){if(!QBMode()||!input.Playmaker)return;Player receiver=game.Receiver(game.PlaymakerIndex);if(!receiver.Active)return;System.Drawing.PointF point=Project(receiver.X,receiver.Z,3.8f);float x=Math.Max(100,Math.Min(1180,point.X)),y=Math.Max(300,Math.Min(620,point.Y-20));Rect(x-65,y-26,130,22,Gold);TextAt("DIRECT #"+receiver.Number,x-57,y-20,10,Ink);if(receiver.CommandUntil>0){System.Drawing.PointF target=Project(receiver.CommandX,receiver.CommandZ,.05f);Line(point.X,point.Y+20,target.X,target.Y,Gold,3);Rect(target.X-5,target.Y-5,10,10,Gold);}Rect(250,228,780,38,Ink);Center(Hint("LT + RECEIVER ICON SELECTS / FLICK RS TO SEND","C + 1-5 SELECTS / ARROWS SEND THE RECEIVER"),241,12,Gold);}
}
}
