using System;
using System.Collections.Generic;
using System.IO;
namespace Downfield {
// Original contact/UI synthesis; the crowd uses embedded CC0 field recordings.
public static class FootballSounds {
 public const int Rate=22050;
 static readonly Dictionary<string,float[]> cache=new Dictionary<string,float[]>();
 public static float[] Get(string name){lock(cache){float[] clip;if(!cache.TryGetValue(name,out clip)){clip=Make(name);cache[name]=clip;}return clip;}}
 static float[] Make(string name){
  if(CrowdRecordings.IsCrowd(name))return CrowdRecordings.Get(name);
  double duration=name=="slam"?.65:name=="horn"?1.3:name=="halftime"?.8:name=="score"?.75:name=="whistle"?.12:name=="truck"?.48:name=="hit"?.34:.16;
  float[] samples=new float[(int)(Rate*duration)];Random random=new Random(714);double low=0,previous=0;
  for(int i=0;i<samples.Length;i++){double t=(double)i/Rate,p=t/duration,value=0,noise=random.NextDouble()*2-1;low+=.12*(noise-low);
   if(name=="hit"||name=="truck"||name=="slam"){double strength=name=="slam"?1.65:name=="truck"?1.3:1;value=(Math.Sin(2*Math.PI*(92*t-80*t*t))*Math.Exp(-t*21)*.62+(noise-previous)*Math.Exp(-t*60)*.18+low*Math.Exp(-t*12)*.65)*strength;previous=noise;}
   else if(name=="snap"||name=="catch"||name=="kick"){double frequency=name=="kick"?100:180;value=Math.Sin(t*frequency*2*Math.PI)*Math.Exp(-t*40)*.38+noise*Math.Exp(-t*75)*.2;}
   else if(name=="whistle"){value=(low*.4+Math.Sin(t*180*2*Math.PI)*.24)*Math.Exp(-t*45);}
   else if(name=="horn"||name=="halftime"){double frequency=name=="horn"?196:p<.45?220:164.81;value=(Math.Sin(t*frequency*2*Math.PI)+.3*Math.Sin(t*frequency*4*Math.PI)+.12*Math.Sin(t*frequency*6*Math.PI))*.18*Math.Min(1,t*28)*Math.Min(1,(duration-t)*7);}
   else{double frequency=name=="jump"?340+p*650:name=="throw"?650-p*400:name=="menu"?520:name=="confirm"?760:name=="score"?new int[]{523,659,784,1046}[(int)(p*4)%4]:440;value=Math.Sin(t*frequency*2*Math.PI)*Math.Min(1,t*120)*(1-p)*.17;}
   samples[i]=(float)Math.Tanh(value);
  }return samples;
 }
 public static byte[] Wave(float[] samples){using(MemoryStream stream=new MemoryStream())using(BinaryWriter w=new BinaryWriter(stream)){w.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));w.Write(36+samples.Length*2);w.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));w.Write(16);w.Write((short)1);w.Write((short)1);w.Write(Rate);w.Write(Rate*2);w.Write((short)2);w.Write((short)16);w.Write(System.Text.Encoding.ASCII.GetBytes("data"));w.Write(samples.Length*2);foreach(float sample in samples)w.Write((short)(Math.Max(-.98,Math.Min(.98,sample))*32767));return stream.ToArray();}}
}
}
