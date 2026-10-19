using System;
namespace Downfield {
public partial class Simulation {
 public bool OnsideAttempt,OnsideRulesShown,OnsideRulesPending;public int OnsideAttempts,OnsideConversions;public string LastOnsideResult="";
 public bool CanOnsideTry {get{return !Practice&&!conversion&&!OnsideAttempt&&!OnsideRulesPending&&!AmbulanceActive&&(State==Phase.PreSnap||State==Phase.Kickoff);}}
 public bool RequestOnsideTry(){if(!CanOnsideTry)return false;if(!OnsideRulesShown){OnsideRulesShown=true;OnsideRulesPending=true;return true;}StartOnsideTry();return true;}
 public void ConfirmOnsideTry(){if(!OnsideRulesPending)return;OnsideRulesPending=false;StartOnsideTry();}
 public void CancelOnsideTry(){OnsideRulesPending=false;}
 void StartOnsideTry(){OnsideAttempt=true;OnsideAttempts++;Spot=30;Down=4;FirstDown=43;conversion=false;Setup();Message="ONSIDE TRY / 4TH & 13 / OWN 30";Log(Message);}
 public void NormalKickoff(){if(State!=Phase.Kickoff||OnsideRulesPending)return;opponentStart=20;BeginOpponent("KICKOFF / OPPONENT AT 20",true);}
 void AfterUserScore(string result){OnsideAttempt=OnsideRulesPending=false;LastPlay=result;Log(result);Charging=false;handoffTime=-1;if(Clock<=0){CheckQuarter();if(State==Phase.Halftime||State==Phase.GameOver)return;}State=Phase.Kickoff;Message=result+" / CHOOSE KICKOFF";}
 void ConvertOnside(){if(!OnsideAttempt)return;OnsideAttempt=false;OnsideConversions++;LastOnsideResult="ONSIDE TRY CONVERTED";}
 void ResetOnside(){OnsideAttempt=OnsideRulesShown=OnsideRulesPending=false;OnsideAttempts=OnsideConversions=0;LastOnsideResult="";}
}
}
