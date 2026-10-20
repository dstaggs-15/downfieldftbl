using System;
using System.Collections.Generic;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 bool DrawManagementCards(){bool coaches=screen=="coaches",training=screen=="training",facilities=screen=="facilities",player=screen=="player";if(!coaches&&!training&&!facilities&&!player)return false;
  FrontHeader(coaches?"COACHING STAFF":training?"WEEKLY TRAINING":facilities?"FACILITIES":"PLAYER FILE");int count=coaches?9:player?4:3;for(int i=0;i<count;i++){float x=40+(coaches?i%3*254:0),y=178+(coaches?i/3*146:i*112),w=coaches?242:750,h=coaches?132:98;bool chosen=selection==i;Rect(x,y,w,h,chosen?Color.FromArgb(63,89,96):Color.FromArgb(36,44,58));if(chosen)Rect(x,y,4,h,Gold);
   string title,detail;if(coaches){int unit=i/3,level=i%3+1,current=unit==0?franchise.Club.CoachOff:unit==1?franchise.Club.CoachDef:franchise.Club.CoachFit;title=new string[]{"OFFENSE","DEFENSE","FITNESS"}[unit];detail="TIER "+level+" / "+(current>=level?"HIRED":"$"+(5+level*5));MenuPicture("COACHES",x+12,y+17,36);TextAt(title,x+57,y+22,13,White);TextAt(detail,x+14,y+78,13,current>=level?Mint:Gold);}
   else{title=player?new string[]{"SET STARTER","RENEW CONTRACT","RELEASE PLAYER","FIND A TRADE"}[i]:training?new string[]{"PASSING","RUNNING","DEFENSE"}[i]:new string[]{"PASSING LAB","WEIGHT ROOM","FILM ROOM"}[i];int level=i==0?franchise.Passing:i==1?franchise.Running:franchise.Defense;detail=player?(i==0?"Choose his spot":i==1?"New salary $"+(SelectedPlayer==null?0:Franchise.ContractSalary(SelectedPlayer.OVR))+" / +2 years":i==2?"Reserves only":"Choose another club"):training?(franchise.TrainingRound==franchise.Round?"DONE THIS WEEK":"$3 / earn player XP"):"LEVEL "+level+" / "+(level>=5?"MAX":"UPGRADE $"+(4+level*3));MenuPicture(player?i==3?"TRADE":"ROSTER":training?"TRAINING":"FACILITIES",x+14,y+17,56);TextAt(title,x+95,y+19,20,White);TextAt(detail,x+95,y+61,13,Gold);if(facilities)for(int n=0;n<5;n++)Rect(x+535+n*34,y+65,25,8,n<level?Mint:Muted);}
   HitArea(x,y,w,h,i);
  }
  Panel(820,178,410,490);DrawFrontDetail(FrontRows());int back=count;Rect(40,coaches?628:player?638:546,750,50,selection==back?TeamPrimary:Color.FromArgb(36,44,58));TextAt("BACK",60,coaches?646:player?656:564,16,selection==back?Gold:White);HitArea(40,coaches?628:player?638:546,750,50,back);TextAt(Clip(franchise.Notice,98),40,725,11,Mint);return true;
 }
}
}
