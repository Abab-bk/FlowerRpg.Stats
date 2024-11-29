namespace FlowerRpg.Stats.Modifiers;

public class Modifier(
    float value,
    ModifierType type,
    int order = 0,
    Object? source = null
    ) : IModifier, IEquatable<Modifier>, IComparable<Modifier>
{
    public Modifier(ModifierType type, float value, int order, object source)
        : this(value, type, order, source) {}
    
    public Modifier(ModifierType type, float value, int order)
        : this(value, type, order) {}
    
    public Modifier(ModifierType type, float value, object source)
        : this(value, type, 0, source) {}
    
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

    public int Order { get; } = order;

    public static bool operator ==(Modifier left, Modifier right) => left.Equals(right);
    public static bool operator !=(Modifier left, Modifier right) => !left.Equals(right);
    public static bool operator <(Modifier left, Modifier right) => left.CompareTo(right) < 0;
    public static bool operator >(Modifier left, Modifier right) => left.CompareTo(right) > 0;
    public static bool operator <=(Modifier left, Modifier right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Modifier left, Modifier right) => left.CompareTo(right) >= 0;
    
    public bool Equals(Modifier other)
    {
        return Value.Equals(other.Value) &&
               Equals(Source, other.Source) &&
               Type == other.Type &&
               Order == other.Order;
    }

    public int CompareTo(Modifier other)
    {
        return Order.CompareTo(other.Order);
    }

    public override bool Equals(object? obj)
    {
        return obj is Modifier other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Source, (int)Type, Order);
    }
}