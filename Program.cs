//using BatailleNavale;
//using System.Text.Json;
using BatailleNavale;
using BatailleNavale.GameManagment;
using BatailleNavale.Gamer;

//class Program
//{
//    static void Main(String[]? args)
//    {
//        Console.WriteLine("Ceci est un jeu de bataille navale.");
//        TestBateaux();
//    }

//    static void TestBateaux()
//    {
//        // Affichage des types de bateaux
//        List<TypeBateau>? ListeTypes = new List<TypeBateau>();
//        String? jsonString = File.ReadAllText("typesBateaux.json");
//        ListeTypes = JsonSerializer.Deserialize<List<TypeBateau>>(jsonString);

//        foreach (TypeBateau tb in ListeTypes) Console.WriteLine($"Catégorie : {tb.NomModele} - Taille : {tb.Taille}");
//        Console.WriteLine();

//        // Affichage des bateaux
//        List<Bateau> ListeBateaux = new List<Bateau>();
//        Random rnd = new Random();
//        for (int i = 0; i < 10; i++) ListeBateaux.Add(new Bateau(ListeTypes[rnd.Next(ListeTypes.Count)], new Coordonnee(rnd.Next(0, 8), rnd.Next(0, 8))));

//        foreach (Bateau b in ListeBateaux) Console.WriteLine($"{b.Nom} ({b.ID}) - Taille {b.Longueur} - Solidité {b.PointsDeVie}/{b.Longueur} - Position ({b.CoordDepart.x}, {b.CoordDepart.y}) ({Bateau.DescriptionEtatBateau[(int)b.EtatBateau]})");
//        Console.WriteLine();

//        // Attaques
//        for (int i = 0; i < 20; i++)
//        {
//            int index = rnd.Next(ListeBateaux.Count);
//            ListeBateaux[index].EstAttaque();
//            Console.WriteLine($"{ListeBateaux[index].Nom} ({ListeBateaux[index].ID}) - Taille {ListeBateaux[index].Longueur} - Solidité {ListeBateaux[index].PointsDeVie}/{ListeBateaux[index].Longueur} - Position ({ListeBateaux[index].CoordDepart.x}, {ListeBateaux[index].CoordDepart.y}) ({Bateau.DescriptionEtatBateau[(int)ListeBateaux[index].EtatBateau]})");
//        }
//    }
//}



GameManagement girdManagment = new GameManagement();

girdManagment.InitGame();
girdManagment.StartGame();


//girdManagment.CheckReceivedBlow();









