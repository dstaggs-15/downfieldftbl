using System;
using System.Collections.Generic;
using System.Drawing;
namespace Downfield {
public partial class Simulation {
 public bool DepthPersonnelPending;
 void RefreshDepthRatings(){if(DepthPersonnelPending&&Franchise!=null)Defense=Franchise.Defense+Franchise.Club.CoachDef+(Franchise.Rating(Franchise.UserTeam,true)-75)/4;}
 public void RefreshDepthPersonnel(){if(DepthPersonnelPending&&State==Phase.PreSnap){Setup();DepthPersonnelPending=false;}}
}
public partial class GameForm {
 string depthBack="roster";int depthSlot;
 Franchise DepthClub {get{return depthBack=="pause"?game.Franchise:franchise;}}
 void OpenDepthChart(string back){if(back=="pause"&&game.Franchise==null){game.Message="NO FRANCHISE ROSTER IN THIS MODE";return;}depthBack=back;Open("depthchart");}
 List<ClubPlayer> DepthCandidates(){Franchise f=DepthClub;List<ClubPlayer> list=f.Squad(f.UserTeam).FindAll(p=>p.Role==Franchise.Roles[depthSlot]);list.Sort((a,b)=>{int healthy=(a.InjuryWeeks>0).CompareTo(b.InjuryWeeks>0);return healthy!=0?healthy:b.OVR.CompareTo(a.OVR);});return list;}
 void ActivateDepthChart(){if(screen=="depthchart"){if(selection==22){Open(depthBack);return;}depthSlot=selection;Open("depthpick");return;}List<ClubPlayer> list=DepthCandidates();if(selection<list.Count){ClubPlayer p=list[selection];if(p.InjuryWeeks>0){DepthClub.Notice="Injured players cannot be put in the active lineup.";return;}DepthClub.StartPlayer(p.Id,depthSlot,depthBack!="pause"||!game.Exhibition);if(depthBack=="pause")game.DepthPersonnelPending=true;}Open("depthchart");selection=depthSlot;}
 string DepthName(ClubPlayer p){return p==null?"VACANT":p.InjuryWeeks>0?"#"+p.Number+" "+p.Name+" / OUT "+p.InjuryWeeks+"W":"#"+p.Number+" "+p.Name+" / "+p.OVR+" OVR";}
 void DrawDepthChart(){Franchise f=DepthClub;Rect(0,0,1280,800,Ink);Rect(0,0,1280,8,Color.FromArgb(255,Color.FromArgb(Franchise.Palette[f.Club.Primary])));TextAt("DOWNFIELD / "+f.Club.Name,40,30,12,Muted);TextAt(screen=="depthchart"?"DEPTH CHART":"CHOOSE "+Franchise.Roles[depthSlot]+" / SLOT "+(depthSlot+1),40,66,28,Gold);TextAt(depthBack=="pause"?"CHANGES APPLY AT THE NEXT SNAP. CURRENT PLAY IS PRESERVED.":"SELECT A SLOT TO CHANGE STARTERS. HEALTHY RESERVES COVER INJURIES.",40,115,11,Mint);
  if(screen=="depthchart"){TextAt("OFFENSE",40,158,14,Gold);TextAt("DEFENSE",660,158,14,Gold);for(int row=0;row<11;row++)for(int side=0;side<2;side++){int slot=side*11+row;float x=40+side*620,y=186+row*43;ClubPlayer starter=f.Starter(f.UserTeam,slot);Rect(x,y,580,39,selection==slot?Color.FromArgb(64,91,96):Color.FromArgb(34,43,57));TextAt(Franchise.Roles[slot],x+10,y+6,12,Gold);TextAt(DepthName(starter),x+58,y+6,11,starter!=null&&starter.InjuryWeeks>0?Red:White);List<ClubPlayer> reserves=f.Squad(f.UserTeam).FindAll(p=>p.Role==Franchise.Roles[slot]&&p.Slot<0&&p.InjuryWeeks==0);reserves.Sort((a,b)=>b.OVR.CompareTo(a.OVR));TextAt("BACKUP: "+(reserves.Count==0?"NONE HEALTHY":DepthName(reserves[0])),x+58,y+23,9,Muted);HitArea(x,y,580,39,slot);}Rect(40,678,1200,40,selection==22?Color.FromArgb(64,91,96):Color.FromArgb(34,43,57));TextAt("BACK",60,691,12,selection==22?Gold:White);HitArea(40,678,1200,40,22);
  }else{List<ClubPlayer> list=DepthCandidates();int first=selection/10*10;for(int i=first;i<Math.Min(first+10,list.Count+1);i++){float x=40+(i-first)%2*620,y=175+(i-first)/2*98;Rect(x,y,580,85,selection==i?Color.FromArgb(64,91,96):Color.FromArgb(34,43,57));if(i==list.Count){TextAt("BACK",x+20,y+30,16,White);}else{ClubPlayer p=list[i];PlayerPortrait(p,x+8,y+8,64);TextAt(DepthName(p),x+82,y+18,13,p.InjuryWeeks>0?Red:White);TextAt((p.Slot<0?"RESERVE":"STARTER SLOT "+(p.Slot+1))+" / SPD "+p.Speed+" / HANDS "+p.Hands+" / POWER "+p.Power,x+82,y+51,10,Muted);}HitArea(x,y,580,85,i);}}
  TextAt(Clip(f.Notice,96),40,743,10,Mint);
 }
}
}
