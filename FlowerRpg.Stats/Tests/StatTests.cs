using FlowerRpg.Stats;
using FlowerRpg.Stats.Modifiers;

namespace Tests;

public class StatTests
{
    [Fact]
    public void Stat_Value_ReturnsBaseValue_WhenNoModifiers()
    {
        // Arrange
        var stat = new Stat(10f);

        // Act
        var value = stat.Value;

        // Assert
        Assert.Equal(10f, value);
    }

    [Fact]
    public void Stat_Value_ReturnsCalculatedValue_WhenModifiersAdded()
    {
        // Arrange
        var stat = new Stat(10f);
        stat.AddModifier(new Modifier(ModifierType.Flat, 5f, 1));

        // Act
        var value = stat.Value;

        // Assert
        Assert.Equal(15f, value);
    }

    [Fact]
    public void Stat_Value_ReturnsUpdatedValue_WhenModifierRemoved()
    {
        // Arrange
        var stat = new Stat(10f);
        var modifier = new Modifier(ModifierType.Flat, 5f, 1);
        stat.AddModifier(modifier);

        // Act
        stat.RemoveModifier(modifier);
        var value = stat.Value;

        // Assert
        Assert.Equal(10f, value);
    }

    [Fact]
    public void Stat_OnValueChanged_IsInvoked_WhenValueChanges()
    {
        // Arrange
        var stat = new Stat(10f);
        var invoked = false;
        stat.OnValueChanged += _ => invoked = true;

        // Act
        stat.AddModifier(new Modifier(ModifierType.Flat, 5f, 1));

        // Assert
        Assert.True(invoked);
    }
    
    [Fact]
    public void Stat_SetBaseValue_ShouldInvokeOnValueChanged()
    {
        // Arrange
        var stat = new Stat(10f);
        var invoked = false;
        stat.OnValueChanged += _ => invoked = true;
        
        // Act
        stat.SetBaseValue(20f);
        
        // Assert
        Assert.True(invoked);
    }
}