// Test affichage
/*
LaGrille GrilleJoueur1 = new();
LaGrille GrilleJoueur2 = new();
LaGrille.Genere(GrilleJoueur1);
LaGrille.Genere(GrilleJoueur2);
Affichage MonEcran = new();
MonEcran.Grille(GrilleJoueur1, true);
MonEcran.Grille(GrilleJoueur2, false);
*/
class Affichage
{
    public void Grille(LaGrille laGrille, bool leJoueur)
    {
        if (laGrille == null) return;
        if (leJoueur)
        {
            Console.Clear();
            Console.WriteLine(" Notre grille:");
        }
        else Console.WriteLine("\n Adversaire:");
        for (int Line = 0; Line < 8; Line++)
        {
            if (Line == 0) Console.WriteLine("    A B C D E F G H");
            Console.Write($"{Line + 1,2} ");
            for (int Col = 0; Col < 8; Col++)
            {
                char? cetEtat = laGrille.LesCases?[Col, Line].etat;
                Console.Write($"{ cetEtat,2}");
            }
            Console.WriteLine();
        }

    }
}
class LaGrille
{
    public Case[,] LesCases { get; set; } = new Case[8, 8]; // static fait partager le même attribut en mémoire
    public static void Genere(LaGrille cetteGrille)
    {
        if (cetteGrille.LesCases == null) return;
        Case cetteCase = new();
        for (int Line = 0; Line < 8; Line++)
        {
            for (int Col = 0; Col < 8; Col++)
            {
                cetteCase.etat = 'V'; // (char)(32 + Line + Col);
                cetteCase.identifiant = 0;
                // Object reference not set to an instance of an object
                cetteGrille.LesCases[Col, Line] = cetteCase;
            }
        }
    }
}

class Case
{
    public int identifiant { get; set; } // ref bâteau ou 0 par défaut
    public char etat { get; set; } // (I)ntact (V)ide (C)oulé
}
