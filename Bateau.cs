using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    internal class Bateau
    {
        /// <summary>
        /// Accepte en entrée le type du bateau (TypeBateau) ainsi que ses coordonnées de départ
        /// </summary>
        /// <param name="tb">Le type du bateau</param>
        /// <param name="cd">Ses coordonnées de départ</param>
        public Bateau(TypeBateau tb, Coordonnee cd)
        {
            ID = NombreBateaux;
            PointsDeVie = tb.Taille;
            Nom = tb.NomModele;
            Longueur = tb.Taille;
            CoordDepart = cd;
            NombreBateaux++;
        }
        /// <summary>
        /// Nombre total de bateaux ayant été créés dans le jeu
        /// </summary>
        public static uint NombreBateaux { get; set; } = 0;
        /// <summary>
        /// Identifiant du bateau
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// Nom du type du navire (ex: sous-marin, porte-avion, etc...)
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Longueur du bateau
        /// </summary>
        public int Longueur { get; set; }
        /// <summary>
        /// Nombre de cases encore intactes
        /// </summary>
        public int PointsDeVie { get; set; }
        /// <summary>
        /// Point de départ du navire
        /// </summary>
        public Coordonnee? CoordDepart { get; set; }
        /// <summary>
        /// Vérifie si le bateau n'a pas coulé
        /// </summary>
        /// <returns>
        /// true si vivant, false autrement
        /// </returns>
        public bool IsAlive()
        {
            return PointsDeVie != 0;
        }
    }

    /// <summary>
    /// Coordonnées sur la grille
    /// </summary>
    public class Coordonnee
    {
        public Coordonnee()
        {
            x = 0;y = 0;
        }
        Coordonnee(int _x,int _y)
        {
            x = _x;y = _y;
        }
        /// <summary>
        /// Abscisse
        /// </summary>
        int x { get; set; }
        /// <summary>
        /// Ordonnée
        /// </summary>
        int y { get; set; }
    }
}
