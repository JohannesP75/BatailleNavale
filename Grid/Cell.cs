using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale;

public enum CellType
{
    CELL_UNEXPLORED,
    CELL_ISTOUCHED,
    CELL_ISOCCUPIED,
    CELL_MISHIT
}

public class Cell
{
    /// <summary>
    /// Liste des images à afficher dans la case
    /// </summary>
    public readonly char[] ImageCase = new char[] { '.', ':', '#', '8' };
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


    /// <summary>
    /// Décrit l'état de la case
    /// </summary>
    public CellType CellType { get; set; }/// 

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
    /// <param name="p">Coordonnées de la cellule</param>
    public Cell(Point p)
    {
        InitCell(p);
    }

    /// <summary>
    /// Initialise la cellule
    /// </summary>
    /// <param name="p">Point de départ</param>
    private void InitCell(Point p)
    {
        IsOccupied = false;
        IsTouched = false;
        IsMisHit = false;
        IsBlowed = false;
        PointCoordinate = p;
        CellType = CellType.CELL_UNEXPLORED;
    }
}



