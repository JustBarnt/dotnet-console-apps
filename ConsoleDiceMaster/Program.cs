using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using DiceMasters.Classes;

namespace DiceMasters;

class Program
{
    private static void Main()
    {
        Console.Clear();
        GameController gameController = new();
        gameController.LoadGame();
        
        while(true)
        {
            Console.WriteLine("Welcome to DiceMasters!");
            Console.WriteLine("Press any key to continue...");
            Console.Write($"Wins: {gameController.GameData.Wins}, Losses: {gameController.GameData.Losses}");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                gameController.QuitGame();
            }
            
            Console.Clear();
            GameOptions[] items = Enum.GetValues<GameOptions>();
            GameOptions selection = TUI.ShowMenu(items);
            gameController.GameData.Options = selection;
            gameController.StartGame();

            return;
        }
    }
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute? attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                              .FirstOrDefault() as DescriptionAttribute;

        return attribute?.Description ?? value.ToString();
    }
}
