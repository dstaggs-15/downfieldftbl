using System;
using System.IO;
using System.Xml.Serialization;
namespace Downfield {
public class AudioSettings {
 public bool Muted;public float[] Gains={.75f,.9f,.25f,1,.6f,.7f};
 public static string PathName {get{return Profile.SavePath+".audio.xml";}}
 public static AudioSettings Load(){try{using(FileStream stream=File.OpenRead(PathName)){AudioSettings settings=(AudioSettings)new XmlSerializer(typeof(AudioSettings)).Deserialize(stream);if(settings.Gains==null||settings.Gains.Length!=6)return new AudioSettings();for(int i=0;i<6;i++)settings.Gains[i]=float.IsNaN(settings.Gains[i])?0:Math.Max(0,Math.Min(1,settings.Gains[i]));return settings;}}catch{return new AudioSettings();}}
 public void Save(){try{Directory.CreateDirectory(Path.GetDirectoryName(PathName));string temp=PathName+".tmp";using(FileStream stream=File.Create(temp))new XmlSerializer(typeof(AudioSettings)).Serialize(stream,this);if(File.Exists(PathName))File.Replace(temp,PathName,null);else File.Move(temp,PathName);}catch{}}
}
}
