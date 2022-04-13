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
        /// Représente si la case est occupée par un navire
        /// </summary>
        public bool IsOccupied { get; set; }
        /// <summary>
        /// Représente si la case a été touchée
        /// </summary>
        public bool IsTouched { get; set; }

        /// <summary>
        /// Représente si la case a été touchée par erreur, sans qu'un navire soit présent
        /// </summary>
        public bool IsMisHit { get; set; }

        /// <summary>
        /// Pour si la case a été touchée et est occupée
        /// </summary>
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
