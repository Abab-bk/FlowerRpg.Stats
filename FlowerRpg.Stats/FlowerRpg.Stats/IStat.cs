using FlowerRpg.Stats.Modifiers;

namespace FlowerRpg.Stats;

/// <summary>
/// Represents a statistical value that can be modified by various modifiers.
/// </summary>
public interface IStat
{
    /// <summary>
    /// Gets the current value of the stat.
    /// </summary>
    float Value { get; }

    /// <summary>
    /// Gets the base value of the stat, without any modifiers applied.
    /// </summary>
    float BaseValue { get; }

    /// <summary>
    /// Gets a collection of modifiers that are currently applied to the stat.
    /// </summary>
    public IEnumerable<IModifier> GetModifiers();

    /// <summary>
    /// Adds a modifier to the stat.
    /// </summary>
    /// <param name="modifier">The modifier to add.</param>
    /// <returns>True if the modifier was added successfully, false otherwise.</returns>
    bool AddModifier(Modifier modifier);

    /// <summary>
    /// Removes a modifier from the stat.
    /// </summary>
    /// <param name="modifier">The modifier to remove.</param>
    /// <returns>True if the modifier was removed successfully, false otherwise.</returns>
    bool RemoveModifier(Modifier modifier);

    /// <summary>
    /// Removes all modifiers from the stat.
    /// </summary>
    void RemoveAllModifiers();

    /// <summary>
    /// Removes all modifiers from the stat that were applied by a specific source.
    /// </summary>
    /// <param name="source">The source that applied the modifiers to remove.</param>
    void RemoveAllModifiersFromSource(object source);

    /// <summary>
    /// Sets the base value of the stat.
    /// </summary>
    /// <param name="value">The new base value.</param>
    void SetBaseValue(float value);

    /// <summary>
    /// Gets a value indicating whether the stat has a specific modifier applied.
    /// </summary>
    /// <param name="modifier">The modifier to check for.</param>
    /// <returns>True if the stat has the modifier applied, false otherwise.</returns>
    bool HasModifier(Modifier modifier);

    /// <summary>
    /// Occurs when the value of the stat changes.
    /// </summary>
    event Action<float> OnValueChanged;
    
    public static float operator +(IStat stat, float value) => stat.Value + value;
    public static float operator -(IStat stat, float value) => stat.Value - value;
    public static float operator *(IStat stat, float value) => stat.Value * value;
    public static float operator /(IStat stat, float value) => stat.Value / value;
    public static float operator +(float value, IStat stat) => value + stat.Value;
    public static float operator -(float value, IStat stat) => value - stat.Value;
    public static float operator *(float value, IStat stat) => value * stat.Value;
    public static float operator /(float value, IStat stat) => value / stat.Value;
}