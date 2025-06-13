using FlowerRpg.Stats;
using FlowerRpg.Stats.Modifiers;

namespace Tests;

public class StatTests
{
    [Theory]
    [InlineData(5f)]
    [InlineData(-29)]
    public void Stat_AddFlatModifier_Correctly(float flatValue)
    {
        var stat = new Stat(10f);
        stat.AddModifier(new Modifier(ModifierType.Flat, flatValue));
        Assert.Equal(10f + flatValue, stat.Value);
    }
    
    [Theory]
    [InlineData(5f)]
    [InlineData(-29)]
    public void Stat_AddPercentAddModifier_Correctly(float percentAddValue)
    {
        var stat = new Stat(10f);
        stat.AddModifier(new Modifier(ModifierType.PercentAdd, percentAddValue));
        Assert.Equal(10f + 10f * percentAddValue, stat.Value);
    }
    
    [Theory]
    [InlineData(5f)]
    [InlineData(-29)]
    public void Stat_AddPercentMultModifier_Correctly(float percentMultValue)
    {
        var stat = new Stat(10f);
        stat.AddModifier(new Modifier(ModifierType.PercentMult, percentMultValue));
        Assert.Equal(10f * (1 + percentMultValue), stat.Value);
    }
    
    [Theory]
    [InlineData(5f, 0.4f, 10f)]
    [InlineData(0f, 0f, 10f)]
    [InlineData(-5f, 0f, -10f)]
    [InlineData(-10, -10, -10)]
    public void Stat_Value_Changed_Correctly_When_Added_Multiple_Modifiers(
        float flatValue,
        float percentAddValue,
        float percentMultValue
        )
    {
        var flatModifierCount = 4;
        var percentAddModifierCount = 4;
        var percentMultModifierCount = 4;
        var baseValue = 10f;
        
        // Arrange
        var stat = new Stat(baseValue);
        
        for (var i = 0; i < flatModifierCount; i++)
        {
            stat.AddModifier(new Modifier(ModifierType.Flat, flatValue));
        }
        
        for (var i = 0; i < percentAddModifierCount; i++)
        {
            stat.AddModifier(new Modifier(ModifierType.PercentAdd, percentAddValue));
        }
        
        for (var i = 0; i < percentMultModifierCount; i++)
        {
            stat.AddModifier(new Modifier(ModifierType.PercentMult, percentMultValue));
        }
    
        var expectedValue = baseValue;
        expectedValue += flatModifierCount * flatValue;
        expectedValue += baseValue * (percentAddModifierCount * percentAddValue);
        expectedValue *= 1 + (percentMultModifierCount * percentMultValue);
        
        // Assert
        Assert.Equal(expectedValue, stat.Value);
    }

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
    
    [Fact]
    public void AddModifier_ShouldHasModifier()
    {
        // Arrange
        var stat = new Stat(10f);
        var modifier = new Modifier(ModifierType.Flat, 5f, 1);
        
        // Act
        stat.AddModifier(modifier);
        
        // Assert
        Assert.True(stat.HasModifier(modifier));
    }
    
    [Fact]
    public void RemoveModifier_ShouldNotHasModifier()
    {
        // Arrange
        var stat = new Stat(10f);
        var modifier = new Modifier(ModifierType.Flat, 5f, 1);
        stat.AddModifier(modifier);
        
        // Act
        stat.RemoveModifier(modifier);
        
        // Assert
        Assert.False(stat.HasModifier(modifier));
    }
    
    [Fact]
    public void RemoveAllModifiersFromSource_ShouldNotHasModifier()
    {
        var stat = new Stat(10f);
        var modifier = new Modifier(ModifierType.Flat, 5f, this);
        stat.AddModifier(modifier);

        if (modifier.Source != null)
        {
            stat.RemoveAllModifiersFromSource(modifier.Source);
            Assert.False(stat.HasModifier(modifier));
            return;
        }
        
        Assert.True(false);
    }

    [Fact]
    public void Modifier_OnValueChanged_ShouldInvoke_OnValueChanged()
    {
        // Arrange
        var stat = new Stat(10f);
        var modifier = new Modifier(ModifierType.Flat, 5f, 1);
        
        stat.AddModifier(modifier);
        
        var invoked = false;
        
        stat.OnValueChanged += _ => invoked = true;
        
        // Act
        modifier.SetValue(10f);
        
        // Assert
        Assert.True(invoked);
    }
    
    [Theory]
    [InlineData(0f, 5f, 5f)]
    [InlineData(10f, -3f, 7f)]
    [InlineData(2.5f, 1.5f, 4f)]
    public void AdditionOperator_WorksWithVariousValues(float initialValue, float addValue, float expected)
    {
        var stat = new Stat(initialValue);
        Assert.Equal(expected, stat + addValue);
    }
    
    [Theory]
    [InlineData(10f, 2f, 20f)]
    [InlineData(3f, 0.5f, 1.5f)]
    [InlineData(-4f, 2f, -8f)]
    public void MultiplicationOperator_WorksWithVariousValues(float initialValue, float multiplier, float expected)
    {
        var stat = new Stat(initialValue);
        Assert.Equal(expected, stat * multiplier);
    }
    
    [Theory]
    [InlineData(10f, 2f, 5f)]
    [InlineData(10f, 0.5f, 20f)]
    [InlineData(10f, -2f, -5f)]
    public void DivisionOperator_WorksWithVariousValues(float initialValue, float divisor, float expected)
    {
        var stat = new Stat(initialValue);
        Assert.Equal(expected, stat / divisor);
    }

    [Theory]
    [InlineData(10f, 2f, 8f)]
    [InlineData(10f, 0.5f, 9.5f)]
    [InlineData(10f, -2f, 12f)]
    public void SubtractionOperator_WorksWithVariousValues(float initialValue, float subtrahend, float expected)
    {
        var stat = new Stat(initialValue);
        Assert.Equal(expected, stat - subtrahend);
    }

    // public void HowToUse()
    // {
    //     var stat = new Stat(
    //         baseValue: 100f
    //         );
    //     
    //     var flatModifier = new Modifier(
    //         type: ModifierType.Flat,
    //         value: 10f
    //         );
    //     
    //     var percentAddModifier = new Modifier(
    //         type: ModifierType.PercentAdd,
    //         value: 0.1f
    //         );
    //     
    //     var percentMultModifier = new Modifier(
    //         type: ModifierType.PercentMult,
    //         value: 0.1f
    //         );
    //     
    //     stat.AddModifier(flatModifier);
    //     
    //     var value = stat.Value; // value = 100 + 10 = 110
    //     
    //     // vital has max and min value
    //     var vital = new Vital(
    //         maxValue: new Stat(100f),
    //         minValue: 0f,
    //         value: 33f
    //         );
    //     
    //     // create with ratio
    //     var vital2 = new Vital(
    //         maxValue: new Stat(100f),
    //         minValue: 0f,
    //         value: 0f,
    //         useRatio: true,
    //         ratio: 0.5f
    //     );
    //     
    //     var vitalValue = vital.Value; // 30
    //     var vitalValue2 = vital2.Value; // 100 * 0.5 = 50
    // }
}