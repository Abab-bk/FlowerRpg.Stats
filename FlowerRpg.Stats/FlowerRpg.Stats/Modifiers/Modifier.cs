namespace FlowerRpg.Stats.Modifiers;

public readonly struct Modifier(
    float value,
    ModifierType type,
    Object? source = null
    ) : IModifier, IEquatable<Modifier>
{
    public Modifier(ModifierType type, float value, object source)
        : this(value, type, source) {}
    
    public Modifier(ModifierType type, float value)
        : this(value, type) {}
    
    public float Value { get; } = value;
    public Object? Source { get; } = source;
    public ModifierType Type { get; } = type;

    public float GetValue(float baseValue)
    {
        switch (Type)
        {
            case ModifierType.Flat:
                return Value;
            case ModifierType.PercentAdd:
                return Value;
            case ModifierType.PercentMult:
                return Value;
            default:
                return baseValue;
        }
    }
    
    public static bool operator ==(Modifier left, Modifier right) => left.Equals(right);
    public static bool operator !=(Modifier left, Modifier right) => !left.Equals(right);
    
    public bool Equals(Modifier other)
    {
        return Value.Equals(other.Value) &&
               Equals(Source, other.Source) &&
               Type == other.Type;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Modifier other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Source, (int)Type);
    }
}