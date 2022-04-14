using BatailleNavale.Gamer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale.GameManagment
{
    public class ShipPlacement
    {
        public Grid grid;
        public Gamers gamer;


        public ShipPlacement(Grid _grid, Gamers _gamer)
        {
            this.grid = _grid;
            this.gamer = _gamer;
        }

        public bool CellIsOccupied(int x, int y)
        {
            return (from list in grid.matrix
                    from cell in list
                    where cell.PointCoordinate == new Point(x, y)
                    select cell).ToList().First().IsOccupied;
        }

        public bool CheckShipDeployementByDirection(int x, int y, bool direction, int size)
        {
            bool S = true;

            // True => Horizontal , False => Verticale
            if (direction)
            {
                Console.WriteLine("(x : {0}, y : {1} , CheckPointEntred(x-size, y): {2}, direction : {3}", x, y, IsValidePointEntred(x - size, y), direction);

                if (!IsValidePointEntred(x, y + size))
                {
                    Console.WriteLine("Imposible de placer le bateuax sur cette direction");
                    S = false;
                    //return false;
                }
                else
                {
                    for (int i = y + 1; i <= y + size; i++)
                    {
                        Console.WriteLine("(x : {0}, y : {1} , CellIsOccupied : {2}", x, y, CellIsOccupied(x, y));

                        if (CellIsOccupied(x, y))
                        {
                            Console.WriteLine("Imposible de placer le bateuax , car les cellules ne sont pas libres");

                            S = false;
                            //return false;
                        }
                    }
                    S = true;

                    //return true;
                }
            }
            else
            {
                Console.WriteLine("(x : {0}, y : {1} , CheckPointEntred(x-size, y): {2}, direction : {3}", x, y, IsValidePointEntred(x - size, y), direction);

                if (!IsValidePointEntred(x - size, y))
                {
                    Console.WriteLine("Imposible de placer le bateuax sur cette direction");
                    S = false;
                    //return false;
                }
                else
                {
                    for (int i = x - 1; i <= x - size; i--)
                    {
                        Console.WriteLine("(x : {0}, y : {1} , CellIsOccupied : {2}", x, y, CellIsOccupied(x, y));
                        if (CellIsOccupied(x, y))
                        {
                            Console.WriteLine("Imposible de placer le bateuax , car les cellules ne sont pas libres");
                            break;
                            //return false;
                        }
                    }
                    S = true;
                    //return true;
                }
            }

            return S;
        }
        public bool CheckDeployementPossibility(int x, int y, bool direction, int size)
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
                Console.WriteLine("La cellule est occupée choisi un autre emplacement");
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
            return !(x < 0 || y < 0 || x > (grid.size - 1) || y > (grid.size - 1));
        }

        public void SetOccupiedCells(ShipType shipType, Point StartPointCoordinate, bool direction)
        {
            if (direction)
            {
                for (int i = 0; i < shipType.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X][StartPointCoordinate.Y + i].IsOccupied = true;
                }
            }
            else
            {
                for (int i = 0; i < shipType.Size; i++)
                {
                    grid.matrix[StartPointCoordinate.X - i][StartPointCoordinate.Y].IsOccupied = true;

                }
            }

        }
        public List<Ship> ShipDeployment(int numberOfShip)
        {
            for (int i = 0; i < numberOfShip; i++)
            {
                ShipType shipType = new ShipType();
                Console.WriteLine("Choisissez le type de navale (un numéro entre 1 et 5) : \n 1: [Porte - avion] => Taille : 5" +
                   "\n 2: [Croiseur] => Taille: 4" +
                   "\n 3: [Frégate] => Taille: 3" +
                   "\n 4: [Sous-marin] => Taille: 3" +
                   "\n 5: [Escorteur] => Taille: 2");

                var navalType = Console.ReadLine();
                if (Convert.ToInt32(navalType) > 0 && Convert.ToInt32(navalType) < 6)
                {
                    shipType.SetShipType(navalType);
                }
                else
                {
                    while (!shipType.SetShipType(navalType))
                    {
                        Console.WriteLine("Choisissez un nombre entre 1 et 5");
                        navalType = Console.ReadLine();
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
                }
                DisplayGrid.Display(grid);

            }
            return gamer.Ships;
        }
    }
}
