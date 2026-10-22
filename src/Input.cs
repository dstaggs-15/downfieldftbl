using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace Downfield {
public partial class Input {
 [StructLayout(LayoutKind.Sequential)]struct Pad{public ushort buttons;public byte lt,rt;public short lx,ly,rx,ry;}
 [StructLayout(LayoutKind.Sequential)]struct State{public uint packet;public Pad pad;}
 [DllImport("xinput1_4.dll",EntryPoint="XInputGetState")]static extern uint Read4(uint i,out State s);
 [DllImport("xinput1_3.dll",EntryPoint="XInputGetState")]static extern uint Read3(uint i,out State s);
 [DllImport("xinput9_1_0.dll",EntryPoint="XInputGetState")]static extern uint Read9(uint i,out State s);
 public const int A=0x1000,B=0x2000,X=0x4000,Y=0x8000,LB=0x100,RB=0x200,Start=0x10,View=0x20,RClick=0x80,Up=1,Down=2,Left=4,Right=8;
 public HashSet<Keys> KeysDown=new HashSet<Keys>();HashSet<Keys> priorKeys=new HashSet<Keys>();
 public bool Playmaker;public bool SuppressPad;public bool Connected,Focused=true,UsesController,PausePressed;public int Controller=-1,Buttons,Previous;public float MoveX,MoveZ,AimX,AimZ,Sprint;
 public string Context="menu";int provider=4,lastPad;float oldX,oldZ,oldAimX,oldAimZ,oldSprint;
 uint Read(uint i,out State s){try{if(provider==4)return Read4(i,out s);if(provider==3)return Read3(i,out s);return Read9(i,out s);}catch(DllNotFoundException){if(provider==4){provider=3;return Read(i,out s);}if(provider==3){provider=9;return Read(i,out s);}s=new State();return 1;}catch(EntryPointNotFoundException){s=new State();return 1;}}
 public static void Analog(short sx,short sy,out float x,out float z){x=sx/32767f;z=sy/32767f;float l=(float)Math.Sqrt(x*x+z*z);if(l<=.18f){x=z=0;return;}float scale=Math.Min(1,(l-.18f)/.82f)/l;x*=scale;z*=scale;}
 public bool Pressed(int b){return(Buttons&b)!=0&&(Previous&b)==0;}public bool Released(int b){return(Buttons&b)==0&&(Previous&b)!=0;}public bool Held(int b){return(Buttons&b)!=0;}
 bool K(Keys k){return KeysDown.Contains(k);}public void NoteKeyboard(){UsesController=false;}
 // Device selection is based on intentional input, not just a connected cable.
 public void ObservePadActivity(int buttons,float x,float z,float ax,float az,float sprint){if(buttons!=lastPad||Math.Abs(x-oldX)>.12f||Math.Abs(z-oldZ)>.12f||Math.Abs(ax-oldAimX)>.12f||Math.Abs(az-oldAimZ)>.12f||Math.Abs(sprint-oldSprint)>.2f)UsesController=true;lastPad=buttons;oldX=x;oldZ=z;oldAimX=ax;oldAimZ=az;oldSprint=sprint;}
 public void Poll(){Previous=Buttons;Buttons=RawButtons=0;MoveX=MoveZ=AimX=AimZ=Sprint=0;PausePressed=CatchPressed=Playmaker=false;Connected=false;Controller=-1;
  State state;for(uint i=0;i<4&&!SuppressPad;i++)if(Read(i,out state)==0){Connected=true;Controller=(int)i;if(Focused){RawButtons=state.pad.buttons|(state.pad.lt>115?0x10000:0)|(state.pad.rt>115?0x20000:0);Buttons=Bindings.TranslatePad(RawButtons);Playmaker=(Buttons&0x10000)!=0;Analog(Bindings.SwapSticks?state.pad.rx:state.pad.lx,Bindings.SwapSticks?state.pad.ry:state.pad.ly,out MoveX,out MoveZ);Analog(Bindings.SwapSticks?state.pad.lx:state.pad.rx,Bindings.SwapSticks?state.pad.ly:state.pad.ry,out AimX,out AimZ);Sprint=(Buttons&0x20000)!=0?1:0;ObservePadActivity(RawButtons,MoveX,MoveZ,AimX,AimZ,Sprint);if(Playmaker)UsesController=true;}break;}
  if(!Focused)return;foreach(Keys key in KeysDown)if(!priorKeys.Contains(key))NoteKeyboard();PausePressed=K(Keys.Escape)&&!priorKeys.Contains(Keys.Escape)||FreshAction(5);CatchPressed=FreshAction(22);
  float x=(ActionKey(3)?1:0)-(ActionKey(2)?1:0),z=(ActionKey(0)?1:0)-(ActionKey(1)?1:0),l=(float)Math.Sqrt(x*x+z*z);if(l>0){MoveX=x/Math.Max(1,l);MoveZ=z/Math.Max(1,l);}if(ActionKey(4))Sprint=1;
  if(K(Keys.Enter)||ActionKey(7)&&Context!="qb")Buttons|=A;if(ActionKey(5))Buttons|=Start;if(ActionKey(6))Buttons|=View;
  if(Context=="qb"){Playmaker|=ActionKey(18);int[] pass={A,X,Y,B,RB};for(int n=0;n<5;n++)if(ActionKey(11+n))Buttons|=pass[n];if(ActionKey(16))Buttons|=LB;if(ActionKey(17))Buttons|=RClick;ApplyKeyboardAim();}
  else if(Context=="runner"||Context=="flight"){ApplyKeyboardAim();if(ActionKey(21))Buttons|=Y;if(ActionKey(19))Buttons|=X;if(ActionKey(20))Buttons|=B;if(ActionKey(16))Buttons|=LB;}
  else{if(K(Keys.Escape)||K(Keys.Back))Buttons|=B;if(ActionKey(8))Buttons|=X;if(ActionKey(9))Buttons|=Y;if(ActionKey(16))Buttons|=LB;if(ActionKey(10))Buttons|=RB;if(K(Keys.Up))Buttons|=Up;if(K(Keys.Down))Buttons|=Down;if(K(Keys.Left))Buttons|=Left;if(K(Keys.Right))Buttons|=Right;}
  priorKeys=new HashSet<Keys>(KeysDown);
 }
 public void ApplyKeyboardAim(){float x=(ActionKey(26)?1:0)-(ActionKey(25)?1:0),z=(ActionKey(23)?1:0)-(ActionKey(24)?1:0);if(x!=0||z!=0){AimX=x;AimZ=z;}}
 public void Clear(){KeysDown.Clear();priorKeys.Clear();Buttons=Previous=0;}
}
}
