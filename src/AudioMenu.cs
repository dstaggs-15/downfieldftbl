using System;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 string audioBack="pause";static readonly string[] AudioLabels={"MASTER","CROWD","WHISTLES","HITS + MOVES","STADIUM HORNS","MENU SOUNDS","MUTE ALL","BACK"};
 void OpenAudioSettings(){audioBack="pause";Open("audio");}
 void AdjustAudio(int index,float delta){if(index>=0&&index<6){audio.Mix.SetGain(index,audio.Mix.GetGain(index)+delta);audio.SaveSettings();}}
 void ActivateAudio(){if(selection<6){float current=audio.Mix.GetGain(selection);audio.Mix.SetGain(selection,current>0?0:selection==2?.25f:.75f);audio.SaveSettings();}else if(selection==6)audio.Muted=!audio.Muted;else Open(audioBack);}
 void AudioMouse(PointF p){AudioMouse(p,true);}
 void AudioMouse(PointF p,bool persist){for(int i=0;i<6;i++)if(new RectangleF(380,148+i*70,760,52).Contains(p)){selection=i;audio.Mix.SetGain(i,Math.Max(0,Math.Min(1,(p.X-420)/680)));if(persist)audio.SaveSettings();Invalidate();return;}foreach(MenuTarget target in menuTargets)if(target.Bounds.Contains(p)){selection=target.Index;ActivateAudio();Invalidate();return;}}
 void DrawAudioSettings(){Rect(0,0,1280,800,Ink);TextAt("PAUSE SETTINGS",50,38,34,Gold);TextAt("LEFT / RIGHT VOLUME   /   A MUTE CHANNEL",50,98,12,Mint);for(int i=0;i<6;i++){float y=148+i*70,value=audio.Mix.GetGain(i);Rect(46,y,1184,55,selection==i?Color.FromArgb(58,83,91):Color.FromArgb(38,45,62));TextAt(AudioLabels[i],65,y+19,14,selection==i?Gold:White);Rect(420,y+23,680,10,Ink);Rect(420,y+23,680*value,10,i==2?Gold:Mint);Rect(416+680*value,y+15,8,26,White);TextAt(((int)Math.Round(value*100))+"%",1140,y+17,14,White);HitArea(46,y,1184,55,i);}for(int i=6;i<8;i++){float y=148+i*70;Rect(46,y,1184,55,selection==i?Color.FromArgb(58,83,91):Color.FromArgb(38,45,62));TextAt(i==6?(audio.Muted?"MUTE ALL: ON":"MUTE ALL: OFF"):"BACK TO PAUSE",65,y+19,14,selection==i?Gold:White);HitArea(46,y,1184,55,i);}TextAt(Hint("VIEW: QUICK MUTE","M: QUICK MUTE"),50,730,11,Muted);}
 void DrawQuickMute(){Rect(1102,121,158,34,Ink);TextAt(audio.Muted?"MUTED":"SOUND ON",1114,132,11,audio.Muted?Gold:White);HitArea(1102,121,158,34,100);}
}
}
