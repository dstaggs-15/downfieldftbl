using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Downfield {
public static partial class UiTests {
 static void CatchMeterScreens(GameForm f,string evidence){Set(f,"matchFixture",-1);Call(f,"StartPractice");Simulation s=(Simulation)Get(f,"game");Input input=(Input)Get(f,"input");input.Bindings=new ControlBindings();input.Clear();s.Playbook=2;s.Snap();for(int n=0;n<35;n++)s.Update(.02f,0,0);s.BeginPass(0);s.Charge=.1f;s.ReleasePass();Step(f,Keys.Y,true,1);Check(s.CatchMeterActive&&!s.CatchMeterReady,"hold starts catch meter");f.SavePreview(Path.Combine(evidence,"catch-meter-ready.png"));PointF anchor=(PointF)Get(f,"catchMeterAnchor");Set(f,"cameraX",4f);s.Receiver(0).X+=2;Step(f,Keys.Y,true,7);f.SavePreview(Path.Combine(evidence,"catch-meter-moving.png"));Check((PointF)Get(f,"catchMeterAnchor")==anchor,"meter stays anchored during hold");Check(!s.CatchMeterLocked&&s.CatchMeterRemaining==4,"held button spends one use without locking");Tap(f,Keys.Escape);float paused=s.CatchMeterValue;Step(f,Keys.Y,true,4);Check(s.CatchMeterValue==paused,"pause freezes held timing");Tap(f,Keys.Escape);Step(f,Keys.Y,false,1);float locked=s.CatchMeterValue;Check(s.CatchMeterLocked,"key release locks through real game input");f.SavePreview(Path.Combine(evidence,"catch-meter-locked.png"));Step(f,Keys.Y,true,4);Step(f,Keys.Y,false,1);Check(s.CatchMeterValue==locked&&s.CatchMeterRemaining==4,"extra presses cannot retry or spend more uses on same pass");f.SavePreview(Path.Combine(evidence,"catch-meter.png"));Set(f,"matchFixture",-1);Set(f,"screen","home");input.Clear();}
}
}

