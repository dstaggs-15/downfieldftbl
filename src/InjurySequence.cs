using System;
using System.Collections.Generic;
namespace Downfield {
public class MatchInjury {public int PlayerId,Weeks;public string Type="";}
public partial class Simulation {
 public readonly List<MatchInjury> InjuryReports=new List<MatchInjury>();
 public Player InjuryPatient;public float AmbulanceTime,AmbulanceX,AmbulanceZ;public bool AmbulanceLoaded;public string InjuryNotice="";
 bool runoffCharged;public int LastRunoff;
 public bool AmbulanceActive {get{return InjuryPatient!=null&&State!=Phase.Live&&State!=Phase.Flight&&State!=Phase.Loose&&AmbulanceTime<4.1f;}}
 void ApplyRunoff(){if(Practice||conversion||runoffCharged)return;runoffCharged=true;LastRunoff=(int)Math.Min(4,Math.Ceiling(Clock));Clock=Math.Max(0,Clock-4);}
 void TryInjury(Player player,bool dangerous){if(player.ClubId<0||Practice||InjuryPatient!=null||MatchInjuries.Contains(player.ClubId))return;float chance=dangerous?.30f:.13f;chance=Math.Max(.035f,chance-Conditioning*.012f);if(random.NextDouble()<chance)Injure(player,dangerous);}
 void Injure(Player player,bool dangerous){int weeks=dangerous?3+random.Next(4):2+random.Next(4);MatchInjury report=new MatchInjury();report.PlayerId=player.ClubId;report.Weeks=weeks;report.Type=dangerous?"HARD LANDING":weeks>=4?"BIG HIT":"BANGED UP";InjuryReports.Add(report);MatchInjuries.Add(player.ClubId);
  if(Franchise!=null){ClubPlayer injured=Franchise.Players.Find(p=>p.Id==player.ClubId);if(injured!=null){injured.InjuryWeeks=weeks;injured.InjuryType=report.Type;}}
  player.Active=false;KnockDown(player,player.KnockVX,player.KnockVZ,Math.Max(7,Distance(0,0,player.KnockVX,player.KnockVZ)));InjuryPatient=player;AmbulanceTime=0;AmbulanceLoaded=false;AmbulanceX=-36;AmbulanceZ=player.Z;InjuryNotice=(player.Name==""?"#"+player.Number:player.Name)+" / OUT "+weeks+" WEEKS";HitLabel="INJURY / "+weeks+" WEEKS";HitFlash=.9f;Cue("crowd_groan");
 }
 void UpdateAmbulance(float dt){AmbulanceTime+=dt;float destination=InjuryPatient.X-3.5f;float approach=Clamp((AmbulanceTime-.45f)/1.2f,0,1),exit=Clamp((AmbulanceTime-2.7f)/1.4f,0,1);AmbulanceX=AmbulanceTime<2.7f?-36+(destination+36)*approach:destination+(-36-destination)*exit;AmbulanceZ=InjuryPatient.Z;AmbulanceLoaded=AmbulanceTime>=2.35f;}
 public void SkipAmbulance(){if(AmbulanceActive){AmbulanceTime=4.1f;AmbulanceLoaded=true;}}
}
}
