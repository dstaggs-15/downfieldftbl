using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace Downfield {
public class Input {
 [StructLayout(LayoutKind.Sequential)]struct Pad{public ushort buttons;public byte lt,rt;public short lx,ly,rx,ry;}
 [StructLayout(LayoutKind.Sequential)]struct State{public uint packet;public Pad pad;}
 [DllImport("xinput1_4.dll",EntryPoint="XInputGetState")]static extern uint Read4(uint i,out State s);
 [DllImport("xinput1_3.dll",EntryPoint="XInputGetState")]static extern uint Read3(uint i,out State s);
 [DllImport("xinput9_1_0.dll",EntryPoint="XInputGetState")]static extern uint Read9(uint i,out State s);
 public const int A=0x1000,B=0x2000,X=0x4000,Y=0x8000,LB=0x100,RB=0x200,Start=0x10,View=0x20,RClick=0x80,Up=1,Down=2,Left=4,Right=8;
 public HashSet<Keys> KeysDown=new HashSet<Keys>();HashSet<Keys> priorKeys=new HashSet<Keys>();
 public bool SuppressPad;public bool Connected,Focused=true,UsesController,PausePressed;public int Controller=-1,Buttons,Previous;public float MoveX,MoveZ,AimX,AimZ,Sprint;
 public string Context="menu";int provider=4,lastPad;float oldX,oldZ,oldAimX,oldAimZ,oldSprint;
 uint Read(uint i,out State s){try{if(provider==4)return Read4(i,out s);if(provider==3)return Read3(i,out s);return Read9(i,out s);}catch(DllNotFoundException){if(provider==4){provider=3;return Read(i,out s);}if(provider==3){provider=9;return Read(i,out s);}s=new State();return 1;}catch(EntryPointNotFoundException){s=new State();return 1;}}
 public static void Analog(short sx,short sy,out float x,out float z){x=sx/32767f;z=sy/32767f;float l=(float)Math.Sqrt(x*x+z*z);if(l<=.18f){x=z=0;return;}float scale=Math.Min(1,(l-.18f)/.82f)/l;x*=scale;z*=scale;}
 public bool Pressed(int b){return(Buttons&b)!=0&&(Previous&b)==0;}public bool Released(int b){return(Buttons&b)==0&&(Previous&b)!=0;}public bool Held(int b){return(Buttons&b)!=0;}
 bool K(Keys k){return KeysDown.Contains(k);}public void NoteKeyboard(){UsesController=false;}
 // Device selection is based on intentional input, not just a connected cable.
 public void ObservePadActivity(int buttons,float x,float z,float ax,float az,float sprint){if(buttons!=lastPad||Math.Abs(x-oldX)>.12f||Math.Abs(z-oldZ)>.12f||Math.Abs(ax-oldAimX)>.12f||Math.Abs(az-oldAimZ)>.12f||Math.Abs(sprint-oldSprint)>.2f)UsesController=true;lastPad=buttons;oldX=x;oldZ=z;oldAimX=ax;oldAimZ=az;oldSprint=sprint;}
 public void Poll(){Previous=Buttons;Buttons=0;MoveX=MoveZ=AimX=AimZ=Sprint=0;PausePressed=false;Connected=false;Controller=-1;
  State s;for(uint i=0;i<4&&!SuppressPad;i++)if(Read(i,out s)==0){Connected=true;Controller=(int)i;if(Focused){Buttons=s.pad.buttons;Analog(s.pad.lx,s.pad.ly,out MoveX,out MoveZ);Analog(s.pad.rx,s.pad.ry,out AimX,out AimZ);Sprint=s.pad.rt/255f;ObservePadActivity(Buttons,MoveX,MoveZ,AimX,AimZ,Sprint);}break;}
  if(!Focused)return;foreach(Keys k in KeysDown)if(!priorKeys.Contains(k))NoteKeyboard();PausePressed=K(Keys.Escape)&&!priorKeys.Contains(Keys.Escape)||K(Keys.P)&&!priorKeys.Contains(Keys.P);priorKeys=new HashSet<Keys>(KeysDown);
  float x=(K(Keys.D)?1:0)-(K(Keys.A)?1:0),z=(K(Keys.W)?1:0)-(K(Keys.S)?1:0),l=(float)Math.Sqrt(x*x+z*z);if(l>0){MoveX=x/Math.Max(1,l);MoveZ=z/Math.Max(1,l);}if(K(Keys.ShiftKey)||K(Keys.LShiftKey)||K(Keys.RShiftKey))Sprint=1;
  if(K(Keys.Enter)||K(Keys.Space))Buttons|=A;if(K(Keys.P))Buttons|=Start;if(K(Keys.M))Buttons|=View;
  if(Context=="qb"){
   if(K(Keys.D1)||K(Keys.NumPad1))Buttons|=A;if(K(Keys.D2)||K(Keys.NumPad2))Buttons|=X;if(K(Keys.D3)||K(Keys.NumPad3))Buttons|=Y;if(K(Keys.D4)||K(Keys.NumPad4))Buttons|=B;if(K(Keys.D5)||K(Keys.NumPad5))Buttons|=RB;
   if(K(Keys.Q))Buttons|=LB;if(K(Keys.T))Buttons|=RClick;ApplyKeyboardAim();
  }else if(Context=="runner"){
   ApplyKeyboardAim();if(K(Keys.J))Buttons|=Y;if(K(Keys.F))Buttons|=X;if(K(Keys.E))Buttons|=B;if(K(Keys.Q))Buttons|=LB;
  }else{
   if(K(Keys.Escape)||K(Keys.Back))Buttons|=B;if(K(Keys.X))Buttons|=X;if(K(Keys.Y))Buttons|=Y;if(K(Keys.Q))Buttons|=LB;if(K(Keys.R))Buttons|=RB;
   if(K(Keys.Up))Buttons|=Up;if(K(Keys.Down))Buttons|=Down;if(K(Keys.Left))Buttons|=Left;if(K(Keys.Right))Buttons|=Right;
  }
 }
 public void ApplyKeyboardAim(){float x=(K(Keys.Right)?1:0)-(K(Keys.Left)?1:0),z=(K(Keys.Up)?1:0)-(K(Keys.Down)?1:0);if(x!=0||z!=0){AimX=x;AimZ=z;}}public void Clear(){KeysDown.Clear();priorKeys.Clear();Buttons=Previous=0;}
}
}
