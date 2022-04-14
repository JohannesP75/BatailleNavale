using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    public enum EtatCase{
        CASE_LIBRE_INTACTE,
        CASE_LIBRE_TOUCHEE,
        CASE_OCCUPEE_INTACTE,
        CASE_OCCUPEE_TOUCHEE
    }

    public class Cell
    {
        public static string[] ImageCase { get; } = new string[] { "-", ":", "#", "8" };

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

        /// <summary>
        /// Constructeur par défaut de la classe
        /// </summary>
        public Cell()
        {
            InitCell(new Point(0, 0));
        }

        /// <summary>
        /// Constructeur de la classe intégrant sa position
        /// </summary>
        /// <param name="x">Absisse</param>
        /// <param name="y">Ordonnée</param>
        public Cell(int x, int y)
        {
            InitCell(new Point(x, y));
        }
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="p">Coordonnées de la classe</param>
        public Cell(Point p)
        {
            InitCell(p);
        }

        private void InitCell(Point p)
        {
            IsOccupied = false;
            IsTouched = false;
            IsMisHit = false;
            IsBlowed = false;
            PointCoordinate = p;
            EtatCaseCell = EtatCase.CASE_LIBRE_INTACTE;
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
        /// <summary>
        /// Décrit l'état de la case
        /// </summary>
        public EtatCase EtatCaseCell { get; set; }

    }
}
