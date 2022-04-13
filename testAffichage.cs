// Test affichage
/*
namespace BatailleNavale;

/*class Program
{
    static void Main()
    {
        public LaGrille cetteGrille { get; set; } = new();
        cetteGrille.Genere();
        Affichage.grille(cetteGrille);
    }
}

class Affichage
{
    public Affichage() // pas utile
    {
    }
    public void grille(LaGrille laGrille)
    {
        Console.Clear();
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                Console.Write(${ laGrille.lesCases[Col, Line].état, 2});
    }
    Console.WriteLine();
        }

    }
}
class LaGrille
{
    public Cases[,]? lesCases { get; set; }
    public LaGrille
        {
         = new Cases[8, 8];

    public void Genere()
    {
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                lesCases[Col, Line].état = (char)(32 + Line + Col);
                lesCases[Col, Line].identifiant = 0;
            }
        }
    }
} 

class Cases
{
    int[8, 8] GrilleIdBâteau;
    char[8, 8] GrilleEtatBâteau;
    LaGrille CetteGrille = new();
    GrilleIdBâteau, GrilleEtatBâteau = CetteGrille.Génère();
    Affichage.grille();
}*/

// Test affichage
namespace BatailleNavale;

class Program
{
	public Affichage() // pas utile
	{
	}
	public void grille(int[,] IdBâteau, int QuelJoueur)
    {
        Console.Clear();
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                Console.Write(${ GrilleEtatBâteau[Col,Line], 2});
            }
            Console.WriteLine();
        }

    }
}
public class LaGrille
{
    static int[,] IdBâteau { get; set; } = new() int[8, 8]; // ref bâteau ou 0 par défaut
    static char[,] EtatBâteau { get; set; } = new() char[8, 8];// (I)ntact (V)ide (C)oulé

    public static LaGrille Génère()
    {
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                EtatBâteau[Col, Line] = 32 + Line + Col;
                IdBâteau[Col, Line] = 0;
            }
        }
        Return EtatBâteau, IdBâteau;
    }
}

*/