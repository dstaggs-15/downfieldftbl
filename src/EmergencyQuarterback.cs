using System;
using System.Collections.Generic;
namespace Downfield {
public partial class Franchise {public int EmergencyQB=-1;}
public partial class Simulation {
 public bool EmergencyChoicePending,EmergencyPasser;
 public List<ClubPlayer> EmergencyCandidates(){if(Franchise==null)return new List<ClubPlayer>();List<ClubPlayer> list=Franchise.Squad(Franchise.UserTeam).FindAll(p=>p.InjuryWeeks==0);list.Sort((a,b)=>b.Power.CompareTo(a.Power));return list;}
 public bool ChooseEmergencyQB(int id){if(State!=Phase.PreSnap||Franchise==null||!EmergencyChoicePending)return false;ClubPlayer player=EmergencyCandidates().Find(p=>p.Id==id);if(player==null)return false;Franchise.EmergencyQB=id;if(!Exhibition)Franchise.Save();Setup();EmergencyChoicePending=false;return true;}
 void PutAtQuarterback(ClubPlayer p){Player qb=Players[0];foreach(Player actor in Players)if(actor.Team==0&&actor!=qb&&actor.Active&&actor.ClubId==p.Id){actor.Active=false;actor.HasBall=false;}qb.Active=true;qb.ClubId=p.Id;qb.Name=p.Name;qb.Number=p.Number;qb.OVR=p.OVR;qb.Hands=p.Hands;qb.Power=p.Power;qb.Speed=SkillSpeed(p.Role,p.Speed);qb.Primary=Franchise.Palette[Franchise.Club.Primary];qb.Secondary=Franchise.Palette[Franchise.Club.Secondary];qb.HasBall=true;Controlled=qb;EmergencyPasser=p.Role!="QB";Wildcat=true;PersonnelNotice=p.Role+" #"+p.Number+" "+p.Name+" AT QB";}
 void CheckEmergencyChoice(){EmergencyChoicePending=false;EmergencyPasser=false;if(Franchise==null)return;bool healthy=Franchise.Squad(Franchise.UserTeam).Exists(p=>p.Role=="QB"&&p.InjuryWeeks==0);if(healthy)return;ClubPlayer chosen=EmergencyCandidates().Find(p=>p.Id==Franchise.EmergencyQB);if(chosen!=null)PutAtQuarterback(chosen);else{EmergencyChoicePending=EmergencyCandidates().Count>0;if(EmergencyChoicePending&&State==Phase.GameOver)State=Phase.PreSnap;}EmergencyPasser=true;}
 void FillWildcatBack(int quarterback){ClubPlayer reserve=Franchise.Squad(Franchise.UserTeam).Find(p=>p.Role=="RB"&&p.InjuryWeeks==0&&p.Id!=quarterback&&p.Slot<0);if(reserve==null)reserve=Franchise.Squad(Franchise.UserTeam).Find(p=>p.Role=="QB"&&p.InjuryWeeks==0&&p.Slot==0);if(reserve==null)return;Player back=Players[1];back.Active=true;back.ClubId=reserve.Id;back.Name=reserve.Name;back.Number=reserve.Number;back.OVR=reserve.OVR;back.Power=reserve.Power;back.Hands=reserve.Hands;back.Speed=SkillSpeed(reserve.Role,reserve.Speed);}
 public float ThrowAbility {get{return EmergencyPasser?Math.Max(.28f,Math.Min(.58f,.28f+(Controlled.Power-70)*.008f)):TrickPasser?Math.Min(.84f,.65f+(Controlled.Power-70)*.006f):1;}}
}
}
