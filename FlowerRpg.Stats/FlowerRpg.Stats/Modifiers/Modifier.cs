namespace FlowerRpg.Stats.Modifiers;

public class Modifier(
    float value,
    ModifierType type,
    Object? source = null
    ) : IModifier
{
    public event Action<float> OnValueChanged = delegate { };
    
    public Modifier(ModifierType type, float value, object source)
        : this(value, type, source) {}
    
    public Modifier(ModifierType type, float value)
        : this(value, type) {}

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged.Invoke(value);
        }
    }

    private float _value = value;
    
    public Object? Source { get; } = source;
    public ModifierType Type { get; } = type;

    public float GetValue(float baseValue)
    {
        return Type switch
        {
            ModifierType.Flat => Value,
            ModifierType.PercentAdd => Value,
            ModifierType.PercentMult => Value,
            _ => baseValue
        };
    }

    public void SetValue(float value)
    {
        Value = value;
    }
}