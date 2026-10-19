using System.Drawing;
namespace Downfield {
public partial class GameForm {
 bool TickOnsideChoice(){if(game.OnsideRulesPending){if(input.Pressed(Input.A))game.ConfirmOnsideTry();else if(input.Pressed(Input.B)||input.PausePressed)game.CancelOnsideTry();Invalidate();return true;}if(game.State!=Phase.Kickoff||game.AmbulanceActive||input.Pressed(Input.Start)||input.PausePressed)return false;if(input.Pressed(Input.A))game.NormalKickoff();else if(input.Pressed(Input.X))game.RequestOnsideTry();Invalidate();return true;}
 bool OnsideMouseAction(int index){if(index==201){game.NormalKickoff();return true;}if(index==202){game.RequestOnsideTry();return true;}if(index==203){game.ConfirmOnsideTry();return true;}if(index==204){game.CancelOnsideTry();return true;}return false;}
 void DrawOnsideChoices(){if(game.OnsideAttempt){Rect(500,165,360,32,Ink);TextAt("ONSIDE TRY / GAIN 13 TO KEEP IT",510,175,11,Gold);}if(game.CanOnsideTry&&game.State==Phase.PreSnap){DesignButton("ONSIDE TRY / RB",990,174,260,40,202,false);}
  if(game.OnsideRulesPending){Rect(0,105,1280,663,Color.FromArgb(210,15,20,36));Panel(210,175,860,462);Center("ONSIDE TRY",198,30,Gold);Center("ONE PLAY. KEEP THE BALL IF YOU CONVERT.",246,14,White);
   Rect(302,292,676,126,Color.FromArgb(35,78,56));for(int i=0;i<8;i++)Line(315+i*90,299,315+i*90,411,Color.FromArgb(100,150,110),1);Line(410,305,410,403,Blue,4);Line(800,305,800,403,Gold,4);Line(430,352,774,352,Mint,5);Line(755,338,774,352,Mint,4);Line(755,366,774,352,Mint,4);TextAt("OWN 30",365,319,12,White);TextAt("OWN 43",754,319,12,Gold);TextAt("4TH & 13",555,373,18,White);
   Center("CONVERT: FIRST DOWN. KEEP YOUR DRIVE.",444,13,Mint);Center("FAIL: TURNOVER ON DOWNS AT THE DEAD-BALL SPOT.",477,12,White);Center("AVAILABLE ANY QUARTER, ANY SCORE. ONE PLAY PER TRY.",508,11,Muted);DesignButton(Hint("A START TRY","ENTER START TRY"),266,559,355,48,203,true);DesignButton(Hint("B BACK","ESC BACK"),658,559,355,48,204,false);return;}
  if(game.State==Phase.Kickoff&&!game.AmbulanceActive){Panel(218,285,844,285);Center("YOUR KICKOFF CHOICE",308,24,Gold);Center("KICK IT AWAY OR PLAY 4TH & 13 FROM YOUR OWN 30",355,12,White);DesignButton(Hint("A NORMAL KICKOFF","ENTER NORMAL KICKOFF"),258,403,356,66,201,true);DesignButton(Hint("X ONSIDE TRY","X ONSIDE TRY"),654,403,366,66,202,false);Center("NO QUARTER, SCORE OR ATTEMPT LIMIT",516,11,Mint);}
 }
}
}
