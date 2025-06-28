using FlowerRpg.Stats.Modifiers;

namespace FlowerRpg.Stats;

public sealed class Stat(float baseValue) : IStat<Stat>
{
    public event Action<float> OnValueChanged = delegate { };
    
    public static float operator +(Stat stat, float value) => stat.Value + value;
    public static float operator +(float value, Stat stat) => stat.Value + value;

    public static float operator -(Stat stat, float value) => stat.Value - value;
    public static float operator -(float value, Stat stat) => stat.Value - value;

    public static float operator *(Stat stat, float value) => stat.Value * value;
    public static float operator *(float value, Stat stat) => stat.Value * value;

    public static float operator /(Stat stat, float value) => stat.Value / value;
    public static float operator /(float value, Stat stat) => value / stat.Value;

    public float Value { get; private set; } = baseValue;

    private readonly List<IModifier> _modifiers = new ();
    
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
    
    public float BaseValue {
        get => _baseValue;
        private set
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
                default:
                    throw new ArgumentOutOfRangeException();
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
        
        modifier.OnValueChanged += _ => IsDirty = true;
        
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

    public IEnumerable<IModifier> GetModifiers() => _modifiers;
    
    public static implicit operator float(Stat stat) => stat.Value;
}