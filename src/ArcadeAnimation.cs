using System;
using System.Drawing;
namespace Downfield {
public partial class Simulation {
 void AnimateCelebration(float dt){if(State!=Phase.Conversion)return;foreach(Player p in Players)if(p.Team==0&&p.Active&&p.KnockTime<=0){p.Step+=dt*3;p.Pose=p==Controlled?"celebrate":"clap";p.PoseTime=.15f;}}
}
public partial class GameForm {
 Simulation catchMeterGame;int catchMeterFlight=-1;PointF catchMeterAnchor;
 void DrawCatchMeter(){Player receiver=game.CatchMeterActive?game.CatchDisplayReceiver:game.CatchReceiver;if(receiver==null||!game.CatchMeterActive&&game.CatchMeterRemaining==0)return;string button=Hint(input.ButtonLabel(Input.Y),input.ActionLabel(22));PointF point=Project(receiver.X,receiver.Z,3.8f);if(!game.CatchMeterActive){float px=Math.Max(70,Math.Min(1050,point.X)),py=Math.Max(289,Math.Min(604,point.Y));TextAt(button+" HOLD CATCH / "+game.CatchMeterRemaining+" LEFT",px-42,py-16,10,Gold);return;}
  if(catchMeterGame!=game||catchMeterFlight!=game.CatchMeterSequence){catchMeterGame=game;catchMeterFlight=game.CatchMeterSequence;catchMeterAnchor=new PointF(Math.Max(170,Math.Min(1110,point.X)),Math.Max(352,Math.Min(588,point.Y)));}
  float x=catchMeterAnchor.X,y=catchMeterAnchor.Y;Color border=game.CatchMeterLocked?(game.CatchGrade=="GREEN"?Mint:game.CatchGrade=="YELLOW"?Gold:Red):White;Rect(x-162,y-73,324,110,border);Rect(x-158,y-69,316,102,Ink);TextAt("#"+receiver.Number+" CATCH / "+game.CatchMeterRemaining+" LEFT",x-140,y-56,11,White);Rect(x-140,y-26,280,24,Red);Rect(x-140+280*.34f,y-26,280*.32f,24,Gold);Rect(x-140+280*.455f,y-26,280*.09f,24,Mint);Rect(x-145+280*game.CatchMeterValue,y-33,10,38,Ink);Rect(x-143+280*game.CatchMeterValue,y-31,6,34,White);string label=game.CatchMeterLocked?"LOCKED / "+game.CatchGrade:game.CatchMeterReady?"RELEASE "+button:"GET READY";TextAt(label,x-PixelFont.Width(label,12)/2,y+13,12,border);}
}
}

