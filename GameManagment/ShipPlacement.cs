using BatailleNavale.Gamer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BatailleNavale.GameManagment
{
    public class ShipPlacement
    {
        public Grid grid;
        public Gamers gamer;
        static List<ShipType> ShipTypes { get; set; } = JsonSerializer.Deserialize<List<ShipType>>(File.ReadAllText("typesBateaux.json"));

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="_grid">Grille du joueur</param>
        /// <param name="_gamer">Le joueur lui-même</param>
        public ShipPlacement(Grid _grid, Gamers _gamer)
        {
            this.grid = _grid;
            this.gamer = _gamer;
        }

        /// <summary>
        /// Indique si la cellule est occupée
        /// </summary>
        /// <param name="x">Abscisse</param>
        /// <param name="y">Ordonnée</param>
        /// <returns></returns>
        public bool CellIsOccupied(int x, int y)
        {
            return (from list in grid.matrix
                    from cell in list
                    where cell.PointCoordinate == new Point(x, y)
                    select cell).ToList().First().IsOccupied;
        }

        /// <summary>
        /// Vérifie si il y a de a place disponible pour placer un navire donné
        /// </summary>
        /// <param name="ship">Bateau à placer</param>
        /// <returns>Indique si le bateau a été bien placé</returns>
        public bool CheckShipDeployementByDirection(Ship ship)
        {
            return CheckShipDeployementByDirection(ship.ShipStartPointCoordinate.X, ship.ShipStartPointCoordinate.Y, ship.Horizontal, ship.Size);
        }

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
                if (!IsValidePointEntred(x, (y-1) + size))
                {
                    Console.WriteLine("Imposible de placer le bateau dans cette direction !");
                    S = false;
                    //return false;
                }

                for (int yi = y + 1; yi <= y + size; yi++)
                {
                    for (int i = y + 1; i <= y + size; i++)
                    {
                        Console.WriteLine("(x : {0}, y : {1} , CellIsOccupied : {2}", x, y, CellIsOccupied(x, y));

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
                if (!IsValidePointEntred(x - size, y))
                {
                    Console.WriteLine("Imposible de placer le bateuax sur cette directionnnn");

                    return false;
                }

                for (int xi = x - 1; xi <= x - size; xi--)
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
        public bool CheckDeployementPossibility(int x, int y, bool direction, int size)
        {

            bool isValid = IsValidePointEntred(x, y);
            //Console.WriteLine("les coordonnées saisis sont valide! x est : {0}  y est : {1} grid.size est : {2} isValid : {3}", x, y, grid.size, isValid);

            // si les coord sont invalide on demande à nouveau de saisir jusqu'à on obtient des valeurs valide
            while (!isValid)
            {
                Console.WriteLine("Entrez des coordonnées X et Y valide ");
                string[] msgSplited = Console.ReadLine().Split(',');

                isValid = IsValidePointEntred(Convert.ToInt32(msgSplited[0]), Convert.ToInt32(msgSplited[1]));
                x = Convert.ToInt32(msgSplited[0]);
                y = Convert.ToInt32(msgSplited[1]);
            }

            bool S = true;

        public void SetOccupiedCellsAndShipPosition(Ship ship, Point StartPointCoordinate, bool direction)
        {
            if (direction)
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].IsOccupied = true;
                    ship.Position.Add(new Point(StartPointCoordinate.X, StartPointCoordinate.Y + i));
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].IsOccupied = true;
                    ship.Position.Add(new Point(StartPointCoordinate.X - 1, StartPointCoordinate.Y));


                }
            }

        }

        public List<Ship> ShipDeployment(int numberOfShip)
        {
            int nbreBateaux = 0;//Nombre bateaux crées
            while (nbreBateaux < numberOfShip)
            for (int i = 1; i <= numberOfShip; i++)
            {
                ShipType shipType = new ShipType();
                Console.WriteLine($"Types de navires (numéro entre 1 et {ShipTypes.Count}) :");
                //foreach (ShipType type in ShipTypes) Console.WriteLine($"{type.ModelName} (taille {type.Size})");
                for (int k = 0; k < ShipTypes.Count; k++) Console.WriteLine($"({k + 1}) {ShipTypes[k].ModelName} (taille {ShipTypes[k].Size})");

                int navalType;
                do
                {
                    Console.WriteLine("Choisissez le type :");
                }while(!Int32.TryParse(Console.ReadLine(), out navalType));

                if (navalType > 0 && navalType <= ShipTypes.Count)
                {
                    //shipType.SetShipType(navalType);
                    shipType = ShipTypes[Convert.ToInt32(navalType)-1];
                }
                else
                {
                    while (navalType < 1 || navalType > ShipTypes.Count)
                    {
                        do
                        {
                            Console.WriteLine($"Vous devez choisir un nombre entre 1 et {ShipTypes.Count}");
                        } while (!Int32.TryParse(Console.ReadLine(), out navalType));
                    }
                    shipType.SetShipType(navalType);
                }


                Console.WriteLine("Tapez [n° de ligne],[n° de colomne] comme point du départ du bateaux");
                string[] msgSplited = Console.ReadLine().Split(',');
                int x = (int) Convert.ToChar(msgSplited[0])-'A';  // reste à valider
                int y = Convert.ToInt32(msgSplited[1]);

                while (!IsValidePointEntred(x, y))   // à corriger
                {
                    Console.WriteLine("Saisissez des coordonnées X et Y valide ");
                    string[] newCoord = Console.ReadLine().Split(',');
                            int x = (int)Convert.ToChar(msgSplited[0]) - 'A';  // reste à valider
                            y = Convert.ToInt32(newCoord[1]);
                }

                Console.WriteLine("Tapez: [1] horizontal placement \n     OU \n Tapez: [0] vertical placement ");
                bool direction = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));

                while (!CheckShipDeployementByDirection(x, y, direction, shipType.Size))
                {
                    Console.WriteLine("Tapez de nouveau [n° de ligne],[n° de colomne] comme pointt du départ du bateaux");
                    string[] msg = Console.ReadLine().Split(',');

                    x = Convert.ToInt32(msg[0]);
                    y = Convert.ToInt32(msg[1]);

                    Console.WriteLine("Tapez: [1] horizontal placement \n     OU \n Tapez: [0] vertical placement ");
                    direction = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                    gamer.Ships.Add(new Ship(shipType, StartPointCoordinate));
                    Console.WriteLine("Bateau {0} est bien placé à partir de  x:{1}, y:{2} ", shipType.ModelName, StartPointCoordinate.X, StartPointCoordinate.Y);
                    SetOccupiedCells(shipType, StartPointCoordinate, direction);
                                var ship = new Ship(shipType, new Point(x, y)) { ID = nbreBateaux++ };

                                gamer.Ships.Add(ship);
                                Console.WriteLine("Bateaux {0} a été bien placé à partir de  x:{1}, y:{2} ", shipType.ModelName, x, y);
                                SetOccupiedCellsAndShipPosition(ship, new Point(x, y), direction);
                                Console.WriteLine("  gamer.Ships in ShipPlacement {0}   ", gamer.Ships.Count());
                               
                }

               
                DisplayGrid.Display(grid);
            }
            return gamer.Ships;
        }
    }
}