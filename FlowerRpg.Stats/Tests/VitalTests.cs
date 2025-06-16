using FlowerRpg.Stats;

namespace Tests;

public class VitalTests
{
    private const float BaseValue = 100;
    private const float MinValue = 0;
    
    private Vital GetBaseFullVital() => new Vital(new Stat(BaseValue), MinValue, BaseValue);
    
    [Fact]
    public void Vital_SetMinValueGreaterThanMaxValue_ShouldThrowException()
    {
        var vital = GetBaseFullVital();
        
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            vital.SetMinValue(BaseValue * 2);
        });
    }
    
    [Fact]
    public void Vital_RatioIsLessZero_ShouldThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var vital = new Vital(new Stat(100), 0, 100, true, -1);
        });

        var vital2 = GetBaseFullVital();
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            vital2.SetValueByRatio(-1);
        });
    }
    
    
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0.5f)]
    [InlineData(10, 0.1f)]
    [InlineData(100, 0f)]
    public void Vital_Value_IsRatioValueWhenUseRatioIsTrue(float baseValue, float ratio)
    {
        // Arrange
        var vital = new Vital(new Stat(baseValue), 0, 0, true, ratio);

        // Act
        var value = vital.Value;

        // Assert
        Assert.Equal(baseValue * ratio, value);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0.5f)]
    [InlineData(10, 0.1f)]
    public void ValueRatioKeep_WhenNewMaxValueSet(float baseValue, float ratio)
    {
        // Arrange
        var vital = new Vital(new Stat(baseValue), 0, 0, true, ratio);
        
        vital.SetMaxValue(new Stat(200));
        
        // Assert
        Assert.Equal(ratio, vital.GetRatio());
    }
    
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0.5f)]
    [InlineData(10, 0.1f)]
    public void KeepValueRatio_WhenMaxValueChanges(float baseValue, float ratio)
    {
        var vital = new Vital(new Stat(baseValue), 0, 0, true, ratio);
        var testRatio = vital.GetRatio();
        vital.MaxValue.SetBaseValue(200);
        Assert.Equal(testRatio, vital.GetRatio());
    }
    
    [Fact]
    public void KeepValueRatio_WhenMaxValueIsNegative_ShouldThrowException()
    {
        var vital = new Vital(new Stat(200f), 0, 200, true, 1f);
        Assert.Throws<InvalidOperationException>(() => vital.MaxValue.SetBaseValue(-200));
    }

    [Fact]
    public void Constructor_WithNegativeMaxValue_ShouldThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Vital(new Stat(-100f), 0, 100, true, 0.5f));
    }

    [Fact]
    public void Constructor_WithNegativeMinValue_ShouldThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Vital(new Stat(100f), -10, 100, true, 0.5f));
    }

    
    [Theory]
    [InlineData(200, 1)]
    [InlineData(200, 0.5f)]
    [InlineData(1200, 0.1f)]
    public void KeepValueRatio_WhenMinValueChanges(float baseValue, float ratio)
    {
        // Arrange
        var vital = new Vital(new Stat(baseValue), 0, 0, true, ratio);
        var testRatio = vital.GetRatio();

        vital.SetMinValue(50f);
        
        // Assert
        Assert.Equal(testRatio, vital.GetRatio());
    }
    
    [Fact]
    public void MinValueChanges_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.SetMinValue(50f);
        
        Assert.True(invoked);
    }

    [Fact]
    public void MaxValueChanges_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.MaxValue.SetBaseValue(200f);
        
        Assert.True(invoked);
    }
    
    [Fact]
    public void Increase_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.Increase(10f);
        
        Assert.True(invoked);
    }
    
    [Fact]
    public void Decrease_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.Decrease(10f);
        
        Assert.True(invoked);
    }
    
    [Fact]
    public void ResetToMin_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.ResetToMin();
        
        Assert.True(invoked);
    }
    
    [Fact]
    public void ResetToMax_Should_InvokeOnValueChanged()
    {
        var invoked = false;
        var vital = GetBaseFullVital();
        
        vital.OnValueChanged += _ => invoked = true;
        vital.ResetToMax();
        
        Assert.True(invoked);
    }

    [Fact]
    public void Increase_Should_IncreaseValue()
    {
        var vital = GetBaseFullVital();
        vital.SetMaxValue(new Stat(BaseValue + 10f));
        
        vital.Increase(10f);
        
        Assert.Equal(BaseValue + 10f, vital.Value);
    }
    
    [Fact]
    public void Decrease_Should_DecreaseValue()
    {
        var vital = GetBaseFullVital();
        
        vital.Decrease(10f);
        
        Assert.Equal(BaseValue - 10f, vital.Value);
    }
    
    [Fact]
    public void ResetToMin_Should_SetValueToMin()
    {
        var vital = GetBaseFullVital();
        
        vital.ResetToMin();
        
        Assert.Equal(MinValue, vital.Value);
    }
    
    [Fact]
    public void ResetToMax_Should_SetValueToMax()
    {
        var vital = GetBaseFullVital();
        
        vital.ResetToMax();
        
        Assert.Equal(BaseValue, vital.Value);
    }
    
    [Fact]
    public void SetValue_Should_SetValue()
    {
        var vital = GetBaseFullVital();
        vital.SetValue(10f);
        Assert.Equal(10f, vital.Value);
    }
    
    
}