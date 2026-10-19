namespace Downfield {
public partial class Simulation {
 Player flightThrower;public bool BallCrossedScrimmage,HandoffOccurred;
 void TrackScrimmage(){if((State==Phase.Live&&Controlled!=null&&Controlled.HasBall&&Controlled.Z>Spot)||(State==Phase.Flight||State==Phase.Loose)&&BallZ>Spot)BallCrossedScrimmage=true;}
 void ReturnedPitch(Player receiver){if(receiver.Role!="QB"||!HandoffOccurred||usedForwardPass||BallCrossedScrimmage||receiver.Z>Spot)return;RunPlay=false;if(IsRunCall(Playbook))Playbook=0;OptionGiven=true;Message="FLEA FLICKER / QB CAN PASS!";}
}
}
