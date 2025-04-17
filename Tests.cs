using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;

[TestFixture]
public class GameTests
{
    [Test]
    public void TestStatUpdate()
    {
        var state = new GameState();
        state.Update(StatType.Health, 20);
        ClassicAssert.AreEqual(70, state.Health);
    }

    [Test]
    public void TestDecisionEffect()
    {
        var state = new GameState();
        var decision = new Decision
        {
            Description = "Boost health",
            Effects = new Dictionary<StatType, int> { { StatType.Health, 10 } }
        };
        decision.ApplyEffect(state);
        ClassicAssert.AreEqual(60, state.Health);
    }

    [Test]
    public void TestExceptionOnNull()
    {
        var decision = new Decision();
        Assert.Throws<GameException>(() => decision.ApplyEffect(null));
    }
}