namespace BatailleNavale;

class DisplayGrid
{
    //[~] = eau inexplorée (bleu)
    //[x] = exploré touché (rouge)
    //[■] (alt 254) = exploré mais manqué (blanc)
    //[O] = Mes bateaux (vert)


    public static void Display(Grid Grid_)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        // Numérotation Abscisse
        Console.Write("   ");
        for (int j = 0; j < Grid_.Size; j++)
        {
            DisplayCell.DisplayCellIndex(j+1);
        }
        Console.WriteLine();

        //plateau de jeu
        for (int i = 0; i < Grid_.Size; i++)
        {
            DisplayCell.DisplayCellIndex(i, false); //Numérotation Ordonnées
            for (int j = -0; j < Grid_.Size; j++) // Ligne
            {
                /*
                if (Grid_.matrix[i][j].IsOccupied == true)
                {
                    DisplayCell.DisplayCellIsOccupied();
                }
                else if (Grid_.matrix[i][j].IsTouched == true)
                {
                    DisplayCell.DisplayCellIsTouched();
                }
                else if (Grid_.matrix[i][j].IsMisHit == true)
                {
                    DisplayCell.DisplayCellIsMisHit();
                }
                else if (Grid_.matrix[i][j].IsBlowed == true)
                {
                    DisplayCell.DisplayCellIsTouched();
                }
                else
                {
                    DisplayCell.DisplayCellIsUnexplored();
                }
                */
                DisplayCell.Display(Grid_.matrix[i][j]);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();
    }



}
