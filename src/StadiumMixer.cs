using System;
using System.Collections.Generic;
namespace Downfield {
// Rendered on the audio thread: ambience and individual events share one PCM stream.
public sealed class StadiumMixer {
 sealed class Voice {public float[] Samples;public int Cursor;public float Gain,Left=1,Right=1;public string Name;}
 readonly object gate=new object();readonly List<Voice> voices=new List<Voice>();readonly float[] ambience=FootballSounds.Get("ambience");
 bool muted,suspended,stadium;float volume=.75f,bedLevel;int bedCursor,variant;long rendered;
 public bool Muted {get{lock(gate)return muted;}set{lock(gate){muted=value;if(value)voices.Clear();}}}
 public bool Suspended {get{lock(gate)return suspended;}set{lock(gate){suspended=value;if(value)voices.Clear();}}}
 public bool Stadium {get{lock(gate)return stadium;}set{lock(gate){if(stadium&&!value)voices.RemoveAll(v=>v.Name.StartsWith("crowd_"));stadium=value;}}}
 public float Volume {get{lock(gate)return volume;}set{lock(gate)volume=Math.Max(0,Math.Min(1,value));}}
 public int VoiceCount {get{lock(gate)return voices.Count;}}
 public long RenderedFrames {get{lock(gate)return rendered;}}
 public void Play(string cue){lock(gate){if(muted||suspended)return;switch(cue){case "score":Add("crowd_cheer",.94f,0);Add("horn",.7f,.12f);break;case "halftime":Add("halftime",.65f,0);Add("crowd_first",.6f,0);break;case "whistle":Add(cue,.68f,.1f);break;default:Add(cue,1,0);break;}}}
 void Add(string name,float gain,float delay){bool crowd=name.StartsWith("crowd_");if(crowd){for(int i=0;i<voices.Count;i++)if(voices[i].Name=="crowd_cheer"&&name!="crowd_cheer")return;voices.RemoveAll(v=>v.Name.StartsWith("crowd_"));}else if(voices.Exists(v=>v.Name==name&&v.Cursor<FootballSounds.Rate/12))return;
  if(voices.Count>=16)voices.RemoveAt(0);Voice voice=new Voice();voice.Samples=FootballSounds.Get(name);voice.Name=name;voice.Gain=gain;voice.Cursor=-(int)(delay*FootballSounds.Rate);variant++;if(name=="hit"||name=="truck"){voice.Left=variant%2==0?1:.8f;voice.Right=variant%2==0?.8f:1;}voices.Add(voice);
 }
 public void Render(short[] output,int frames){if(output==null||frames<0||output.Length<frames*2)throw new ArgumentException("Stereo output buffer too small");lock(gate){for(int frame=0;frame<frames;frame++){
   bedLevel+=((stadium&&!muted&&!suspended?1f:0)-bedLevel)*.00025f;float bed=ambience[bedCursor++]*bedLevel*(.9f+.1f*(float)Math.Sin(rendered/(double)FootballSounds.Rate*.47));if(bedCursor>=ambience.Length)bedCursor=FootballSounds.Rate/4;
   float left=bed,right=bed;for(int i=voices.Count-1;i>=0;i--){Voice voice=voices[i];if(voice.Cursor>=0){float sample=voice.Samples[voice.Cursor]*voice.Gain;left+=sample*voice.Left;right+=sample*voice.Right;}voice.Cursor++;if(voice.Cursor>=voice.Samples.Length)voices.RemoveAt(i);}
   bool silent=muted||suspended;output[frame*2]=silent?(short)0:(short)(Math.Tanh(left*volume)*28000);output[frame*2+1]=silent?(short)0:(short)(Math.Tanh(right*volume)*28000);rendered++;
  }}}
}
}
