using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    /// <summary>
    /// Encapsule l'état du bateau (intact, touché et coulé
    /// </summary>
    enum EtatBateau
    {
        BATEAU_INTACT,
        BATEAU_TOUCHE,
        BATEAU_COULE,
        BATEAU_NBRE_ETATS
    }
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
        /// Description des états des tableaux par un tableai dont l'index doit être EtatBateau
        /// </summary>
        public readonly static string[] DescriptionEtatBateau = { "intact", "touché", "coulé" };
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
        /// Etat du bateau
        /// (EtatBateau.BATEAU_COULE s'il a coulé, EtatBateau.BATEAU_TOUCHE s'il a été touché et EtatBateau.BATEAU_INTACT s'il est intact)
        /// </summary>
        public EtatBateau EtatBateau { get; set; } = EtatBateau.BATEAU_INTACT;
        /// <summary>
        /// Vérifie si le bateau n'a pas coulé
        /// </summary>
        /// <returns>
        /// false s'il a coulé, true sinon
        /// </returns>
        public bool IsAlive()
        {
            return EtatBateau != EtatBateau.BATEAU_COULE;
        }
        /// <summary>
        /// Indique que le navire à été touché
        /// </summary>
        public void EstAttaque()
        {
            if (PointsDeVie > 0)
            {
                PointsDeVie--;

                EtatBateau = (PointsDeVie > 0) ? EtatBateau.BATEAU_TOUCHE : EtatBateau.BATEAU_COULE;
            }
        }
    }

    /// <summary>
    /// Coordonnées sur la grille
    /// </summary>
    public class Coordonnee
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public Coordonnee()
        {
            x = 0;y = 0;
        }
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="_x">Abscisse</param>
        /// <param name="_y">Ordonnée</param>
        public Coordonnee(int _x,int _y)
        {
            x = _x;y = _y;
        }
        /// <summary>
        /// Abscisse
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// Ordonnée
        /// </summary>
        public int y { get; set; }
    }
}
