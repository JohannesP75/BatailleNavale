// Test affichage
/*
namespace BatailleNavale;

class Program
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
    public int identifiant; // ref bâteau ou 0 par défaut
    public char état; // (I)ntact (V)ide (C)oulé
}

// Test affichage
namespace BatailleNavale;

class Program
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
    public int identifiant; // ref bâteau ou 0 par défaut
    public char état; // (I)ntact (V)ide (C)oulé
}

*/