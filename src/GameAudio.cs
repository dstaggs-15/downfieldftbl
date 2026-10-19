using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
namespace Downfield {
// Original generated waveforms: no copyrighted recordings or audio downloads are needed.
public class GameAudio:IDisposable {
 readonly Dictionary<string,SoundPlayer> players=new Dictionary<string,SoundPlayer>();readonly List<MemoryStream> streams=new List<MemoryStream>();
 public bool Muted;public string LastCue="";public int Played;
 public GameAudio(){string[] names={"menu","confirm","snap","throw","catch","hit","truck","jump","kick","score","whistle","halftime"};for(int i=0;i<names.Length;i++){MemoryStream stream=new MemoryStream(CreateWave(names[i]));streams.Add(stream);players[names[i]]=new SoundPlayer(stream);}}
 public static byte[] CreateWave(string name){int sampleRate=22050;double seconds=name=="score"?.8:name=="halftime"?.65:name=="hit"||name=="truck"?.26:.14;int count=(int)(sampleRate*seconds);Random noise=new Random(714);using(MemoryStream output=new MemoryStream())using(BinaryWriter w=new BinaryWriter(output)){w.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));w.Write(36+count*2);w.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));w.Write(16);w.Write((short)1);w.Write((short)1);w.Write(sampleRate);w.Write(sampleRate*2);w.Write((short)2);w.Write((short)16);w.Write(System.Text.Encoding.ASCII.GetBytes("data"));w.Write(count*2);for(int i=0;i<count;i++){double t=(double)i/sampleRate,part=t/seconds;double frequency=name=="jump"?340+part*650:name=="throw"?650-part*400:name=="kick"?240-part*150:name=="catch"?880:name=="menu"?520:name=="confirm"?760:name=="whistle"?1800:name=="snap"?130:name=="score"?new int[]{523,659,784,1046}[(int)(part*4)%4]:name=="halftime"?new int[]{784,659,523}[(int)(part*3)%3]:75;double tone=Math.Sin(t*frequency*2*Math.PI),envelope=Math.Min(1,t*120)*(1-part);if(name=="hit"||name=="truck")tone=tone*.4+(noise.NextDouble()*2-1)*.6;if(name=="snap")tone=noise.NextDouble()*2-1;if(name=="score"||name=="menu")tone=Math.Sign(tone)*.55;w.Write((short)(tone*envelope*6500));}return output.ToArray();}}
 public void Play(string cue){LastCue=cue;Played++;if(Muted||Profile.StorageOverride!=null)return;SoundPlayer player;if(players.TryGetValue(cue,out player))try{player.Play();}catch(Exception){}}
 public void Dispose(){foreach(SoundPlayer p in players.Values)p.Dispose();foreach(MemoryStream s in streams)s.Dispose();}
}
}
