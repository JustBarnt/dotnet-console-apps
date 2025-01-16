using DiceMasters.Classes;

namespace DiceMasters;

class Program
{
    static void Main()
    {
        Console.WriteLine(
                "Welcome to DiceMasters!\r\n" +
                "The game where a dice is randomly rolled and you try to guess the number!\r\n" +
                "Please choose the difficulty setting you wish to play, or exit the game.");

        ConsoleMenu tui = new();
        MenuOptions SelectedOption = tui.ShowMenu();

        Console.Clear();
        if (SelectedOption is MenuOptions.IConsoleMenu)
        {
            Console.WriteLine("Crit Fail!\r\nApplication has shutdown...");
            Environment.Exit(0);
        }

        Console.WriteLine($"Current Difficulty: {SelectedOption}");
        Console.WriteLine("Press any key to start the game!");
        Console.ReadKey(true);
    }
}
