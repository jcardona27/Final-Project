using System; 

public class GameException:Exception
{
    public GameException(string message): base(message) {}
}

public enum StatType
{
    Health,
    Wealth,
    Popularity
}

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ScenarioAttribute : Attribute
{
    public string Tag {get;}

    public ScenarioAttribute (string tag)
    {
        Tag = tag;
    }
}
