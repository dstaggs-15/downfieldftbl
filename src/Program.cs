using System;
using System.IO;
using System.Windows.Forms;
namespace Downfield {
static class Program {
    [STAThread] static int Main(string[] args){
        if(args.Length>0&&args[0]=="--test")return SimulationTests.Run()+InputTests()+ProfileTests.Run()+UpdateTests.Run()+FranchiseTests.Run()+MechanicsTests.Run()+LinePlayTests.Run()+BetaTests.Run()+AudioTests.Run()+DesignerTests.Run()+ArcadeTests.Run()+OnsideTests.Run()+EconomyTests.Run()+ClubUpdateTests.Run()+PersonnelTests.Run()+DefenseBalanceTests.Run();
        if(args.Length>0&&args[0]=="--balance-test")return DefenseBalanceTests.Run();
        if(args.Length>0&&args[0]=="--audio-smoke")return AudioTests.Smoke();
        if(args.Length>1&&args[0]=="--audio-preview"){AudioTests.Preview(args[1]);return 0;}
        Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
        if(args.Length>0&&args[0]=="--ui-test")return UiTests.Run();
        try{using(GameForm form=new GameForm()){
            if(args.Length>1&&(args[0]=="--screenshot"||args[0]=="--game-screenshot")){form.Show();if(args[0]=="--game-screenshot")form.PreviewGame();Application.DoEvents();form.SavePreview(Path.GetFullPath(args[1]));return 0;}
            Application.Run(form);return 0;
        }}catch(Exception e){string path=Path.Combine(Path.GetTempPath(),"Downfield-error.txt");File.WriteAllText(path,e.ToString());MessageBox.Show("Downfield could not continue. Details saved to "+path,"Downfield");return 1;}
    }
    static int InputTests(){int failures=0;float x,z;Input.Analog(0,0,out x,out z);if(x!=0||z!=0)failures++;Input.Analog(1000,1000,out x,out z);if(x!=0||z!=0)failures++;Input.Analog(0,-32768,out x,out z);if(z>-.99f)failures++;Input.Analog(32767,32767,out x,out z);if(Math.Abs(Math.Sqrt(x*x+z*z)-1)>.001)failures++;Input.Analog(16384,0,out x,out z);if(x<.35||x>.45)failures++;Console.WriteLine("Input: 5 checks, "+failures+" failures (radial deadzone, backward, diagonal, analog magnitude)");return failures;}
}
}
