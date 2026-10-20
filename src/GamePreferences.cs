using System;
using System.IO;
using System.Xml.Serialization;
namespace Downfield {
public class GamePreferences {
 public int Difficulty=1;
 public static readonly string[] DifficultyNames={"ROOKIE","NORMAL","HARD","EXTREME"};
 static string PathName {get{return Profile.SavePath+".settings.xml";}}
 public static GamePreferences Load(){try{using(FileStream f=File.OpenRead(PathName)){GamePreferences p=(GamePreferences)new XmlSerializer(typeof(GamePreferences)).Deserialize(f);p.Difficulty=Math.Max(0,Math.Min(3,p.Difficulty));return p;}}catch{return new GamePreferences();}}
 public void Save(){try{Directory.CreateDirectory(Path.GetDirectoryName(PathName));using(FileStream f=File.Create(PathName))new XmlSerializer(typeof(GamePreferences)).Serialize(f,this);}catch{}}
}
public partial class GameForm {
 void ChangeDifficulty(){difficulty=(difficulty+1)%4;game.Difficulty=difficulty;new GamePreferences{Difficulty=difficulty}.Save();}
}
}
