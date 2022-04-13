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
        public uint LifePoint { get; set; }

        public uint Size { get; set; }


        public ShipType(string modelName)
        {
            ModelName = modelName;
            
        }
   
   
    }
}