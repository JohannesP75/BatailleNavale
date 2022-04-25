using BatailleNavale;
using BatailleNavale.GameManagment;
using BatailleNavale.Gamer;
using System.Drawing;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;


class Program
{
    static void Main(string[] args)
    {
        GameManagement gridManagment = new GameManagement();
        gridManagment.InitGame();
        gridManagment.StartGame();
                
    }
}