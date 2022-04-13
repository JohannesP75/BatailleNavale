namespace BatailleNavale;

class main
{
    int[,] GrilleIdBateau=new int[8, 8];
    char[,] GrilleEtatBateau=new char[8, 8];
    LaGrille CetteGrille = new();
    GrilleIdBateau, GrilleEtatBateau = CetteGrille.Genere();
    Affichage.grille();
}

public class Affichage
{
	public Affichage() // pas utile
	{
	}
	public void grille(int[,] IdBateau, int QuelJoueur)
    {
        Console.Clear();
		for (int Line = 1; Line < 9; Line++)
        {
			for (int Col = 1; Col < 9; Col++)
            {
                Console.Write(${ GrilleEtatBateau[Col,Line], 2});
            }
            Console.WriteLine();
        }

    }
}
public class LaGrille
{
    static int[,] IdBateau { get; set; } = new int[8, 8]; // ref bâteau ou 0 par défaut
    static char[,] EtatBateau { get; set; } = new char[8, 8];// (I)ntact (V)ide (C)oulé

    public static LaGrille Genere()
    {
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                EtatBateau[Col, Line] = (char)(32 + Line + Col);
                IdBateau[Col, Line] = 0;
            }
        }
        
        return EtatBateau, IdBateau;
    }
}

