namespace DiceMasters.Classes;

public static class TUI
{
    private static int consoleWidth = Console.WindowWidth;
    private static int consoleHeight = Console.WindowHeight;
    private static int currentRow = 0;
    
    public static GameOptions ShowMenu(GameOptions[] options)
    {
        int current_selection = 0;
    
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select your difficulty:");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == current_selection)
                {
                    Console.WriteLine($"> {options[i]} {options[i].GetDescription()}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{options[i]} {options[i].GetDescription()}");
                }
            }
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.K:
                    current_selection = (current_selection == 0) ? options.Length - 1 : current_selection - 1;
                    break;
    
                case ConsoleKey.DownArrow:
                case ConsoleKey.J:
                    current_selection = (current_selection == options.Length - 1) ? 0 : current_selection + 1;
                    break;
    
                case ConsoleKey.Enter:
                    return options[current_selection];
            }
        }
    }
    
    public static void WriteToCenter(List<string> lines)
    {
        int row = (consoleHeight - lines.Count) / 2;

        for(int i = 0; i < lines.Count; i++)
        {
            WriteToCenter(lines[i], row + i);
        }
    }
    
    public static void WriteToCenter(string text, int row)
    {
        int centerX = (consoleWidth - text.Length) / 2;
        if (row < 0 || row >= consoleHeight) return;
        Console.SetCursorPosition(centerX, row);
        Console.Write(text);
    }
}