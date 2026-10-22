using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Downfield {
public static partial class UiTests {
 static void CatchMeterScreens(GameForm f,string evidence){Set(f,"matchFixture",-1);Call(f,"StartPractice");Simulation s=(Simulation)Get(f,"game");Input input=(Input)Get(f,"input");input.Bindings=new ControlBindings();input.Clear();s.Playbook=2;s.Snap();for(int n=0;n<35;n++)s.Update(.02f,0,0);s.BeginPass(0);s.Charge=.1f;s.ReleasePass();Tap(f,Keys.Y);Check(s.CatchMeterActive&&!s.CatchMeterReady,"catch meter shows ready cue before moving");f.SavePreview(Path.Combine(evidence,"catch-meter-ready.png"));PointF anchor=(PointF)Get(f,"catchMeterAnchor");Set(f,"cameraX",4f);s.Receiver(0).X+=2;f.SavePreview(Path.Combine(evidence,"catch-meter-moving.png"));Check((PointF)Get(f,"catchMeterAnchor")==anchor,"meter stays anchored when receiver and camera move");Step(f,Keys.None,false,9);Check(s.CatchMeterValue>.2f&&s.CatchMeterValue<.35f,"visible sweep progresses slowly through actual frame updates");Tap(f,Keys.Escape);float paused=s.CatchMeterValue;Step(f,Keys.None,false,4);Check(s.CatchMeterValue==paused,"pause freezes catch timing");Tap(f,Keys.Escape);Tap(f,Keys.Y);float locked=s.CatchMeterValue;Check(s.CatchMeterLocked,"second keyboard press locks through real game input");f.SavePreview(Path.Combine(evidence,"catch-meter-locked.png"));Step(f,Keys.Y,true,6);Step(f,Keys.Y,false,1);Check(s.CatchMeterValue==locked,"holding catch after lock cannot restart movement");f.SavePreview(Path.Combine(evidence,"catch-meter.png"));Check((PointF)Get(f,"catchMeterAnchor")==anchor,"locked result retains the same screen position");Set(f,"matchFixture",-1);Set(f,"screen","home");input.Clear();}
}
}
