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
                    Console.WriteLine("Imposible de placer le bateuax sur cette direction");
                    return false;
                }

                for (int yi = y + 1; yi <= y + size; yi++)
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
            for (int i = 1; i <= numberOfShip; i++)
            {
                ShipType shipType = new ShipType();
                Console.WriteLine("Choisissez le type de navale (un numéro entre 1 et 5) : \n 1: [Porte - avion] => Taille : 5" +
                   "\n 2: [Croiseur] => Taille: 4" +
                   "\n 3: [Frégate] => Taille: 3" +
                   "\n 4: [Sous-marin] => Taille: 3" +
                   "\n 5: [Escorteur] => Taille: 2");

                var navalType = Console.ReadLine();
                if (Convert.ToInt32(navalType) > 0 && Convert.ToInt32(navalType) < 6)
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
                    shipType.SetShipType(navalType);
                }


                Console.WriteLine("Tapez [n° de ligne],[n° de colomne] comme point du départ du bateaux");
                string[] msgSplited = Console.ReadLine().Split(',');
                int x = Convert.ToInt32(msgSplited[0]);
                int y = Convert.ToInt32(msgSplited[1]);

                while (!IsValidePointEntred(x, y))
                {
                    Console.WriteLine("Saisissez des coordonnées X et Y valide ");
                    string[] newCoord = Console.ReadLine().Split(',');
                    x = Convert.ToInt32(newCoord[0]);
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
                }

                var ship = new Ship(shipType, new Point(x, y)) { ID = i };

                gamer.Ships.Add(ship);
                Console.WriteLine("Bateaux {0} a été bien placé à partir de  x:{1}, y:{2} ", shipType.ModelName, x, y);
                SetOccupiedCellsAndShipPosition(ship, new Point(x, y), direction);
                Console.WriteLine("  gamer.Ships in ShipPlacement {0}   ", gamer.Ships.Count());
                DisplayGrid.Display(grid);

            }
            return gamer.Ships;
        }
    }
}
