using System;
using System.Drawing;
namespace Downfield {
public partial class Simulation {
 void AnimateCelebration(float dt){if(State!=Phase.Conversion)return;foreach(Player p in Players)if(p.Team==0&&p.Active&&p.KnockTime<=0){p.Step+=dt*3;p.Pose=p==Controlled?"celebrate":"clap";p.PoseTime=.15f;}}
}
public partial class GameForm {
 void DrawCatchMeter(){Player receiver=game.CatchReceiver;if(receiver==null)return;PointF point=Project(receiver.X,receiver.Z,3.8f);float x=Math.Max(70,Math.Min(1210,point.X)),y=Math.Max(289,Math.Min(604,point.Y));if(!game.CatchMeterActive){TextAt(Hint(input.ButtonLabel(Input.Y),input.ActionLabel(22))+" CATCH",x-42,y-16,10,Gold);return;}Rect(x-68,y-35,136,34,Ink);Rect(x-60,y-25,120,12,Red);Rect(x-60+120*.28f,y-25,120*.44f,12,Gold);Rect(x-60+120*.435f,y-25,120*.13f,12,Mint);Rect(x-62+120*game.CatchMeterValue,y-31,4,24,White);TextAt(game.CatchMeterLocked?game.CatchGrade:Hint(input.ButtonLabel(Input.Y),input.ActionLabel(22))+" STOP",x-40,y+4,10,White);}
}
}
