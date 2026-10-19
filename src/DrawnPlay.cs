using System;
using System.Collections.Generic;
namespace Downfield {
public class RouteNode {public float X,Z;public RouteNode(){}public RouteNode(float x,float z){X=x;Z=z;}}
public class DrawnRoute {public string Label="CUSTOM";public bool Hold;public int Receiver;public List<RouteNode> Points=new List<RouteNode>();}
public partial class DrawnPlay {public string Name="CUSTOM PLAY";public List<DrawnRoute> Routes=new List<DrawnRoute>();public DrawnPlay Copy(){DrawnPlay p=new DrawnPlay();p.Name=Name;p.BasePlay=BasePlay;foreach(DrawnRoute r in Routes){DrawnRoute copy=new DrawnRoute();copy.Receiver=r.Receiver;copy.Label=r.Label;copy.Hold=r.Hold;foreach(RouteNode n in r.Points)copy.Points.Add(new RouteNode(n.X,n.Z));p.Routes.Add(copy);}return p;}}
public partial class Franchise {public DrawnPlay SavedPlay=new DrawnPlay();}
public partial class Simulation {
 public DrawnPlay CustomPlay;public bool UseCustom;
 float[] CustomPoint(Player p,float t){if(!UseCustom||CustomPlay==null)return null;int receiver=-1;for(int i=0;i<5;i++){Player original=Receiver(i);if(p.Number==original.Number&&p.StartX==original.StartX){receiver=i;break;}}DrawnRoute route=CustomPlay.Routes.Find(r=>r.Receiver==receiver);if(route==null||route.Points.Count==0)return null;float x=p.StartX,z=p.StartZ-Spot,distance=Math.Max(0,t)*p.Speed;foreach(RouteNode n in route.Points){float length=Distance(x,z,n.X,n.Z);if(length>.01f&&distance<=length)return new float[]{x+(n.X-x)*distance/length,Spot+z+(n.Z-z)*distance/length};distance-=length;x=n.X;z=n.Z;}return new float[]{x,Spot+z+(route.Hold?0:Math.Min(20,Math.Max(0,distance)))};}
}
}
