using System;
using System.Collections.Generic;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 DrawnPlay draftPlay;int routeReceiver,cursorX=12,cursorZ=5;bool drawing,freehand,drawStartsNew;float designRepeat;
 int designSection,routeChoice,designAction;string designNotice="";
 static readonly string[] RouteChoices={"GO","SLANT","OUT","IN","CROSS","CURL","POST","CORNER","FLAT","STOCK"};
 static readonly string[] DesignActions={"USE PLAY","SAVE","LOAD","DRAW","UNDO","BACK"};
 static readonly int[] DesignActionIds={5,8,9,11,6,10};
 void OpenDesigner(){if(game.State!=Phase.PreSnap)return;draftPlay=game.UseCustom&&game.CustomPlay!=null?game.CustomPlay.Copy():DrawnPlay.Default(game.Playbook,game);routeReceiver=routeChoice=designSection=designAction=0;screen="designer";menuTargets.Clear();selection=0;navTime=.2f;designRepeat=.2f;drawing=freehand=false;designNotice="PICK A PLAYER, THEN A ROUTE";CenterDesignCursor();}
 DrawnRoute EditRoute(){DrawnRoute r=draftPlay.Routes.Find(x=>x.Receiver==routeReceiver);if(r==null){r=new DrawnRoute();r.Receiver=routeReceiver;draftPlay.Routes.Add(r);}return r;}
 void CenterDesignCursor(){Player p=game.Receiver(routeReceiver);cursorX=Math.Max(0,Math.Min(25,(int)Math.Round((p.StartX+25)/2)));cursorZ=4;}
 PointF DesignPoint(float x,float z){return new PointF(306+(x+26)*11,575-z*10.5f);}
 void DesignerMouse(PointF p){
  if(!drawing)foreach(MenuTarget target in menuTargets)if(target.Bounds.Contains(p)){DesignerAction(target.Index);return;}
  if(p.X<306||p.X>878||p.Y<155||p.Y>659)return;
  if(!freehand){for(int i=0;i<5;i++){Player player=game.Receiver(i);PointF dot=DesignPoint(player.StartX,player.StartZ-game.Spot);if(Simulation.Distance(dot.X,dot.Y,p.X,p.Y)<25){DesignerAction(i);return;}}designNotice="PICK A ROUTE ON THE RIGHT / DRAW FOR FREEHAND";return;}
  AddDesignPoint(Math.Max(-25,Math.Min(25,(p.X-306)/11-26)),Math.Max(-8,Math.Min(40,(575-p.Y)/10.5f)));drawing=true;
 }
 void AddDesignPoint(float x,float z){DrawnRoute r=EditRoute();if(drawStartsNew){r.Points.Clear();drawStartsNew=false;}r.Label="CUSTOM";r.Hold=false;if(r.Points.Count>=20){designNotice="20 POINT LIMIT / UNDO TO CHANGE";return;}if(r.Points.Count==0||Simulation.Distance(x,z,r.Points[r.Points.Count-1].X,r.Points[r.Points.Count-1].Z)>1){r.Points.Add(new RouteNode(x,z));designNotice="ROUTE DRAWN / USE PLAY WHEN READY";}}
 void DesignerAction(int action){
  audio.Play("menu");
  if(action<5){routeReceiver=action;freehand=false;designSection=1;CenterDesignCursor();designNotice="CHOOSE "+ReceiverIcon(routeReceiver)+" ROUTE";return;}
  if(action>=20&&action<30){routeChoice=action-20;Player p=game.Receiver(routeReceiver);draftPlay.Routes.RemoveAll(r=>r.Receiver==routeReceiver);DrawnRoute route=DrawnPlay.Preset(routeReceiver,RouteChoices[routeChoice],p.StartX,p.StartZ-game.Spot);if(route.Points.Count>0)draftPlay.Routes.Add(route);freehand=false;designSection=1;designNotice="#"+p.Number+" / "+RouteChoices[routeChoice]+" / READY";return;}
  if(action==5){ApplyDraft();return;}
  if(action==6){DrawnRoute r=EditRoute();if(r.Points.Count>0)r.Points.RemoveAt(r.Points.Count-1);r.Label="CUSTOM";designNotice="LAST POINT REMOVED";}
  else if(action==7){EditRoute().Points.Clear();designNotice="USING STOCK ROUTE";}
  else if(action==8){if(!draftPlay.Routes.Exists(r=>r.Points.Count>0)){designNotice="ADD A ROUTE FIRST";return;}franchise.SavedPlay=draftPlay.Copy();franchise.Save();designNotice="PLAY SAVED";}
  else if(action==9){if(franchise.SavedPlay==null||!franchise.SavedPlay.Routes.Exists(r=>r.Points.Count>0)){designNotice="NO SAVED PLAY YET";return;}draftPlay=franchise.SavedPlay.Copy();freehand=false;designNotice="SAVED PLAY LOADED";}
  else if(action==10)Open("game");
  else if(action==11){freehand=!freehand;drawStartsNew=true;drawing=false;CenterDesignCursor();designNotice=freehand?"DRAG ON FIELD / A ADDS CURSOR POINT":"CHOOSE A ROUTE";}
 }
 void ApplyDraft(){if(!draftPlay.Routes.Exists(r=>r.Points.Count>0)){designNotice="CHOOSE A ROUTE FIRST";return;}game.CustomPlay=draftPlay.Copy();game.UseCustom=true;game.RunPlay=false;game.Playbook=Simulation.IsRunCall(draftPlay.BasePlay)?0:Math.Max(0,Math.Min(23,draftPlay.BasePlay));Open("game");game.Message="CUSTOM PLAY READY / SNAP";}
 void TickDesigner(float dt){
  designRepeat-=dt;int dx=input.MoveX>.5f||input.Held(Input.Right)?1:input.MoveX<-.5f||input.Held(Input.Left)?-1:0,dz=input.MoveZ>.5f||input.Held(Input.Up)?-1:input.MoveZ<-.5f||input.Held(Input.Down)?1:0;
  if(freehand){if(designRepeat<=0&&(dx!=0||dz!=0)){cursorX=Math.Max(0,Math.Min(25,cursorX+dx));cursorZ=Math.Max(-4,Math.Min(20,cursorZ-dz));designRepeat=.1f;}if(input.Pressed(Input.A))AddDesignPoint(-25+cursorX*2,cursorZ*2);}
  else{
   if(designRepeat<=0&&(dx!=0||dz!=0)){if(designSection==0){if(dx>0)designSection=1;else if(dz>0&&routeReceiver==4)designSection=2;else routeReceiver=(routeReceiver+dz+5)%5;}
    else if(designSection==1){if(dx<0&&routeChoice%2==0)designSection=0;else if(dx>0&&routeChoice%2==1){designSection=2;designAction=0;}else if(dz>0&&routeChoice>=8){designSection=2;designAction=0;}else routeChoice=(routeChoice+dx+dz*2+10)%10;}
    else{if(dz<0)designSection=1;else designAction=(designAction+dx+(dz!=0?1:0)+6)%6;}designRepeat=.18f;audio.Play("menu");}
   if(input.Pressed(Input.A)){if(designSection==0){designSection=1;designNotice="CHOOSE A ROUTE";}else if(designSection==1)DesignerAction(20+routeChoice);else DesignerAction(DesignActionIds[designAction]);}
  }
  if(input.Pressed(Input.Y)){routeReceiver=(routeReceiver+1)%5;CenterDesignCursor();}
  if(input.Pressed(Input.X))DesignerAction(6);if(input.Pressed(Input.RB))DesignerAction(8);if(input.Pressed(Input.LB))DesignerAction(9);
  if(input.Pressed(Input.Start)){ApplyDraft();return;}
  if(input.Pressed(Input.B)||input.PausePressed){if(freehand){freehand=false;drawing=false;designNotice="CHOOSE A ROUTE";}else if(designSection!=0)designSection=0;else Open("game");}
 }
 void DesignButton(string text,float x,float y,float w,float h,int action,bool selected){Rect(x,y,w,h,selected?Color.FromArgb(65,97,98):Color.FromArgb(38,45,62));if(selected)Rect(x,y,w,3,Gold);TextAt(text,x+12,y+13,12,selected?Gold:White);HitArea(x,y,w,h,action);}
 void RoutePreview(DrawnRoute route,Player player,Color color,bool selected){PointF previous=DesignPoint(player.StartX,player.StartZ-game.Spot);if(route!=null)foreach(RouteNode n in route.Points){PointF next=DesignPoint(n.X,n.Z);Line(previous.X,previous.Y,next.X,next.Y,color,selected?4:2);previous=next;}
  if(route!=null&&route.Points.Count>0){if(route.Hold)Ell(previous.X-5,previous.Y-5,10,10,color);else{Line(previous.X-5,previous.Y+8,previous.X,previous.Y,color,2);Line(previous.X+5,previous.Y+8,previous.X,previous.Y,color,2);}}
  PointF start=DesignPoint(player.StartX,player.StartZ-game.Spot);Ell(start.X-10,start.Y-10,20,20,selected?White:Ink);Ell(start.X-7,start.Y-7,14,14,color);TextAt(ReceiverIcon(route==null?Array.IndexOf(new Player[]{game.Receiver(0),game.Receiver(1),game.Receiver(2),game.Receiver(3),game.Receiver(4)},player):route.Receiver),start.X-4,start.Y+14,10,color);
 }
 void DrawDesigner(){
  Rect(0,0,1280,800,Ink);TextAt("CREATE A PLAY",28,25,32,Gold);TextAt("1 PICK PLAYER   >   2 PICK ROUTE   >   3 USE PLAY",28,76,14,White);
  TextAt("PLAYERS",28,119,12,Mint);TextAt(freehand?"DRAW ON THE FIELD":"YOUR PLAY",306,119,12,Mint);TextAt("ROUTES",914,119,12,Mint);
  Rect(306,155,572,504,Color.FromArgb(35,78,56));for(int i=0;i<11;i++)Line(306,155+i*48,878,155+i*48,Color.FromArgb(75,116,80),1);for(int i=0;i<5;i++){float y=575-i*105;Line(306,y,878,y,White,1);TextAt((i*10).ToString(),312,y-17,10,Muted);}Line(306,575,878,575,Blue,3);TextAt("LINE OF SCRIMMAGE",455,558,10,White);
  for(int i=0;i<5;i++){Player p=game.Receiver(i);DrawnRoute route=draftPlay.Routes.Find(r=>r.Receiver==i);DesignButton(ReceiverIcon(i)+"  #"+p.Number+" "+p.Role,28,155+i*82,245,71,i,routeReceiver==i);TextAt(route==null||route.Points.Count==0?"STOCK":route.Label,40,196+i*82,11,IconColor(i));if(i!=routeReceiver)RoutePreview(route,p,IconColor(i),false);}
  RoutePreview(draftPlay.Routes.Find(r=>r.Receiver==routeReceiver),game.Receiver(routeReceiver),IconColor(routeReceiver),true);
  for(int i=0;i<10;i++){float x=914+i%2*174,y=155+i/2*95;DesignButton(RouteChoices[i],x,y,158,82,20+i,designSection==1&&routeChoice==i&&!freehand);DrawnRoute thumbnail=DrawnPlay.Preset(0,RouteChoices[i],0,0);PointF last=new PointF(x+78,y+74);foreach(RouteNode point in thumbnail.Points){PointF next=new PointF(x+78+point.X*2,y+74-point.Z*1.6f);Line(last.X,last.Y,next.X,next.Y,IconColor(routeReceiver),2);last=next;}}
  if(freehand){PointF cursor=DesignPoint(-25+cursorX*2,cursorZ*2);Line(cursor.X-10,cursor.Y,cursor.X+10,cursor.Y,Gold,3);Line(cursor.X,cursor.Y-10,cursor.X,cursor.Y+10,Gold,3);}
  TextAt(Clip(designNotice,90),28,681,12,Mint);
  for(int i=0;i<6;i++)DesignButton(DesignActions[i],28+i*207,710,192,40,DesignActionIds[i],designSection==2&&designAction==i||i==3&&freehand);
  TextAt(Hint("A CHOOSE / Y NEXT PLAYER / MENU USE PLAY / B BACK","CLICK PLAYER + ROUTE / P USE PLAY / ESC BACK"),28,101,10,Muted);
 }
}
}