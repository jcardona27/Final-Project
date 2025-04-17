using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public sealed class GameEngine
{
    private static readonly Lazy<GameEngine> _instance = new(() => new GameEngine());
    public static GameEngine Instance => _instance.Value;

    private GameState _state;
    private List<Scenario> _scenarios;

    private GameEngine()
    {
        _state = new GameState();
        _scenarios = ScenarioLoader.LoadScenarios();
    }

    public async Task StartGameAsync()
    {
        Console.WriteLine("Welcome to Reigns: Console Edition!");

        foreach (var method in typeof(ScenarioLoader).GetMethods())
        {
            var attr = method.GetCustomAttribute<ScenarioAttribute>();
            if (attr != null)
            {
                Console.WriteLine($"[Scenario Tag: {attr.Tag}]");
            }
        }

        foreach (var scenario in _scenarios)
        {
            Console.WriteLine($"\n{scenario.Text}");
            for (int i = 0; i < scenario.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {scenario.Options[i].Description}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > scenario.Options.Count)
            {
                Console.WriteLine("Invalid choice. Try again.");
            }

            try
            {
                scenario.Options[choice - 1].ApplyEffect(_state);
                Console.WriteLine($"Status: {_state}");
            }
            catch (GameException ex)
            {
                Console.WriteLine($"Game error: {ex.Message}");
            }

            await Task.Delay(500);
        }

        await SaveGameAsync();
        Console.WriteLine("Game Over. Thanks for playing!");
    }

    private async Task SaveGameAsync()
    {
        try
        {
            string json = JsonSerializer.Serialize(_state);
            await File.WriteAllTextAsync("gamestate.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving game: " + ex.Message);
        }
    }
}




