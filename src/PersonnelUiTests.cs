using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Downfield {
public static partial class UiTests {
 static void PersonnelScreens(GameForm f,string evidence){Franchise club=Franchise.Create(new Profile(),new Roster());club.UserTeam=1;Set(f,"franchise",club);ClubPlayer starter=club.Starter(club.UserTeam,1),reserve=club.Players.Find(p=>p.Team==club.UserTeam&&p.Role=="RB"&&p.Slot<0);starter.InjuryWeeks=3;Call(f,"StartGame");Simulation s=(Simulation)Get(f,"game");Check(s.Players[1].ClubId==reserve.Id&&s.Players[1].Number!=starter.Number,"replacement on field uses reserve identity and jersey");f.SavePreview(Path.Combine(evidence,"injury-substitution.png"));reserve.InjuryWeeks=2;Call(s,"Setup");Check(s.OnFieldCount(0)==10&&!s.Players[1].Active,"exhausted RB depth shows ten actual players");f.SavePreview(Path.Combine(evidence,"short-handed.png"));Tap(f,Keys.X);Tap(f,Keys.Space);Check(s.Controlled.Role=="QB"&&s.Controlled.HasBall&&!s.HandoffPending,"short-handed run call keeps ball with real QB");
  starter.InjuryWeeks=reserve.InjuryWeeks=0;Call(f,"StartPractice");s=new Simulation(120);s.Practice=true;s.NewGame();Set(f,"game",s);s.Players[0].Hands=99;Tap(f,Keys.X);Tap(f,Keys.Space);Step(f,Keys.None,false,10);Tap(f,Keys.Q);for(int i=0;i<20&&s.State==Phase.Flight;i++)Step(f,Keys.None,false,1);Check(s.State==Phase.Live&&s.Controlled.Role=="QB"&&s.CanThrow&&!s.RunPlay,"RB pitch back gives QB passing controls and receiver routes");f.SavePreview(Path.Combine(evidence,"flea-flicker-ready.png"));Tap(f,Keys.D1);Check(s.State==Phase.Flight&&s.Attempts==1,"flea flicker forward pass works through keyboard input");f.SavePreview(Path.Combine(evidence,"flea-flicker-pass.png"));Set(f,"screen","home");Set(f,"matchFixture",-1);}
}
}
