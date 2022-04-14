using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{

    public class Ship
    {
        /// <summary>
        /// Point de départ du navire
        /// </summary>
        public Point ShipStartPointCoordinate { get; set; }
      
        /// <summary>
        /// Description des états des tableaux par un tableai dont l'index doit être EtatBateau
        /// </summary>
        public readonly static string[] DescriptionEtatBateau = { "intact", "touché", "coulé" };
        /// <summary>
        /// Nombre de bateaux créés
        /// </summary>
        public static int NombreBateaux { get; set; } = 0;
        /// <summary>
        /// Numéro d'identification du bateau
        /// </summary>
        public int ID { get; init; }
        /// <summary>
        /// Nom du bateau
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Etat du bateau
        /// </summary>
    
        public ShipState ShipState;
        /// <summary>
        /// Nombre de cases intactes du bateau
        /// </summary>
        public int LifePoint { get; set; }
        /// <summary>
        /// Longueur du bateau
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Indique si le navire est horizontal (true) ou vertical (false)
        /// </summary>
        public bool Horizontal { get; set; }

        /// <summary>
        /// Accepte en entrée le type du bateau (TypeBateau) ainsi que ses coordonnées de départ
        /// </summary>
        /// <param name="shipType">Le type du bateau</param>
        /// <param name="StartPointCoordinate">Ses coordonnées de départ</param>
        /// <param name="horizontal">Position horizontale</param>
        public Ship(ShipType shipType,  Point StartPointCoordinate, bool horizontal=true)
        {
            ID = NombreBateaux;
            NombreBateaux++;
            Name = shipType.ModelName;
            Size = shipType.Size;
            LifePoint = shipType.Size;
            ShipStartPointCoordinate = new Point(StartPointCoordinate.X, StartPointCoordinate.Y);
            ShipState = ShipState.ShipIntact;
            Horizontal = horizontal;
        }
    }
}
