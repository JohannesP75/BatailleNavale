using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    public class Grid
    {
        /// <summary>
        /// Taille du tableau
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Tableau en 2D représentant les cases du tableau
        /// </summary>
        public List<List<Cell>>? matrix;

        /// <summary>
        /// Constructeur de la classe utilisant la valeur standard de size
        /// </summary>
        public Grid(){
            InitGrid(Size);
        }
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="s">Taille de la carte (longeur et largeur)</param>
        public Grid(int s){
            InitGrid(s);
        }
        /// <summary>
        /// Initialise la grille
        /// </summary>
        /// <param name="s">Taille de la carte (longeur et largeur)</param>
        public void InitGrid(int s)
        {
            Size = s;
             matrix = new List<List<Cell>>();

            for (int x = 0; x < Size; x++)
            {
                matrix.Add(new List<Cell>(Size));


                Console.WriteLine();
                for (int y = 0; y < Size; y++)
                {
                    Cell c = new Cell(new Point(x, y));
                    matrix[x].Add(c);
                    //Console.Write("("+x+ "," +y+")");
                    Console.Write($"({c.PointCoordinate.X}, {c.PointCoordinate.Y})");
                }
            }

        }

        public void Show()
        {
            Console.Write($" ");
            for (int x = 0; x < Size; x++) Console.Write($"{x+1}");

            Console.WriteLine();

            // Montrer coordonnées
            for (int x = 0; x < Size; x++) {
                Console.Write($"{x+1}");
                for (int y = 0; y < Size; y++) Console.Write(Cell.ImageCase[(int)matrix[x][y].EtatCaseCell]);
                Console.WriteLine();
            }
        }

        public bool PutShip(Ship ship)
        {
            bool S = true;
            /*
             * Standard: le navire sera, si vertical, orienté vers le bas, et si horizontal, orienté vers la droite.
             */
            int taille = (int)ship.Size;

            S = (ship.Horizontal) ? (ship.ShipStartPointCoordinate.X + taille<Size) : (ship.ShipStartPointCoordinate.Y + taille < Size);

            if (S)
            {
                if(ship.Horizontal)
                    for(int x= ship.ShipStartPointCoordinate.X;x< ship.ShipStartPointCoordinate.X + taille; x++)
                    {
                        matrix[x][ship.ShipStartPointCoordinate.Y].IsOccupied = true;
                        matrix[x][ship.ShipStartPointCoordinate.Y].CellState();
                    }
                else
                    for (int y = ship.ShipStartPointCoordinate.Y; y < ship.ShipStartPointCoordinate.Y + taille; y++)
                    {
                        matrix[ship.ShipStartPointCoordinate.X][y].IsOccupied = true;
                        matrix[ship.ShipStartPointCoordinate.X][y].CellState();
                    }
            }

            return S;
        }

    }
}
