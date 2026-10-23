using System;
using System.IO;
using System.Windows.Forms;
namespace Downfield {
public static partial class UiTests {
 static void MedicalStaffScreens(GameForm f,string evidence){Set(f,"matchFixture",-1);Franchise club=Franchise.Create(new Profile(),new Roster());club.Credits=100;Set(f,"franchise",club);Set(f,"screen","staffhub");Set(f,"selection",3);Call(f,"ActivateSelection");Check((string)Get(f,"screen")=="medical","staff hub opens priced medical choices");f.SavePreview(Path.Combine(evidence,"medical-staff.png"));Set(f,"selection",3);Call(f,"ActivateSelection");Check((string)Get(f,"screen")=="confirm"&&((string)Get(f,"confirmDetail")).Contains("$32M"),"elite medical hire reviews exact hiring fee");Set(f,"selection",1);Call(f,"ActivateSelection");Check(club.Club.MedicalStaff==3&&club.Credits==68&&(string)Get(f,"screen")=="medical","confirmed medical staff hired through menu");f.SavePreview(Path.Combine(evidence,"medical-staff-hired.png"));Tap(f,Keys.Escape);Check((string)Get(f,"screen")=="staffhub","medical back returns to staff management");Set(f,"screen","home");}
}
}
