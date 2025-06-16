using BenchmarkDotNet.Attributes;
using FlowerRpg.Stats;
using FlowerRpg.Stats.Modifiers;

namespace Benchmark;

[MemoryDiagnoser]
public class StatBenchmark
{
    private Stat _stat = null!;
    private Vital _vital = null!;
    private readonly object _source = new();
    private Modifier _modifierToAddAndRemove = null!;
    private List<Modifier> _initialModifiers = null!;

    [Params(1000, 10000)]
    public int ModifierCount { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _initialModifiers = new List<Modifier>(ModifierCount);
        for (int i = 0; i < ModifierCount; i++)
        {
            var type = (ModifierType)(i % 3);
            var source = (i % 10 == 0) ? _source : null;
            _initialModifiers.Add(new Modifier(1f, type, source));
        }

        _modifierToAddAndRemove = new Modifier(ModifierType.Flat, 100f);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _stat = new Stat(100f);
        foreach (var modifier in _initialModifiers)
        {
            _stat.AddModifier(modifier);
        }
        
        _ = _stat.Value; 

        _vital = new Vital(_stat, 0, _stat.Value);
    }

    [Benchmark(Description = "Stat: Calculate Value")]
    public float CalculateStatValue()
    {
        _stat.SetBaseValue(101f);
        return _stat.Value;
    }

    [Benchmark(Description = "Stat: Add & Remove Modifier")]
    public void AddAndRemoveModifier()
    {
        _stat.AddModifier(_modifierToAddAndRemove);
        _stat.RemoveModifier(_modifierToAddAndRemove);
    }
    
    [Benchmark(Description = "Vital: Update on Stat Change")]
    public float UpdateVitalOnStatChange()
    {
        _stat.SetBaseValue(120f);
        return _vital.Value;
    }
}