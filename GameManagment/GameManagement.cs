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
    public Communication communication;
    /// <summary>
    /// Encapsule les communications réseau
    /// </summary>
    private Communication communication;
    
    /// <summary>
    /// Le joueur
    /// </summary>
    public Gamers gamer;
    
    /// <summary>
    /// La grille du joueur
    /// </summary>
    public Grid myGrid;
    
    /// <summary>
    /// La grille ennemie
    /// </summary>
    public Grid adverseGrid;

    /// <summary>
    /// Place les navires
    /// </summary>
    public ShipPlacement shipPlacement;

    /// <summary>
    /// Nombre de bateaux du joueur
    /// </summary>
    public int ShipNumber = 5;
    public Point Blow;

    /// <summary>
    /// token est le jeton indiquant celui qui va commencer à jouer en premier lorsque si il est à True
    /// </summary>
    public bool token { get; set; } = true;

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    public GameManagement()
    {
        communication = new Communication();
        gamer = new Gamers("Joueur1", "192.168.1.112");
        myGrid = new Grid(10);
        adverseGrid = new Grid(10);
        shipPlacement = new ShipPlacement(myGrid, gamer);
        Blow = new Point(-1, -1);


    }

    /// <summary>
    /// Initialise le jeu
    /// </summary>
    public void InitGame()
    {
        gamer.Ships = shipPlacement.ShipDeployment(ShipNumber);

    }

    public bool IsValideBlowEntred(int x, int y)
    {
        if (x < 0 || y < 0 || x > (adverseGrid.size - 1) || y > (adverseGrid.size - 1))
        {
            //Console.WriteLine("les coordonnées saisis sont invalide! x est : {0}  y est : {1} grid.size est : {2}", x,y, grid.size) ;
            return false;
        }
        else
            return true;
    }

    public void StartGame()
    {
        var initToken = Communication.InitJeton(gamer.IPAddress);
        switch (initToken)
        {
            case 'A':
                return;
                break;
            case 'M':
                token = true;
                break;
            case 'E':
                token = false;
                break;


        }

        while (true)
        {
            if (token)
            {
                Console.WriteLine("Saissisez le cooordonnées du COUP que vous souhaitez effectué ");
                string[] entredBlow = Console.ReadLine().Split(',');
                Blow = new Point(Convert.ToInt32(entredBlow[0]), Convert.ToInt32(entredBlow[1]));

                while (!IsValideBlowEntred(Blow.X, Blow.Y))
                {
                    Console.WriteLine("Le COUP saisis est invalide!, Saissisez de nouveau :");
                    string[] newEntredBlow = Console.ReadLine().Split(',');
                    Blow = new Point(Convert.ToInt32(newEntredBlow[0]), Convert.ToInt32(newEntredBlow[1]));
                }

                SendingBlow(Blow.X, Blow.Y);

                do
                {
                    ReadReceivingMessageAndUpdateAdversairGrid();
                } while (!Communication.UdpDispo);


                Console.WriteLine("Adversaire grid");
                DisplayGrid.Display(adverseGrid);

                token = !token;
            }
            else
            {
                Console.WriteLine("Attente du COUP de l'adversaire ");
                var reveivedBlowPoint = ReceivingBlow();
                Console.WriteLine("Coup reçu! ");

                CheckReceivedBlow(reveivedBlowPoint);
                Console.WriteLine("My grid");
                DisplayGrid.Display(myGrid);


                token = !token;

            }
        }

    }

    //public void SetAdverserGrid(List<string> listString)
    //{

    //    if (listString.First() == "start" && listString.Last() == "fin")
    //    {
    //        listString.Remove("start");
    //        listString.Remove("fin");
    //        foreach (string point in listString)
    //        {
    //            adverseGrid.matrix[Convert.ToInt32(point[0])][Convert.ToInt32(point[1])].IsOccupied = true;
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("Les cellules occupée de l'adversaire non pas tout reçu");
    //    }
    //}

    public void GiveFeedbackToAdverser(string message)
    {
        Communication.SendMessage(message, gamer.IPAddress);
    }
    public void CheckReceivedBlow(Point p)
    {
        if (myGrid.matrix[p.X][p.Y].IsOccupied)
        {
            var state = "Touché";
            myGrid.matrix[p.X][p.Y].IsTouched = true;
            DisplayGrid.Display(myGrid);


            var releventShip = (from ship in gamer.Ships
                                from position in ship.Position
                                where position == p
                                select ship).ToList().First();
            releventShip.LifePoint--;
            if (releventShip.LifePoint == 0)
            {
                Console.WriteLine(releventShip.Name + "dont l'ID est" + releventShip.ID + "est coulé");
                state = "Coulé";
                releventShip.ShipState = ShipState.ShipBlowed;
            }
            GiveFeedbackToAdverser(state);
        }
        else
        {
            myGrid.matrix[p.X][p.Y].IsMisHit = true;
            GiveFeedbackToAdverser("Raté!");
          
        }
    }

    public void ReadReceivingMessageAndUpdateAdversairGrid()
    {
        if (Communication.UdpDispo)
        {
            string message = Communication.UDPRecu;
            if (message == "Touché")
            {
                adverseGrid.matrix[Blow.X][Blow.Y].IsTouched = true;
            }

            if (message == "Raté")
            {
                adverseGrid.matrix[Blow.X][Blow.Y].IsMisHit = true;
            }

            if (message == "Coulé")
            {
                adverseGrid.matrix[Blow.X][Blow.Y].IsTouched = true;
                Console.WriteLine("Le Bateaux est coulé");

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
    /// <param name="x">Abscisse du point d'arrivée</param>
    /// <param name="y">Ordonnée du point d'arrivée</param>
    /// 
    public void SendingBlow(int x, int y)
    {
        Communication.SendMessage(x.ToString() + "," + y.ToString(), gamer.IPAddress);
    }

    /// <summary>
    /// Vérifie si un joueur peut recevoir des coups
    /// </summary>
    /// <param name="token">Indique si le joueur à le droit d'agir</param>
    public Point ReceivingBlow()
    {
        while (!Communication.UdpDispo) ;
        string message = Communication.UDPRecu;
        string[] msgSplited = message.Split(',');
        Console.WriteLine("X : , Y : ", msgSplited[0], msgSplited[1]);
        return new Point(Convert.ToInt32(msgSplited[0]), Convert.ToInt32(msgSplited[1]));
    }

    /// <summary>
    /// Renvoie le contenu d'une cellule de coordonnées p
    /// </summary>
    /// <param name="p">Coordonnées de la céllule recherchée</param>
    /// <returns>Valeur de la cellule recherchée</returns>
    public Cell CellContent(Point p)
    {
        return (from list in myGrid.matrix
                from cell in list
                where cell.PointCoordinate == p
                select cell).ToList().First();
    }


}





