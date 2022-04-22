namespace BatailleNavale;

class DisplayCell
{
    /// <summary>
    /// Tableau représentant les dessins des images à afficher (l'index est un CellType)
    /// </summary>
    public static readonly char[] CellImage = new char[] { '~', 'x', 'O', '■' };
    /// <summary>
    /// Tableau représentant les couleurs des images à afficher (l'index est un CellType)
    /// </summary>
    public static readonly ConsoleColor[] CellColor = new ConsoleColor[] { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.White };


    public static void DisplayCellIndex(int index, bool abscisse=true)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"[{((abscisse) ? index : Char.ConvertFromUtf32(0x41 + index))}]"); // 0x41 représene 'A' sur UTF-16
        Console.ResetColor();
    }

    public static void Display(Cell c)
    {
       // Console.WriteLine(" ForegroundColor : {0}", CellColor[(int)c.CellType]);

        Console.ForegroundColor = CellColor[(int)c.CellType];
        Console.Write($"[{CellImage[(int)c.CellType]}]");
        Console.ResetColor();
    }
}