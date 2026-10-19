using System;
namespace Downfield {
public sealed class GameAudio:IDisposable {
 internal readonly StadiumMixer Mix;readonly WaveOutput output;
 public string LastCue="";public int Played;
 public bool Muted {get{return Mix.Muted;}set{Mix.Muted=value;}}
 public bool OutputAvailable {get{return output!=null&&output.Available;}}
 public string OutputError {get{return output==null?"Playback suppressed for tests":output.Error;}}
 public int WrittenBuffers {get{return output==null?0:output.WrittenBuffers;}}
 public GameAudio(){foreach(string name in new string[]{"ambience","crowd_cheer","crowd_big","crowd_first","crowd_groan"})FootballSounds.Get(name);Mix=new StadiumMixer();if(Profile.StorageOverride==null)output=new WaveOutput(Mix);}
 public static byte[] CreateWave(string cue){return FootballSounds.Wave(FootballSounds.Get(cue));}
 public void SetContext(bool focused,bool stadium){Mix.Suspended=!focused;Mix.Stadium=stadium&&focused;}
 public void Play(string cue){LastCue=cue;Played++;Mix.Play(cue);}
 public void Dispose(){if(output!=null)output.Dispose();}
}
}