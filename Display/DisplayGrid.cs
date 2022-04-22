namespace BatailleNavale;

class DisplayGrid
{
    //[~] = eau inexplorée (bleu)
    //[x] = exploré touché (rouge)
    //[■] (alt 254) = exploré mais manqué (blanc)
    //[O] = Mes bateaux (vert)


    public static void Display(Grid Grid_)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();


        // Numérotation Abscisse
        Console.Write("   ");
        Console.Write("                                  ");

        for (int j = 0; j < Grid_.size; j++)
        {
            DisplayCell.DisplayCellIndex(j);
        }
        Console.WriteLine();

        //plateau de jeu
        for (int i = 0; i < Grid_.size; i++)
        {
            Console.Write("                                  ");
            DisplayCell.DisplayCellIndex(i, false); //Numérotation Ordonnées
            for (int j = -0; j < Grid_.size; j++) // Ligne
            {


                DisplayCell.Display(Grid_.matrix[i][j]);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();


    }
}
