namespace BatailleNavale
{
    /// <summary>
    /// Encapsule le type du bateau
    /// </summary>
    public class ShipType
    {

        /// <summary>
        /// Nom du type du navire (ex: sous-marin, porte-avion, etc...)
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Nombre de cases encore intactes
        /// </summary>
        public int LifePoint { get; set; }

        public int Size { get; set; }



        public bool SetShipType(string type)
        {
            switch (type)
            {

                case "1":
                    ModelName = "Porte-avion";
                    Size = 5;
                    break;
                case "2":
                    ModelName = "Croiseur";
                    Size = 4;
                    break;

                case "3":
                    ModelName = "Frégate";
                    Size = 3;
                    break;
                case "4":
                    ModelName = "Sous-marin";
                    Size = 3;
                    break;

                case "5":
                    ModelName = "Escorteur";
                    Size = 2;
                    break;
                default:
                    return false;
            }

            return true;
        }
   
   
    }
}

