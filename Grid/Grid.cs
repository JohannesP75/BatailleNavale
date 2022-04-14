using System.Drawing;

namespace BatailleNavale
{
    public class Grid
    {
        /// <summary>
        /// Taille du tableau
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// Tableau en 2D représentant les cases du tableau
        /// </summary>
        public List<List<Cell>>? matrix;

        /// <summary>
        /// Constructeur de la classe utilisant la valeur standard de size
        /// </summary>
        public Grid()
        {
            InitGrid(size);
        }
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="s">Taille de la carte (longeur et largeur)</param>
        public Grid(int s)
        {
            size = s;
            InitGrid(s);

        }
        /// <summary>
        /// Initialise la grille
        /// </summary>
        /// <param name="size">Taille de la carte (longeur et largeur)</param>
        public void InitGrid(int size)
        {
            matrix = new List<List<Cell>>();

            for (int x = 0; x < size; x++)
            {
                matrix.Add(new List<Cell>(size));


                Console.WriteLine();
                for (int y = 0; y < size; y++)
                {
                    matrix[x].Add(new Cell() { PointCoordinate = new Point(x, y) });
                   // Console.Write("(" + x + "," + y + ")");
                }
            }

        }

    }
}
