using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace DownfieldSetup {
public static class InstallEngine {
 public static string DefaultFolder {get{return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Downfield 2");}}
 public static string DesktopFolder {get{return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);}}
 static byte[] Payload(string name){using(Stream input=Assembly.GetExecutingAssembly().GetManifestResourceStream("Downfield."+name)){if(input==null)throw new InvalidDataException("Installer is missing "+name);using(MemoryStream output=new MemoryStream()){input.CopyTo(output);return output.ToArray();}}}
 static void Put(string path,byte[] bytes){string temp=path+".installing";File.WriteAllBytes(temp,bytes);try{if(File.Exists(path))File.Replace(temp,path,null);else File.Move(temp,path);}catch{if(File.Exists(temp))File.Delete(temp);throw;}}
 public static void Install(string folder,string desktop){if(!Environment.Is64BitOperatingSystem)throw new PlatformNotSupportedException("Downfield needs 64-bit Windows.");folder=Path.GetFullPath(folder);desktop=Path.GetFullPath(desktop);Directory.CreateDirectory(folder);Directory.CreateDirectory(desktop);byte[] game=Payload("Game");Put(Path.Combine(folder,"Downfield.exe"),game);Put(Path.Combine(folder,"START HERE.txt"),Payload("Readme"));Put(Path.Combine(folder,"FRANCHISE-GUIDE.md"),Payload("Guide"));Put(Path.Combine(folder,"LICENSE.txt"),Payload("License"));Put(Path.Combine(desktop,"Downfield 2.exe"),game);}
#if DOWNFIELD_INSTALLER_TESTS
 static string Hash(byte[] bytes){using(SHA256 sha=SHA256.Create())return Convert.ToBase64String(sha.ComputeHash(bytes));}
 public static int SelfTest(){string root=Path.Combine(Path.GetTempPath(),"Downfield-install-test-"+Guid.NewGuid().ToString("N"));try{string app=Path.Combine(root,"app"),desktop=Path.Combine(root,"desktop");Install(app,desktop);byte[] game=Payload("Game");if(Hash(game)!=Hash(File.ReadAllBytes(Path.Combine(app,"Downfield.exe")))||Hash(game)!=Hash(File.ReadAllBytes(Path.Combine(desktop,"Downfield 2.exe"))))throw new Exception("Installed game differs from packaged game");if(!File.Exists(Path.Combine(app,"FRANCHISE-GUIDE.md"))||!File.Exists(Path.Combine(app,"LICENSE.txt")))throw new Exception("Missing instructions/license");File.WriteAllText(Path.Combine(app,"keep-my-file.txt"),"keep");File.WriteAllText(Path.Combine(desktop,"Downfield 2.exe"),"old version");Install(app,desktop);if(Hash(game)!=Hash(File.ReadAllBytes(Path.Combine(desktop,"Downfield 2.exe")))||File.ReadAllText(Path.Combine(app,"keep-my-file.txt"))!="keep")throw new Exception("Repair installation failed");return 0;}catch(Exception e){Console.Error.WriteLine(e);return 1;}finally{string full=Path.GetFullPath(root),allowed=Path.Combine(Path.GetTempPath(),"Downfield-install-test-");if(full.StartsWith(allowed,StringComparison.OrdinalIgnoreCase)&&Directory.Exists(full))Directory.Delete(full,true);}}
#endif
}
public class SetupForm:Form {
 readonly Label status=new Label();readonly Button install=new Button();bool done;
 public SetupForm(){Text="Install Downfield 2";ClientSize=new Size(680,470);FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;StartPosition=FormStartPosition.CenterScreen;BackColor=Color.FromArgb(24,27,46);ForeColor=Color.FromArgb(246,235,203);Font=new Font("Consolas",11);
  Add("DOWNFIELD 2",30,26,620,45,26,Color.FromArgb(247,195,85));Add("LET'S GET YOU ON THE FIELD",32,80,610,28,13,ForeColor);Add("Windows 10 / 11  |  64-bit  |  No admin needed",32,124,610,25,11,Color.FromArgb(117,220,162));Add("This puts Downfield 2.exe on your desktop.\nThe game, guide and license also go here:",32,172,610,48,11,ForeColor);Add(InstallEngine.DefaultFolder,32,229,610,50,10,Color.FromArgb(155,170,162));Add("No accounts, launcher or game downloads.\nYour existing team save stays where it is.",32,289,610,50,11,ForeColor);
  status.SetBounds(32,350,610,44);status.ForeColor=Color.FromArgb(117,220,162);Controls.Add(status);install.Text="INSTALL GAME";install.SetBounds(32,405,280,42);install.FlatStyle=FlatStyle.Flat;install.BackColor=Color.FromArgb(64,101,100);install.Click+=(s,e)=>InstallClick();Controls.Add(install);Button close=new Button();close.Text="CLOSE";close.SetBounds(480,405,160,42);close.FlatStyle=FlatStyle.Flat;close.Click+=(s,e)=>Close();Controls.Add(close);
 }
 void Add(string text,int x,int y,int w,int h,float size,Color color){Label label=new Label();label.Text=text;label.SetBounds(x,y,w,h);label.ForeColor=color;label.Font=new Font("Consolas",size);Controls.Add(label);}
 void InstallClick(){if(done){Process.Start(Path.Combine(InstallEngine.DesktopFolder,"Downfield 2.exe"));Close();return;}try{install.Enabled=false;status.Text="Installing...";Application.DoEvents();InstallEngine.Install(InstallEngine.DefaultFolder,InstallEngine.DesktopFolder);done=true;status.Text="You're ready. Look for Downfield 2 on your desktop.";install.Text="PLAY NOW";}catch(Exception e){status.ForeColor=Color.FromArgb(247,195,85);status.Text="Couldn't finish. Close Downfield and try again.";MessageBox.Show(e.Message,"Downfield setup",MessageBoxButtons.OK,MessageBoxIcon.Information);}finally{install.Enabled=true;}}
}
static class Program {
 [STAThread]static int Main(string[] args){
#if DOWNFIELD_INSTALLER_TESTS
  if(args.Length>0&&args[0]=="--self-test")return InstallEngine.SelfTest();
#endif
  Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);using(SetupForm form=new SetupForm()){
#if DOWNFIELD_INSTALLER_TESTS
   if(args.Length>1&&args[0]=="--screenshot"){form.Show();Application.DoEvents();using(Bitmap b=new Bitmap(form.ClientSize.Width,form.ClientSize.Height)){form.DrawToBitmap(b,new Rectangle(Point.Empty,form.ClientSize));b.Save(Path.GetFullPath(args[1]));}return 0;}
#endif
   Application.Run(form);}return 0;}
}
}
