using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class ScenarioLoader
{
    [Scenario("starter")]
    public static List<Scenario> LoadScenarios()
    {
        return new List<Scenario>
{
new Scenario
{
Text = "A healer offers you a potion.",
Options = new List<Decision>
{
new Decision
{
Description = "Drink it",
Effects = new() { { StatType.Health, 10 }, { StatType.Wealth, -5 } }
},
new Decision
{
Description = "Decline",
Effects = new() { { StatType.Popularity, -5 } }
}
}
}
};
    }

    public static List<Scenario> LoadFromJson(string path)
    {
        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Scenario>>(json);
        }
        catch
        {
            Console.WriteLine("Failed to load scenarios from JSON. Falling back to default.");
            return LoadScenarios();
        }
    }
}