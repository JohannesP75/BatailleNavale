
namespace BatailleNavale;

class DisplayCell
{
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
}

