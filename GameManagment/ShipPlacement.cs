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

        private bool CheckShipDeployementByDirection(int x, int y, bool direction, int size)
        {
            bool S = true;

            // True => Horizontal , False => Verticale
            if (direction)
            {
                Console.WriteLine($"(x : {x}, y : {y} , CheckPointEntred({x-size}, y): {IsValidePointEntred(x - size, y)}, direction : {direction}");

                if (!IsValidePointEntred(x, y + size))
                {
                    Console.WriteLine("Imposible de placer le bateau dans cette direction !");
                    S = false;
                    //return false;
                }
                else
                {
                    for (int i = y + 1; i < y + size; i++)
                    {
                        Console.WriteLine("(x : {0}, y : {1} , CellIsOccupied : {2}", x, y, CellIsOccupied(x, y));

                        if (CellIsOccupied(x, y))
                        {
                            Console.WriteLine("Imposible de placer le bateau car les cellules ne sont pas libres !");

                            S = false;
                            break;
                            //return false;
                        }
                        S = true;
                    }
                    //return true;
                }
            }
            else
            {
                Console.WriteLine($"(x : {x}, y : {y} , CheckPointEntred({x-size}, y): {IsValidePointEntred(x - size, y)}, direction : {direction}");

                if (!IsValidePointEntred(x - size, y))
                {
                    Console.WriteLine("Imposible de placer le bateaux dans cette direction !");
                    S = false;
                    //return false;
                }
                else
                {
                    for (int i = x - 1; i < x - size; i--)
                    {
                        Console.WriteLine("(x : {0}, y : {1} , CellIsOccupied : {2}", x, y, CellIsOccupied(x, y));
                        if (CellIsOccupied(x, y))
                        {
                            Console.WriteLine("Imposible de placer le bateau car les cellules ne sont pas libres !");
                            break;
                            //return false;
                        }
                        S = true;
                    }
                    //return true;
                }
            }

            return S;
        }

        /// <summary>
        /// Vérifie si un bateau peut être placé sur la grille
        /// </summary>
        /// <param name="ship">Bateau à placer</param>
        /// <returns>Indique si le bateau a bien pu être placé</returns>
        public bool CheckDeployementPossibility(Ship ship)
        {
            return CheckDeployementPossibility(ship.ShipStartPointCoordinate.X, ship.ShipStartPointCoordinate.Y, ship.Horizontal, ship.Size);
        }

        /// <summary>
        /// Vérifie si un bateau peut être placé sur la grille
        /// </summary>
        /// <param name="x">Abscisse du point de départ</param>
        /// <param name="y">Ordonnée du point de départ</param>
        /// <param name="direction">Indique si le bateau est horizontal ou non</param>
        /// <param name="size">Longueur du bateau</param>
        /// <returns>Indique si le bateau a bien pu être placé</returns>
        private bool CheckDeployementPossibility(int x, int y, bool direction, int size)
        {
            bool isValid = IsValidePointEntred(x, y);
            //Console.WriteLine("les coordonnées saisis sont valide! x est : {0}  y est : {1} grid.size est : {2} isValid : {3}", x, y, grid.size, isValid);

            // si les coord sont invalide on demande à nouveau de saisir jusqu'à on obtient des valeurs valide
            while (!isValid)
            {
                Console.WriteLine("Entrez des coordonnées X et Y valides :");
                string[] msgSplited = Console.ReadLine().Split(',');

                x = Convert.ToInt32(msgSplited[0]);
                y = Convert.ToInt32(msgSplited[1]);
                isValid = IsValidePointEntred(x, y);
            }

            bool S = true;

            if (CellIsOccupied(x, y))
            {
                Console.WriteLine("La cellule est occupée: choisissez un autre emplacement !");
                S = false;
                //return false;
            }

            if (!CheckShipDeployementByDirection(x, y, direction, size))
            {
                S = false;
                //return false;
            }

            return S;
        }

        public bool IsValidePointEntred(int x, int y)
        {
            /*if (x < 0 || y < 0 || x > (grid.size - 1) || y > (grid.size - 1))
            {
                //Console.WriteLine("les coordonnées saisis sont invalide! x est : {0}  y est : {1} grid.size est : {2}", x,y, grid.size) ;
                return false;
            }
            else
                return true;*/
            return (x >= 0 && y >= 0 && x < grid.size  && y < grid.size - 1);
        }

        public void SetOccupiedCells(ShipType shipType, Point StartPointCoordinate, bool direction)
        {
            if (direction)
            {
                for (int i = 0; i < shipType.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].IsOccupied = true;
                    grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].CellState();
                }
            }
            else
            {
                for (int i = 0; i < shipType.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].IsOccupied = true;
                    grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].CellState();
                }
            }

        }
        public List<Ship> ShipDeployment(int numberOfShip)
        {
            int nbreBateaux = 0;//Nombre bateaux crées
            while (nbreBateaux < numberOfShip)
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
                }
                
                Console.WriteLine("Mettez les coordonnées X et Y comme point du départ du bateau");
                string[] msgSplited = Console.ReadLine().Split(',');

                Point StartPointCoordinate = new Point(Convert.ToInt32(msgSplited[0]), Convert.ToInt32(msgSplited[1]));
                Console.WriteLine("Choisissez 1 pour le placer horizontalement sinon 0 ");
                bool direction = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));

                if (CheckDeployementPossibility(StartPointCoordinate.X, StartPointCoordinate.Y, direction, shipType.Size))
                {
                    gamer.Ships.Add(new Ship(shipType, StartPointCoordinate));
                    Console.WriteLine("Bateau {0} est bien placé à partir de  x:{1}, y:{2} ", shipType.ModelName, StartPointCoordinate.X, StartPointCoordinate.Y);
                    SetOccupiedCells(shipType, StartPointCoordinate, direction);
                    nbreBateaux++;
                }
                
                DisplayGrid.Display(grid);
            }
            return gamer.Ships;
        }
    }
}