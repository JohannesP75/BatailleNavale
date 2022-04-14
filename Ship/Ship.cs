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
        /// 
        public Point ShipStartPointCoordinate { get; set; }
      
        /// <summary>
        /// Description des états des tableaux par un tableai dont l'index doit être EtatBateau
        /// </summary>
        public readonly static string[] DescriptionEtatBateau = { "intact", "touché", "coulé" };

        public static int Id = 0;
        public string? Name { get; set; }
    
        public ShipState shipState;
        public int LifePoint { get; set; }
        public int size { get; set; }

        /// <summary>
        /// Accepte en entrée le type du bateau (TypeBateau) ainsi que ses coordonnées de départ
        /// </summary>
        /// <param name="tb">Le type du bateau</param>
        /// <param name="cd">Ses coordonnées de départ</param>
        public Ship(ShipType shipType,  Point StartPointCoordinate)
        {
            Id ++;
            Name = shipType.ModelName;
            size = shipType.Size;
            LifePoint = shipType.Size;
            ShipStartPointCoordinate = new Point(StartPointCoordinate.X, StartPointCoordinate.Y);
            shipState = ShipState.ShipIntact;
        }
    }
}
