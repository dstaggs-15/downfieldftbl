using System;
using System.Windows.Forms;
namespace Downfield {
public partial class Franchise {
 public const int CustomMoveCost=45;
 public bool RelocateCustom(string city){if(!CanManageClub())return false;city=(city??"").Trim().ToUpperInvariant();while(city.Contains("  "))city=city.Replace("  "," ");if(city.Length<2||city.Length>24){Notice="City name needs 2 to 24 characters.";return false;}foreach(char c in city)if(!(c>='A'&&c<='Z'||c>='0'&&c<='9'||c==' '||c=='-')){Notice="Use letters, numbers, spaces or hyphens.";return false;}if(Teams.Exists(t=>string.Equals(t.City,city,StringComparison.OrdinalIgnoreCase))){Notice="A club already plays in that city.";return false;}if(Credits<CustomMoveCost){Notice="Custom city costs $45M: $15M move + $30M custom fee.";return false;}Credits-=CustomMoveCost;Club.City=city;Log("Moved to "+city+" / $45M. Roster and records kept.");Save();return true;}
}
public partial class GameForm {
 string cityDraft="";
 bool CityTyping(Keys key){if(screen!="cityname")return false;if(key>=Keys.A&&key<=Keys.Z||key>=Keys.D0&&key<=Keys.D9||key==Keys.Space||key==Keys.OemMinus){if(cityDraft.Length<24)cityDraft+=key==Keys.Space?' ':key==Keys.OemMinus?'-':(char)(int)key;selection=38;Invalidate();return true;}if(key==Keys.Back){if(cityDraft.Length>0)cityDraft=cityDraft.Substring(0,cityDraft.Length-1);Invalidate();return true;}return false;}
 void ActivateCityName(){string letters="ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";if(selection<36&&cityDraft.Length<24)cityDraft+=letters[selection];else if(selection==36&&cityDraft.Length<24)cityDraft+=" ";else if(selection==37&&cityDraft.Length>0)cityDraft=cityDraft.Substring(0,cityDraft.Length-1);else if(selection==38){Confirm("MOVE TO CUSTOM CITY",cityDraft+" / $45M TOTAL / $15M MOVE + $30M CUSTOM","cityname",()=>{if(franchise.RelocateCustom(cityDraft))Open("club");else Open("cityname");});}else if(selection==39)Open("relocate");}
 void DrawCityName(){FrontHeader("NAME YOUR CITY / $45M");Panel(70,170,1135,62);TextAt(cityDraft+"_",90,188,22,Gold);string letters="ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";for(int i=0;i<40;i++){float x=70+i%8*144,y=250+i/8*82;Rect(x,y,132,69,selection==i?System.Drawing.Color.FromArgb(64,101,100):System.Drawing.Color.FromArgb(38,45,62));string label=i<36?letters[i].ToString():i==36?"SPACE":i==37?"DEL":i==38?"REVIEW":"BACK";TextAt(label,x+66-PixelFont.Width(label,14)/2,y+25,14,selection==i?Gold:White);HitArea(x,y,132,69,i);}TextAt(Clip(franchise.Notice,100),70,690,11,Mint);TextAt("$15M MOVE + $30M CUSTOM CITY / YOUR PLAYERS AND HISTORY STAY",70,725,12,Gold);}
}
}
