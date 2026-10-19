using System;
namespace Downfield {
public partial class Simulation {
 bool throwingAway;
 public bool ThrowAway(){if(!CanThrow)return false;Charging=false;target=null;passLob=true;lateral=false;throwingAway=true;usedForwardPass=true;toX=Controlled.X<0?-34:34;toZ=Math.Max(Spot+2,Controlled.Z+8);passDuration=.65f;RecordAttempt();Attempts++;Cue("throw");StartFlight("THROWN AWAY");return true;}
}
}
