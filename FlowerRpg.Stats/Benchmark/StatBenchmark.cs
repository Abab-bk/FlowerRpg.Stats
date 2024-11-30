using BenchmarkDotNet.Attributes;
using FlowerRpg.Stats;
using FlowerRpg.Stats.Modifiers;

namespace Benchmark;

public class StatBenchmark
{
    [Benchmark]
    [Arguments(1000)]
    public void AddModifiers(int modifierCount)
    {
        var stat = new Stat(10f);

        for (int i = 0; i < modifierCount; i++)
        {
            stat.AddModifier(CreateModifier());
        }
    }

    [Benchmark]
    [Arguments(1000)]
    public void HasModifier(int modifierCount)
    {
        var stat = GetStatWithModifiers(modifierCount);

        for (int i = 0; i < 10; i++)
        {
            stat.HasModifier(CreateModifier());
        }
    }

    private Stat GetStatWithModifiers(int modifierCount)
    {
        var stat = new Stat(10f);

        for (int i = 0; i < modifierCount; i++)
        {
            stat.AddModifier(CreateModifier());
        }
        
        return stat;
    }

    private Modifier CreateModifier() => new Modifier(ModifierType.Flat, 5f);
}