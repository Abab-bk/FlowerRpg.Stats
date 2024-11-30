namespace FlowerRpg.Stats.Modifiers;

/// <summary>
/// Represents a modifier that can be applied to a IStat.
/// </summary>
public interface IModifier : IComparable<Modifier>
{
    /// <summary>
    /// Gets the value of the modifier.
    /// </summary>
    float Value { get; }

    /// <summary>
    /// Gets the source of the modifier, or null if it is not applicable.
    /// </summary>
    object? Source { get; }

    /// <summary>
    /// Gets the type of the modifier.
    /// </summary>
    ModifierType Type { get; }

    /// <summary>
    /// Gets the modified value based on the provided base value.
    /// </summary>
    /// <param name="baseValue">The base value to be modified.</param>
    /// <returns>The modified value.</returns>
    float GetValue(float baseValue);

    /// <summary>
    /// Compares this modifier to another modifier based on their types.
    /// </summary>
    /// <param name="other">The other modifier to compare with.</param>
    /// <returns>A negative integer, zero, or a positive integer if this modifier is less than, equal to, or greater than the other modifier, respectively.</returns>
    int IComparable<Modifier>.CompareTo(Modifier other)
    {
        return Type.CompareTo(other.Type);
    }
}