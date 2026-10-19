using System;
namespace Downfield {
public partial class DrawnPlay {
 public int BasePlay;
 public static DrawnPlay Default(int basePlay,Simulation game){DrawnPlay play=new DrawnPlay();play.BasePlay=Simulation.IsRunCall(basePlay)?0:basePlay;for(int i=0;i<5;i++){Player p=game.Receiver(i);play.Routes.Add(Preset(i,i==3?"FLAT":i==2?"OUT":i==4?"CROSS":"GO",p.StartX,p.StartZ-game.Spot));}return play;}
 public static DrawnRoute Preset(int receiver,string name,float x,float z){DrawnRoute route=new DrawnRoute();route.Receiver=receiver;route.Label=name;float inside=x<0?1:-1,outside=-inside;Action<float,float> point=(a,b)=>route.Points.Add(new RouteNode(Math.Max(-25,Math.Min(25,a)),Math.Max(-8,Math.Min(40,b))));
  switch(name){case "GO":point(x,z+30);break;case "SLANT":point(x,z+4);point(x+inside*12,z+15);break;case "OUT":point(x,z+9);point(x+outside*8,z+9);route.Hold=true;break;case "IN":point(x,z+9);point(x+inside*13,z+9);route.Hold=true;break;case "CROSS":point(x,z+5);point(inside*19,z+7);route.Hold=true;break;case "CURL":point(x,z+13);point(x+inside*2,z+10);route.Hold=true;break;case "POST":point(x,z+10);point(x+inside*13,z+29);break;case "CORNER":point(x,z+10);point(x+outside*10,z+26);break;case "FLAT":point(x+outside*10,3);route.Hold=true;break;}
  return route;
 }
}
}
