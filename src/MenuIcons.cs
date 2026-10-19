using System;
using System.Drawing;
namespace Downfield {
public partial class GameForm {
 int MenuIcon(string label,int fallback){switch(label){case "CLUBHOUSE":case "MOVE":return 12;case "MY COACH":case "COACH":return 8;case "TROPHIES":case "CHAMPIONS":return 3;case "BACK":case "QUIT":return 9;case "HISTORY":return 22;case "NAME":return 13;case "SKIN":return 15;case "HAIR":return 16;case "HAT":return 14;case "OUTFIT":case "ROSTER":return 0;case "STAFF":return 8;case "HEALTH":return 11;case "SCHEDULE":return 10;case "TRADE":return 18;case "TEAM":return 19;case "NEWS":return 21;case "LEADERS":case "TABLE":return 5;case "JOBS":return 4;case "PLAYOFFS":return 3;case "TRAIN":case "UPGRADES":return 20;default:return fallback;}}
 void ExtraIcon(int kind,float x,float y,float u,Color color){Action<float,float,float,float> r=(a,b,w,h)=>Rect(x+a*u,y+b*u,w*u,h*u,color);
  switch(kind){case 8:case 15:case 16:r(3,0,4,4);r(1,6,8,4);if(kind==16){Rect(x+3*u,y+u,4*u,3*u,Ink);r(2,0,6,1);}break;
   case 9:r(0,4,10,2);r(0,3,2,4);r(2,1,2,8);break;
   case 10:r(1,1,8,9);Rect(x+2*u,y+4*u,6*u,5*u,Ink);r(2,0,1,3);r(7,0,1,3);r(3,5,1,1);r(6,5,1,1);r(3,7,1,1);r(6,7,1,1);break;
   case 11:r(0,4,2,2);r(2,2,1,5);r(3,5,2,2);r(5,0,1,9);r(6,7,1,3);r(7,4,3,2);break;
   case 12:r(1,3,8,7);r(0,1,10,2);Rect(x+4*u,y+6*u,2*u,4*u,Ink);Rect(x+2*u,y+4*u,u,u,Ink);Rect(x+7*u,y+4*u,u,u,Ink);break;
   case 13:for(int i=0;i<7;i++)r(i,8-i,2,2);r(7,0,2,2);break;
   case 14:r(2,2,6,5);r(0,7,10,2);break;
   case 18:r(0,1,8,2);r(6,0,2,4);r(2,7,8,2);r(2,6,2,4);break;
   case 19:r(1,1,8,8);Rect(x+5*u,y+u,4*u,4*u,Gold);Rect(x+u,y+5*u,4*u,4*u,Blue);Rect(x+5*u,y+5*u,4*u,4*u,White);break;
   case 20:r(0,3,2,4);r(8,3,2,4);r(2,4,6,2);r(1,1,1,8);r(8,1,1,8);break;
   case 21:r(0,1,10,9);Rect(x+u,y+2*u,8*u,7*u,Ink);r(2,3,3,3);r(6,3,2,1);r(6,5,2,1);r(2,7,6,1);break;
   case 22:r(0,1,4,8);r(6,1,4,8);r(4,3,2,7);Rect(x+u,y+2*u,2*u,5*u,Ink);Rect(x+7*u,y+2*u,2*u,5*u,Ink);break;
  }
 }
}
}
