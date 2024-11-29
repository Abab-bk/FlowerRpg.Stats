using BenchmarkDotNet.Attributes;
using FlowerRpg.Stats;
using FlowerRpg.Stats.Modifiers;

namespace Benchmark;

public class StatBenchmark
{
    [Benchmark]
    [Arguments(1000)]
    [Arguments(10000)]
    // [Arguments(100000)]
    public void AddModifiers(int modifierCount)
    {
        var stat = new Stat(10f);

        for (int i = 0; i < modifierCount; i++)
        {
            stat.AddModifier(CreateModifier());
        }
    }
    
    private Modifier CreateModifier() => new Modifier(ModifierType.Flat, 5f);
}