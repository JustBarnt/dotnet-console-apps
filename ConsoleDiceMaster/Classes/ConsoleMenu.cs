using System.ComponentModel;
using DiceMasters.Interfaces;

namespace DiceMasters.Classes;

public enum MenuOptions
{
    [Description("(Easy Mode)")]
    D6,
    [Description("(Medium Mode)")]
    D10,
    [Description("(Hard Mode)")]
    D20,
    [Description("(Crit Fail - Exit)")]
    IConsoleMenu
}

class ConsoleMenu() : ITerminalUI
{
    private MenuOptions[] MenuItems { get; init; } = Enum.GetValues<MenuOptions>();

    public MenuOptions ShowMenu()
    {
        int current_selection = 0;

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (i == current_selection)
                {
                    Console.WriteLine($"> {MenuItems[i]} {MenuItems[i].GetDescription()}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{MenuItems[i]} {MenuItems[i].GetDescription()}");
                }
            }

            ConsoleKeyInfo key_info = Console.ReadKey(true);

            switch (key_info.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.K:
                    current_selection = (current_selection == 0) ? MenuItems.Length - 1 : current_selection - 1;
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.J:
                    current_selection = (current_selection == MenuItems.Length - 1) ? 0 : current_selection + 1;
                    break;

                case ConsoleKey.Enter:
                    return MenuItems[current_selection];
            }
        }
    }
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                              .FirstOrDefault() as DescriptionAttribute;

        return attribute?.Description ?? value.ToString();
    }
}
