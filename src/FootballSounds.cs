using System;
using System.Collections.Generic;
using System.IO;
namespace Downfield {
// Original stadium synthesis. No third-party recordings or runtime downloads.
public static class FootballSounds {
 public const int Rate=22050;
 static readonly Dictionary<string,float[]> cache=new Dictionary<string,float[]>();
 public static float[] Get(string name){lock(cache){float[] clip;if(!cache.TryGetValue(name,out clip)){clip=Make(name);cache[name]=clip;}return clip;}}
 static float[] Make(string name){
  if(name=="ambience")return Crowd(8,91,.16f,false,false);
  if(name=="crowd_cheer")return Crowd(4.3,724,.57f,true,false);
  if(name=="crowd_big")return Crowd(2.3,163,.48f,true,false);
  if(name=="crowd_first")return Crowd(2.4,351,.36f,true,false);
  if(name=="crowd_groan")return Crowd(2.6,267,.38f,false,true);
  double duration=name=="horn"?1.3:name=="halftime"?.8:name=="score"?.75:name=="whistle"?.32:name=="truck"?.48:name=="hit"?.34:.16;
  float[] samples=new float[(int)(Rate*duration)];Random random=new Random(714);double low=0,previous=0;
  for(int i=0;i<samples.Length;i++){double t=(double)i/Rate,p=t/duration,value=0,noise=random.NextDouble()*2-1;low+=.12*(noise-low);
   if(name=="hit"||name=="truck"){double strength=name=="truck"?1.3:1;value=(Math.Sin(2*Math.PI*(92*t-80*t*t))*Math.Exp(-t*21)*.62+(noise-previous)*Math.Exp(-t*60)*.18+low*Math.Exp(-t*12)*.65)*strength;previous=noise;}
   else if(name=="snap"||name=="catch"||name=="kick"){double frequency=name=="kick"?100:180;value=Math.Sin(t*frequency*2*Math.PI)*Math.Exp(-t*40)*.38+noise*Math.Exp(-t*75)*.2;}
   else if(name=="whistle"){double envelope=Math.Min(1,t*130)*Math.Min(1,(duration-t)*50);value=(Math.Sin(t*3050*2*Math.PI)+.5*Math.Sin(t*3227*2*Math.PI))*(.68+.32*Math.Sin(t*36*2*Math.PI))*envelope*.16;}
   else if(name=="horn"||name=="halftime"){double frequency=name=="horn"?196:p<.45?220:164.81;value=(Math.Sin(t*frequency*2*Math.PI)+.3*Math.Sin(t*frequency*4*Math.PI)+.12*Math.Sin(t*frequency*6*Math.PI))*.18*Math.Min(1,t*28)*Math.Min(1,(duration-t)*7);}
   else{double frequency=name=="jump"?340+p*650:name=="throw"?650-p*400:name=="menu"?520:name=="confirm"?760:name=="score"?new int[]{523,659,784,1046}[(int)(p*4)%4]:440;value=Math.Sin(t*frequency*2*Math.PI)*Math.Min(1,t*120)*(1-p)*.17;}
   samples[i]=(float)Math.Tanh(value);
  }return samples;
 }
 static float[] Crowd(double seconds,int seed,float level,bool cheering,bool groan){
  int length=(int)(seconds*Rate);float[] samples=new float[length];Random random=new Random(seed);double smooth=0;
  // Many drifting, staggered vowel-like voices make a roar rather than a pitched beep.
  for(int voice=0;voice<28;voice++){double pitch=95+random.NextDouble()*180,phase=random.NextDouble()*6.28,drift=random.NextDouble()*6.28,offset=random.NextDouble()*6.28;double amplitude=.029+random.NextDouble()*.018;
   for(int i=0;i<length;i++){double t=(double)i/Rate,progress=t/seconds,energy=.45+.55*Math.Pow(Math.Sin(t*(1.7+voice%5*.22)+offset),2),swell=groan?1-progress*.34:1+Math.Sin(progress*Math.PI)*.2;phase+=2*Math.PI*pitch*swell*(1+.022*Math.Sin(t*7+drift))/Rate;if(phase>Math.PI*2)phase-=Math.PI*2;double vowel=Math.Sin(phase)+.46*Math.Sin(phase*2)+.24*Math.Sin(phase*5)+.12*Math.Sin(phase*7);samples[i]+=(float)(vowel*energy*amplitude);}
  }
  // Diffuse crowd wash, scattered handclaps, then a short stadium echo.
  for(int i=0;i<length;i++){double noise=random.NextDouble()*2-1;smooth+=.19*(noise-smooth);samples[i]+=(float)(smooth*.5);}
  int claps=(int)(seconds*(cheering?35:groan?4:8));for(int c=0;c<claps;c++){int start=random.Next(length),duration=500+random.Next(600);double volume=.07+random.NextDouble()*.12;for(int i=0;i<duration&&start+i<length;i++)samples[start+i]+=(float)((random.NextDouble()*2-1)*Math.Exp(-i/110.0)*volume);}
  int delay=(int)(Rate*.13);for(int i=length-1;i>=delay;i--)samples[i]+=samples[i-delay]*.22f;
  for(int i=0;i<length;i++){double t=(double)i/Rate,envelope=cheering||groan?Math.Min(1,t/.18)*Math.Min(1,(seconds-t)/1.15):1;samples[i]=(float)(Math.Tanh(samples[i]*1.6)*level*envelope);}
  if(!cheering&&!groan){int cross=Rate/4;for(int i=0;i<cross;i++){float mix=(float)i/cross;samples[length-cross+i]=samples[length-cross+i]*(1-mix)+samples[i]*mix;}}
  return samples;
 }
 public static byte[] Wave(float[] samples){using(MemoryStream stream=new MemoryStream())using(BinaryWriter w=new BinaryWriter(stream)){w.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));w.Write(36+samples.Length*2);w.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));w.Write(16);w.Write((short)1);w.Write((short)1);w.Write(Rate);w.Write(Rate*2);w.Write((short)2);w.Write((short)16);w.Write(System.Text.Encoding.ASCII.GetBytes("data"));w.Write(samples.Length*2);foreach(float sample in samples)w.Write((short)(Math.Max(-.98,Math.Min(.98,sample))*32767));return stream.ToArray();}}
}
}
