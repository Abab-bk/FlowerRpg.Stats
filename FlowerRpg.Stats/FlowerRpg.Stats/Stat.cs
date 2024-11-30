using FlowerRpg.Stats.Modifiers;

namespace FlowerRpg.Stats;

public sealed class Stat(float baseValue) : IStat
{
    public event Action<float> OnValueChanged = delegate { };

    public float Value { get; private set; } = baseValue;

    private readonly List<Modifier> _modifiers = new ();
    
    private bool IsDirty {
        get => _isDirty;
        set
        {
            _isDirty = value;
            if (!IsDirty) return;
            Value = CalculateValue();
            OnValueChanged.Invoke(Value);
            _isDirty = false;
        }
    }
    private bool _isDirty = true;
    
    private float BaseValue {
        get => _baseValue;
        set
        {
            if (_baseValue.Equals(value)) return;
            _baseValue = value;
            IsDirty = true;
        }
    }
    
    private float _baseValue = baseValue;
    
    private float CalculateValue()
    {
        var finalValue = BaseValue;
        var flatValue = 0f;
        var percentAddValue = 0f;
        var percentMultValue = 0f;
        
        foreach (var modifier in _modifiers)
        {
            switch (modifier.Type)
            {
                case ModifierType.Flat:
                    flatValue += modifier.GetValue(BaseValue);
                    break;
                case ModifierType.PercentAdd:
                    percentAddValue += modifier.GetValue(BaseValue);
                    break;
                case ModifierType.PercentMult:
                    percentMultValue += modifier.GetValue(BaseValue);
                    break;
            }
        }

        finalValue += flatValue;
        finalValue += BaseValue * percentAddValue;
        finalValue *= 1 + percentMultValue;
        
        return (float)Math.Round(finalValue, 4);
    }

    public bool HasModifier(Modifier modifier) => _modifiers.Contains(modifier);

    public bool AddModifier(Modifier modifier)
    {
        _modifiers.Add(modifier);
        
        IsDirty = true;
        return true;
    }

    public bool RemoveModifier(Modifier modifier)
    {
        var result = _modifiers.Remove(modifier);
        
        IsDirty = result;
        return result;
    }

    public void RemoveAllModifiers()
    {
        _modifiers.Clear();
        IsDirty = true;
    }

    public void RemoveAllModifiersFromSource(object source)
    {
        _modifiers.RemoveAll(m => m.Source == source);
        IsDirty = true;
    }

    public void SetBaseValue(float value)
    {
        BaseValue = value;
    }

    public IEnumerable<Modifier> GetModifiers() => _modifiers;
}