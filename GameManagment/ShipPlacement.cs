using BatailleNavale.Gamer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BatailleNavale.GameManagment;

public class ShipPlacement
{
    public Grid grid;
    public Gamer.Gamer gamer;

    static string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
    public static List<ShipType> ShipTypes { get; set; } = JsonSerializer.Deserialize<List<ShipType>>(File.ReadAllText(path + "\\" + "typesBateaux.json"));

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    /// <param name="_grid">Grille du joueur</param>
    /// <param name="_gamer">Le joueur lui-même</param>
    public ShipPlacement(Grid _grid, Gamer.Gamer _gamer)
    {
        grid = _grid;
        gamer = _gamer;

    }

    /// <summary>
    /// Indique si la cellule est occupée
    /// </summary>
    /// <param name="x">Abscisse</param>
    /// <param name="y">Ordonnée</param>
    /// <returns></returns>
    public bool CellIsOccupied(int x, int y)
    {

        //Console.WriteLine(" in shipplacement CellIsOccupied(int x, int y) , grid.matrix[2][5].IsOccupied: {0} ", grid.matrix[2][5].PointCoordinate);

        return (from list in grid.matrix
                from cell in list
                where cell.PointCoordinate == new Point(x, y)
                select cell).ToList().First().IsOccupied;


        /*
                var a = (from list in grid.matrix
                        from cell in list
                        where cell.PointCoordinate == new Point(x, y)
                        select cell).ToList();

                Console.WriteLine(" in shipplacement CellIsOccupied(int x, int y) : {0} ", a.Count());


               return false;
        */
    }

    /// <summary>
    /// Vérifie si il y a de a place disponible pour placer un navire donné
    /// </summary>
    /// <param name="ship">Bateau à placer</param>
    /// <returns>Indique si le bateau a été bien placé</returns>


    public bool IsValidePointEntred(int x, int y)
    {
        if (x < 0 || y < 0 || x > (grid.size - 1) || y > (grid.size - 1))
        {
            //Console.WriteLine("les coordonnées saisis sont invalide! x est : {0}  y est : {1} grid.size est : {2}", x,y, grid.size) ;
            return false;
        }
        else
            return true;
    }

    public bool CheckShipDeployementByDirection(int x, int y, bool direction, int size)
    {
        if (!IsValidePointEntred(x, y))
        {
            Console.WriteLine("Saisissez des coordonnées X et Y valide ");
            return false;
        }

        // True => Horizontal , False => Verticale
        if (direction)
        {
            if (!IsValidePointEntred(x, (y - 1) + size))
            {
                Console.WriteLine("Imposible de placer le bateau dans cette direction !");
                // S = false;
                return false;
            }

            for (int yi = y; yi < y + size; yi++)
            {
                if (CellIsOccupied(x, yi))
                {
                    Console.WriteLine("Imposible de placer le bateuax , car les cellules ne sont pas libres");
                    return false;
                }
            }
            return true;
        }
        else
        {
            if (!IsValidePointEntred((x + 1) - size, y))
            {
                Console.WriteLine("Imposible de placer le bateuax sur cette directionnnn");
                return false;
            }

            for (int xi = x; xi > x - size; xi--)
            {

                if (CellIsOccupied(xi, y))
                {
                    Console.WriteLine("Imposible de placer le bateuax , car les cellules ne sont pas libres");
                    return false;
                }
            }
            return true;
        }
    }

    /*
    public bool CheckDeployementPossibility(int x, int y, bool direction, int size)
    {
        bool isValid = IsValidePointEntred(x, y);
        //Console.WriteLine("les coordonnées saisis sont valide! x est : {0}  y est : {1} grid.size est : {2} isValid : {3}", x, y, grid.size, isValid);

        // si les coord sont invalide on demande à nouveau de saisir jusqu'à on obtient des valeurs valide
        while (!isValid)
        {
            Console.WriteLine("Entrez des coordonnées X et Y valide ");
            string[] msgSplited = Console.ReadLine().Split(',');
            x = (int)Convert.ToChar(msgSplited[0]) - 'A';  // reste à valider
            y = Convert.ToInt32(msgSplited[1]);
            isValid = IsValidePointEntred(x, y);

        }
        return true;
    }

    */


    public void SetOccupiedCellsAndShipPosition(Ship ship, Point StartPointCoordinate, bool direction)
    {
        if (direction)
        {
            for (int i = 0; i < ship.Size; i++)
            {
                grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].IsOccupied = true;
                grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].CellType = CellType.CELL_ISOCCUPIED;
                ship.Position.Add(new Point(StartPointCoordinate.X, StartPointCoordinate.Y + i));
            }
        }
        else
        {
            for (int i = 0; i < ship.Size; i++)
            {
                grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].IsOccupied = true;
                grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].CellType = CellType.CELL_ISOCCUPIED;

                ship.Position.Add(new Point(StartPointCoordinate.X - i, StartPointCoordinate.Y));
            }
        }
    }

    public List<Ship> ShipDeployment(int numberOfShip)
    {
        uint nbreBateaux = 0;//Nombre bateaux crées
        while (nbreBateaux < numberOfShip)
        {
            ShipType shipType = new ShipType();
            Console.WriteLine($"Types de navires (numéro entre 1 et {ShipTypes.Count}) :");
            //foreach (ShipType type in ShipTypes) Console.WriteLine($"{type.ModelName} (taille {type.Size})");
            for (int k = 0; k < ShipTypes.Count; k++)
            {
                Console.WriteLine($"    ({k + 1}) {ShipTypes[k].ModelName} (taille {ShipTypes[k].Size})");
            }

            int navalType;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Choisissez le type :");

            do
            {
                while (!Int32.TryParse(Console.ReadLine(), out navalType))
                {
                    Console.WriteLine("Attention pas de lettre!");
                }

                if (!(navalType > 0 && navalType <= ShipTypes.Count))
                {
                    Console.WriteLine($"Attention Types de navires (numéro entre 1 et {ShipTypes.Count}) :");
                }

            } while (!(navalType > 0 && navalType <= ShipTypes.Count));

            shipType = ShipTypes[navalType - 1];
            int x = 0;
            int y = 0;
            bool direction = true;
            do
            {
                Console.WriteLine("Tapez [la lettre de la ligne] , [n° de colomne] comme point du départ du bateau");

                // Boucle sur le point de départ 
                do
                {
                    string[] msgSplited = Console.ReadLine().ToUpper().Split(',');
                    x = (int)Convert.ToChar(msgSplited[0][0]) - 'A';
                    y = (int)Convert.ToChar(msgSplited[1][0]) - '0';
                    if (!IsValidePointEntred(x, y))
                    {
                        Console.WriteLine("Les coordonnées entrées sont invalides! \n \nTapez [la lettre de la ligne],[n° de colomne] comme point du départ du bateau");
                    }
                } while (!IsValidePointEntred(x, y));

                Console.WriteLine("Tapez: [1] horizontal placement \n     OU \nTapez: [0] vertical placement ");

                // vérification de la direction Horizonatal/Vertical conforme
                switch (Console.ReadLine())
                {
                    case "1":
                        direction = true;
                        break;
                    case "0":
                        direction = false;
                        break;
                    default:
                        direction = true;
                        break;
                }
            }
            while (!CheckShipDeployementByDirection(x, y, direction, shipType.Size));

            Ship ship = new Ship(shipType, new Point(x, y)) { ID = nbreBateaux++ };

            gamer.Ships.Add(ship);

            Console.WriteLine("Bateau {0} a été bien placé ", shipType.ModelName);
            SetOccupiedCellsAndShipPosition(ship, new Point(x, y), direction);
            Console.WriteLine("Ma Grille");
            DisplayGrid.Display(grid);

        }
        return gamer.Ships;
    }
}
