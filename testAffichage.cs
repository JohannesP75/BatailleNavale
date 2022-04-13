// Test affichage
//using BatailleNavale;

LaGrille CetteGrille = new();
CetteGrille.Genere();
Affichage MonEcran = new();
MonEcran.Grille(CetteGrille);

//namespace BatailleNavale;
class Affichage
{
    public void Grille(LaGrille laGrille)
    {
        if (laGrille == null) return;
        Console.Clear();
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                char? cetEtat = laGrille.LesCases?[Col, Line].état;
                Console.Write($"{ cetEtat, 2}");
            }
            Console.WriteLine();
        }

    }
}
internal class LaGrille
{
    public Case[,] LesCases { get; set; } = new Case[8, 8]; // static fait partager le même attribut en mémoire
    public void Genere()
    {
        if (LesCases == null) return;
        for (int Line = 1; Line < 9; Line++)
        {
            for (int Col = 1; Col < 9; Col++)
            {
                LesCases[Col, Line].état = 'C'; // (char)(32 + Line + Col);
                LesCases[Col, Line].identifiant = 0;
            }
        }
    }
}

internal class Case
{
    public int identifiant; // ref bâteau ou 0 par défaut
    public char état; // (I)ntact (V)ide (C)oulé
}

*/