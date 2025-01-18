using System.ComponentModel;
using System.Text.Json;
using DiceMasters.Interfaces;

namespace DiceMasters.Classes;

public enum GameOptions
{
    [Description("(Easy)")]
    D6,
    [Description("(Medium)")]
    D10,
    [Description("(Hard)")]
    D20
}

public class GameData(int wins, int losses, GameOptions options)
{
    public int Wins { get; set; } = wins;
    public int Losses { get; set; } = losses;
    public GameOptions Options { get; set; } = options;
}

public class GameController : IGameController
{
    private readonly string _savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dicemasters.json");
    private int _currentRoll = 0;
    public GameData GameData { get; set; }

    public GameData LoadGame()
    {
        GameData = new GameData(0,0, GameOptions.D6);

        if (!File.Exists(_savePath))
        {
            SaveGame();
            return GameData;
        }
        
        string json = File.ReadAllText(_savePath);
        GameData = JsonSerializer.Deserialize<GameData>(json);
        return GameData;
    }

    public void StartGame()
    {
        while (true)
        {
            Console.Clear();
            bool gameWon = RollDice();

            if (gameWon) GameData.Wins += 1;
            else GameData.Losses += 1;

            Console.WriteLine("Would you like to play again? (y/n)");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape or ConsoleKey.N:
                    QuitGame();
                    break;
                case ConsoleKey.Y:
                    continue;
            }
            
            break;
        }
    }

    private bool RollDice()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Time to roll the {GameData.Options}!\r\n");
            Random random = new();
            _currentRoll = GameData.Options switch
            {
                GameOptions.D6 => random.Next(1, 6),
                GameOptions.D10 => random.Next(1, 10),
                GameOptions.D20 => random.Next(1, 20),
                _ => _currentRoll
            };

            string? userGuess = Console.ReadLine();

            if (userGuess == "")
            {
                Console.WriteLine("Please enter a valid guess!");
                continue;
            }
            
            int guess = Convert.ToInt32(userGuess);

            if (guess is 0)
            {
                Console.WriteLine("0 is not a possible outcome... Let's re-roll the dice and try again.");
                continue;
            }

            if (guess == _currentRoll)
            {
                Console.WriteLine("Wow...\r\nDid you cheat?!\r\nYou actually guessed that right...\r\nCongrats...");
                return true;
            }

            Console.WriteLine($"Sorry, that's not correct. Would you like to play again?");
            return false;
        }
    }

    public void QuitGame()
    {
        Console.WriteLine("Saving DiceMasters data...");
        SaveGame();
        Environment.Exit(0);
    }
        
    
    public void SaveGame()
    {
        string json = JsonSerializer.Serialize(GameData);
        File.WriteAllText(_savePath, json);
    }
}