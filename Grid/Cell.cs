using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    public class Cell
    {
        public Point PointCoordinate { get; set; }

        /// <summary>
        /// IsOccupied True means YES, False means is free
        /// </summary>
        public bool IsOccupied { get; set; }

        public bool IsTouched { get; set; }


        public bool IsMisHit { get; set; }

        public bool IsBlowed { get; set; }

        public Cell()
        {
            IsOccupied = false;
            IsTouched = false;
            IsMisHit = false;
            IsBlowed = false;
            PointCoordinate = new Point(0,0);
         
        }


        public void CellState()
        {
            if (IsTouched & IsOccupied)
            {
                IsBlowed = true;
            }
             if (IsTouched & !IsOccupied)
            {
                IsMisHit = true;
            }
        }

    }
}
