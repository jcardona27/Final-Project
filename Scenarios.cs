using System.Collections.Generic;
using System.Linq;

public class Scenario
{
    public string Text { get; set; }
    public List<Decision> Options { get; set; }
}

public class Decision
{
    public string Description { get; set; }
    public Dictionary<StatType, int> Effects { get; set; }

    public void ApplyEffect(GameState state)
    {
        if (Effects == null || state == null)
            throw new GameException("Decision or state is null.");

        Effects.ToList().ForEach(effect => state.Update(effect.Key, effect.Value));
    }
}