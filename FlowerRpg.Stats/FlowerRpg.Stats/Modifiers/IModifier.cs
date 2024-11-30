namespace FlowerRpg.Stats.Modifiers;

public interface IModifier : IComparable<Modifier>
{
    float Value { get; }
    object? Source { get; }
    ModifierType Type { get; }
    float GetValue(float baseValue);

    int IComparable<Modifier>.CompareTo(Modifier other)
    {
        return Type.CompareTo(other.Type);
    }
}