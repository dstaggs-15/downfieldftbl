using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace Downfield {
public class KeyBinding {public string Action;public Keys Key;public KeyBinding(){}public KeyBinding(string action,Keys key){Action=action;Key=key;}}
public class PadBinding {public int Action,Button;public PadBinding(){}public PadBinding(int a,int b){Action=a;Button=b;}}
public class ControlBindings {
 public List<KeyBinding> Keyboard=new List<KeyBinding>();public List<PadBinding> Controller=new List<PadBinding>();public bool SwapSticks;
 public static readonly string[] Actions={"MOVE UP","MOVE DOWN","MOVE LEFT","MOVE RIGHT","SPRINT","PAUSE","MUTE","SNAP / SELECT","RUN-PASS","PLAYBOOK","ONSIDE TRY","PASS 1","PASS 2","PASS 3","PASS 4","PASS 5","GIVE / LATERAL","THROW AWAY","DIRECT RECEIVER","DIVE","SPIN","JUMP","AGGRESSIVE CATCH","SKILL UP","SKILL DOWN","SKILL LEFT","SKILL RIGHT"};
 public static readonly Keys[] Defaults={Keys.W,Keys.S,Keys.A,Keys.D,Keys.ShiftKey,Keys.P,Keys.M,Keys.Space,Keys.X,Keys.Y,Keys.R,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.Q,Keys.T,Keys.C,Keys.F,Keys.E,Keys.J,Keys.Y,Keys.Up,Keys.Down,Keys.Left,Keys.Right};
 public static readonly int[] PadActions={Input.A,Input.B,Input.X,Input.Y,Input.LB,Input.RB,Input.RClick,Input.Start,Input.View,0x10000,0x20000,Input.Up,Input.Down,Input.Left,Input.Right};
 public static readonly string[] PadLabels={"A / SNAP / PASS 1","B / SPIN / PASS 4","X / DIVE / PASS 2","Y / JUMP / CATCH / PASS 3","LB / GIVE / LATERAL","RB / PASS 5 / ONSIDE","RS CLICK / THROW AWAY","MENU / PAUSE","VIEW / MUTE","LT / DIRECT RECEIVER","RT / SPRINT","D-PAD UP","D-PAD DOWN","D-PAD LEFT","D-PAD RIGHT"};
 static string PathName {get{return Profile.SavePath+".controls.xml";}}
 public ControlBindings(){for(int i=0;i<Actions.Length;i++)Keyboard.Add(new KeyBinding(Actions[i],Defaults[i]));foreach(int b in PadActions)Controller.Add(new PadBinding(b,b));}
 public Keys Key(int action){KeyBinding b=Keyboard.Find(x=>x.Action==Actions[action]);return b==null?Defaults[action]:b.Key;}
 public string KeyName(int action){return KeyLabel(Key(action));}public static string KeyLabel(Keys key){string s=key.ToString().ToUpperInvariant();return s.StartsWith("D")&&s.Length==2&&char.IsDigit(s[1])?s.Substring(1):s=="SHIFTKEY"?"SHIFT":s;}
 public static string PadName(int b){int[] values={Input.A,Input.B,Input.X,Input.Y,Input.LB,Input.RB,Input.Start,Input.View,Input.RClick,0x40,0x10000,0x20000,Input.Up,Input.Down,Input.Left,Input.Right};string[] names={"A","B","X","Y","LB","RB","MENU","VIEW","RS CLICK","LS CLICK","LT","RT","D-PAD UP","D-PAD DOWN","D-PAD LEFT","D-PAD RIGHT"};int n=Array.IndexOf(values,b);return n<0?"?":names[n];}
 public int PadButton(int action){PadBinding b=Controller.Find(x=>x.Action==action);return b==null?action:b.Button;}
 static int KeyGroup(int n){if(n<=7||n>=23)return 0;if(n>=8&&n<=10)return 1;if(n>=11&&n<=15||n==17||n==18)return 2;if(n==19||n==20||n==21)return 3;if(n==22)return 4;return 5;}
 public string BindKey(int action,Keys key){if(key==Keys.Escape||key==Keys.Enter||key==Keys.F11||key==Keys.None)return "ESC / ENTER / F11 ARE RESERVED";Keys old=Key(action);foreach(KeyBinding b in Keyboard){int n=Array.IndexOf(Actions,b.Action);if(n!=action&&b.Key==key&&(KeyGroup(n)==0||KeyGroup(action)==0||KeyGroup(n)==KeyGroup(action)))b.Key=old;}Keyboard.Find(b=>b.Action==Actions[action]).Key=key;Save();return "SAVED / CONFLICTS SWAPPED";}
 public void BindPad(int action,int physical){PadBinding target=Controller.Find(b=>b.Action==action);int prior=target.Button;foreach(PadBinding b in Controller)if(b!=target&&b.Button==physical)b.Button=prior;target.Button=physical;Save();}
 public int TranslatePad(int physical){int translated=0;foreach(PadBinding b in Controller)if((physical&b.Button)!=0)translated|=b.Action;return translated;}
 public static ControlBindings Load(){try{using(FileStream f=File.OpenRead(PathName)){ControlBindings p=(ControlBindings)new XmlSerializer(typeof(ControlBindings)).Deserialize(f); // XML fills constructor lists: keep the last binding for each action.
 ControlBindings clean=new ControlBindings();foreach(KeyBinding b in p.Keyboard){KeyBinding c=clean.Keyboard.Find(x=>x.Action==b.Action);if(c!=null)c.Key=b.Key;}foreach(PadBinding b in p.Controller){PadBinding c=clean.Controller.Find(x=>x.Action==b.Action);if(c!=null)c.Button=b.Button;}clean.SwapSticks=p.SwapSticks;return clean;}}catch{return new ControlBindings();}}
 public void Save(){try{Directory.CreateDirectory(Path.GetDirectoryName(PathName));using(FileStream f=File.Create(PathName+".tmp"))new XmlSerializer(typeof(ControlBindings)).Serialize(f,this);if(File.Exists(PathName))File.Replace(PathName+".tmp",PathName,null);else File.Move(PathName+".tmp",PathName);}catch{}}
}
public partial class Input {
 public ControlBindings Bindings=ControlBindings.Load();public int RawButtons;public bool CatchPressed;
 bool ActionKey(int n){Keys k=Bindings.Key(n);return KeysDown.Contains(k)||k==Keys.ShiftKey&&(KeysDown.Contains(Keys.LShiftKey)||KeysDown.Contains(Keys.RShiftKey));}
 bool FreshAction(int n){return ActionKey(n)&&!priorKeys.Contains(Bindings.Key(n));}
 public string ActionLabel(int n){return Bindings.KeyName(n);}public string ButtonLabel(int virtualButton){return ControlBindings.PadName(Bindings.PadButton(virtualButton));}
}
}
