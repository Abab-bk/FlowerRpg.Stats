using FlowerRpg.Stats.Modifiers;

namespace FlowerRpg.Stats;

public interface IStat
{
    public event Action<float> OnValueChanged;
    public float Value { get; }
    
    public bool HasModifier(Modifier modifier);
    public bool AddModifier(Modifier modifier);
    public bool RemoveModifier(Modifier modifier);
    public void RemoveAllModifiers();
    public void RemoveAllModifiersFromSource(object source);
    public void SetBaseValue(float value);
    
    public IEnumerable<Modifier> GetModifiers();
}