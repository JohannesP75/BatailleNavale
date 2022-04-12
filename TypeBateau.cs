namespace BatailleNavale
{
    /// <summary>
    /// Encapsule le type du bateau
    /// </summary>
    public class TypeBateau
    {
        public TypeBateau()
        {
            NomModele = "";
            Taille = 0;
        }
        public TypeBateau(string nom, int taille)
        {
            NomModele = nom;
            Taille = taille;
        }
        /// <summary>
        /// Nom du type du navire (ex: sous-marin, porte-avion, etc...)
        /// </summary>
        public String? NomModele { get; set; }
        /// <summary>
        /// Longueur du bateau
        /// </summary>
        public int Taille { get; set; }
    }
}