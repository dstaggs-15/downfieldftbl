using System.Drawing;
namespace Downfield {
public partial class GameForm {
 void DrawPersonnel(){if(game.Practice||game.AmbulanceActive||game.Controlled==null||game.Controlled.Name.Length==0)return;Rect(24,174,440,61,Ink);TextAt("#"+game.Controlled.Number+" "+game.Controlled.Name+" / "+game.Controlled.Role,34,184,11,White);string note=game.State==Phase.PreSnap&&game.PersonnelNotice.Length>0?game.PersonnelNotice:"ON FIELD: YOU "+game.OnFieldCount(0)+" / DEFENSE "+game.OnFieldCount(1);TextAt(Clip(note,34),34,211,10,game.OnFieldCount(0)<11?Gold:Mint);}
}
}
