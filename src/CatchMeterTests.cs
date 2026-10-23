using System;
using System.Windows.Forms;
namespace Downfield {
public static class CatchMeterTests {
 static int checks,failures;static void Check(bool ok,string name){checks++;if(!ok){failures++;Console.WriteLine("FAIL Catch meter: "+name);}}
 static Simulation Throw(){Simulation s=new Simulation(120);s.Practice=true;s.NewGame();Pass(s);return s;}
 static void Pass(Simulation s){typeof(Simulation).GetMethod("Setup",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).Invoke(s,null);s.CallPlay(2);s.Snap();for(int n=0;n<35;n++)s.Update(.02f,0,0);s.BeginPass(0);s.Charge=.1f;s.ReleasePass();}
 static void Advance(Simulation s,float seconds,int fps){for(int n=0;n<(int)Math.Round(seconds*fps);n++)s.Update(1f/fps,0,0);}
 public static int Run(){checks=failures=0;Simulation s=Throw();s.AggressiveCatch();Check(s.CatchMeterRemaining==4,"activation consumes one of five uses");Advance(s,.15f,60);Check(s.CatchMeterValue==0&&!s.CatchMeterReady,"ready cue before sweep");Advance(s,.55f,60);Check(Simulation.CatchChance(s.CatchMeterValue)==1&&!s.CatchMeterLocked,"holding reaches narrow green timing window");s.AggressiveCatch();Check(!s.CatchMeterLocked&&s.CatchMeterRemaining==4,"extra press cannot lock or spend another use");s.ReleaseCatch();float locked=s.CatchMeterValue;Check(s.CatchMeterLocked&&s.CatchGrade=="GREEN","release locks sampled value");Advance(s,.4f,60);s.ReleaseCatch();Check(s.CatchMeterValue==locked,"releasing again cannot change result");for(int n=0;n<300&&s.State==Phase.Flight;n++)s.Update(1f/60,0,0);Check(s.Completions==1,"well-timed green completes catch");Advance(s,1,60);Check(s.CatchDisplayReceiver==null,"result clears after display");
  foreach(int fps in new int[]{20,60,144}){s=Throw();s.AggressiveCatch();Advance(s,.7f,fps);Check(Math.Abs(s.CatchMeterValue-(.7f-.18f)/1.05f)<.012f,"real-time timing independent of FPS / "+fps);}
  s=Throw();s.AggressiveCatch();s.ReleaseCatch();Check(s.CatchGrade=="RED","quick tap releases too early");s=Throw();s.AggressiveCatch();Advance(s,2.5f,60);Check(s.CatchMeterLocked&&s.CatchGrade=="RED"&&s.State!=Phase.Flight,"holding past window fails without hanging");
  s=Throw();for(int i=0;i<5;i++){if(i>0)Pass(s);s.AggressiveCatch();s.ReleaseCatch();Advance(s,2,60);}Check(s.CatchMeterRemaining==0&&s.CatchMeterUses==5,"five passes exhaust game budget");Pass(s);s.AggressiveCatch();Check(!s.CatchMeterActive&&s.CatchMeterUses==5,"sixth activation denied");Advance(s,2.5f,60);Check(s.State!=Phase.Flight,"ordinary catches resolve after budget runs out");s.NewGame();Check(s.CatchMeterRemaining==5,"new game refills budget");
  Input input=new Input();input.SuppressPad=true;input.Bindings=new ControlBindings();input.Bindings.BindKey(22,Keys.H);input.Context="flight";input.KeysDown.Add(Keys.H);input.Poll();Check(input.CatchPressed&&input.CatchHeld,"remapped keyboard press starts hold");input.Poll();Check(!input.CatchPressed&&input.CatchHeld,"held key is not another press");input.KeysDown.Clear();input.Poll();Check(!input.CatchHeld,"remapped key release detected");Check(Simulation.CatchChance(.55f)<1&&Simulation.CatchChance(.5f)==1,"narrower green region");
  Console.WriteLine("Catch meter: "+checks+" checks, "+failures+" failures");return failures;
 }
}
}
