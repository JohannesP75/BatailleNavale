
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
    public static void DisplayCellIsUnexplored()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("[~]");
        Console.ResetColor();
    }

    public static void DisplayCellIsTouched()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[x]");
        Console.ResetColor();
    }

    public static void DisplayCellIsOccupied()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[O]");
        Console.ResetColor();
    }

    public static void DisplayCellIsMisHit()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[■]");
        Console.ResetColor();
    }

    public static void DisplayCellIndex(int index)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"[{index}]");
        Console.ResetColor();
    }

    public static void Display(Cell c)
    {
        Console.ForegroundColor = CellColor[(int)c.CellType];
        Console.Write($"[{CellImage[(int)c.CellType]}]");
        Console.ResetColor();
    }
}

