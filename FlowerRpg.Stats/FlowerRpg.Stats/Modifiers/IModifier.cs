namespace FlowerRpg.Stats.Modifiers;

/// <summary>
/// Represents a modifier that can be applied to a IStat.
/// </summary>
public interface IModifier
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
    
    void SetValue(float value);
}