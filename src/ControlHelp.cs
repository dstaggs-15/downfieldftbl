using System;
namespace Downfield {
public partial class GameForm {
 string ControlHint(string pad,string keyboard){return input.UsesController?pad:keyboard;}
 void DrawCurrentControls(){Rect(0,0,1280,800,Ink);TextAt("HOW TO PLAY",50,40,32,Gold);TextAt("YOUR CURRENT BINDINGS",50,98,12,Mint);bool controller=input.UsesController;Func<int,string> button=b=>input.ButtonLabel(b);Func<int,string> key=n=>input.ActionLabel(n);string[,] rows={
 {"MOVE / SPRINT",controller?(input.Bindings.SwapSticks?"RIGHT STICK / ":"LEFT STICK / ")+button(0x20000):key(0)+key(2)+key(1)+key(3)+" / "+key(4)},
 {"SNAP / PLAYBOOK",controller?button(Input.A)+" / "+button(Input.Y):key(7)+" / "+key(9)},
 {"PASS / TAP LOB / HOLD BULLET",controller?button(Input.A)+" "+button(Input.X)+" "+button(Input.Y)+" "+button(Input.B)+" "+button(Input.RB):key(11)+" "+key(12)+" "+key(13)+" "+key(14)+" "+key(15)},
 {"HANDOFF / BACKWARD LATERAL",controller?button(Input.LB):key(16)},
 {"THROW AWAY / DIRECT RECEIVER",controller?button(Input.RClick)+" / HOLD "+button(0x10000):key(17)+" / HOLD "+key(18)},
 {"JUMP / DIVE / SPIN",controller?button(Input.Y)+" / "+button(Input.X)+" / "+button(Input.B):key(21)+" / "+key(19)+" / "+key(20)},
 {"SKILLS / TRUCK UP / JUKE L-R",controller?input.Bindings.SwapSticks?"LEFT STICK":"RIGHT STICK":key(23)+" "+key(24)+" "+key(25)+" "+key(26)},
 {"AGGRESSIVE CATCH / PRESS TWICE",controller?button(Input.Y):key(22)}};for(int n=0;n<8;n++){float y=151+n*59;TextAt(rows[n,0],50,y,12,Muted);TextAt(rows[n,1],590,y,12,White);Line(50,y+38,1230,y+38,Muted,1);}TextAt("CATCH METER: GREEN 100% / YELLOW 75% / RED 4%",50,642,12,Mint);TextAt("THREE TIMEOUTS EACH HALF / PAUSE > TIMEOUT",50,680,12,Gold);TextAt("ENTER OR A BACK / CHANGE BINDINGS IN OPTIONS > CONTROLS",50,728,11,White);}
}
}
