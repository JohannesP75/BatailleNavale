using BatailleNavale;
using BatailleNavale.Gamer;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Text;
using System.Linq;
using BatailleNavale.GameManagment;

public class GameManagement
{
    private Communication communication;
    public Gamers gamer;
    public Grid myGrid;
    public Grid adverseGrid;
    public ShipPlacement shipPlacement;
    public int ShipNumber = 5;

    /// <summary>
    /// token est le jeton indiquant celui qui va commencer à jouer en premier lorsque si il est à True
    /// </summary>
    public bool token { get; set; } = true;
    public GameManagement()
    {
        communication = new Communication();
        gamer = new Gamers("Joueur1", "192.168.1.112");
        myGrid = new Grid(10);
        adverseGrid = new Grid(10);
        shipPlacement = new ShipPlacement(myGrid, gamer);


    }
    public void InitGame()
    {

        shipPlacement.ShipDeployment(ShipNumber);
        if (token)
        {
        
        SendMyOccupiedCells(MyOccupiedCells());
            token = !token;
        }
        else
        {
            SetAdverserGrid(ReceivingHisOccupiedCellsCoord());
            token = !token;

        }



    }

    public void SendMyOccupiedCells(List<string> MyOccupiedCells)
    {
        communication.SendMessage(String.Join(';', MyOccupiedCells), gamer.IPAddress);

    }

    public List<string> MyOccupiedCells()
    {
        var listOccupiedCellsCoordinate = new List<string>() { "start" };
        var listOccupiedCells = (from list in myGrid.matrix
                                 from cell in list
                                 where cell.IsOccupied == true
                                 select cell).ToList();
        foreach (var cell in listOccupiedCells)
        {
            listOccupiedCellsCoordinate.Add(cell.PointCoordinate.X.ToString() + "," + cell.PointCoordinate.Y.ToString());
            Console.WriteLine("My occupied celles are : {0}", cell.PointCoordinate);

        }
        listOccupiedCellsCoordinate.Add("fin");

        return listOccupiedCellsCoordinate;
    }


    public void SetAdverserGrid(List<string> listString)
    {

        if (listString.First() == "start" && listString.Last() == "fin")
        {
            listString.Remove("start");
            listString.Remove("fin");
            foreach (string point in listString)
            {
                adverseGrid.matrix[Convert.ToInt32(point[0])][Convert.ToInt32(point[1])].IsOccupied = true;
            }
        }
        else
        {
            Console.WriteLine("Les cellules occupée de l'adversaire non pas tout reçu");
        }
    }


    public void CheckReceivedBlow()
    {
        Point point = ReceivingBlow(token);
        if (CellContent(point).IsOccupied)
        {
            myGrid.matrix[point.X][point.Y].IsBlowed = true;
        }
        else
        {
            myGrid.matrix[point.X][point.Y].IsMisHit = true;
        }
    }

    public List<string> ReceivingHisOccupiedCellsCoord()
    {
            string message = communication.ReceiveMessage();
            List<string> result = message.Split(';').ToList();
            return result;
    }


    /// <summary>
    /// Envoie les coups d'un joueur à un autre
    /// </summary>
    /// <param name="token">Indique si le joueur à le droit d'agir</param>
    public void SendingBlow(bool token, int x, int y)
    {
        if (token)
        {
            communication.SendMessage(x.ToString() + "," + y.ToString(), gamer.IPAddress);
            token = !token;
        }

    }

    /// <summary>
    /// Vérifie si un joueur peut recevoir des coups
    /// </summary>
    /// <param name="token">Indique si le joueur à le droit d'agir</param>
    public Point ReceivingBlow(bool token)
    {
        if (!token)
        {
            string message = communication.ReceiveMessage();
            string[] msgSplited = message.Split(',');

            Console.WriteLine("X : , Y : ", msgSplited[0], msgSplited[1]);
            token = !token;
            return new Point(Convert.ToInt32(msgSplited[0]), Convert.ToInt32(msgSplited[1]));

        }
        return new Point(-1, -1);
    }

    public Cell CellContent(Point p)
    {
        return (from list in myGrid.matrix
                from cell in list
                where cell.PointCoordinate == p
                select cell).ToList().First();

    }


}





