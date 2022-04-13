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
        public int size { get; set; } = 10;
        public List<List<Cell>> matrix;

        public Grid()
        {
 

        }

        public void InitGrid(int size)
        {
             matrix = new List<List<Cell>>();

            for (int x = 0; x < size; x++)
            {
                matrix.Add(new List<Cell>(10));


                Console.WriteLine();
                for (int y = 0; y < size; y++)
                {
                    matrix[x].Add ( new Cell() { PointCoordinate = new Point(x,y) });
                    Console.Write("("+x+ "," +y+")");
                }
            }

        }

    }
}
