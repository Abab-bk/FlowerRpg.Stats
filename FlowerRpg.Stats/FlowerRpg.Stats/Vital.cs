namespace FlowerRpg.Stats;

/// <summary>
/// Vital represents a statistical value that has a maximum value and a minimum value.
/// </summary>
public class Vital
{
    public event Action OnValueToMax = delegate { };
    public event Action OnValueToMin = delegate { };
    public event Action<float> OnValueChanged = delegate { };
    
    public IStat MaxValue { get; private set; }
    public float MinValue { get; private set; }
    public float Value { get; private set; }
    
    /// <summary>
    /// Returns the ratio of the current value to the max value
    /// </summary>
    public float Ratio => GetRatio();
    
    private float _lastRatio;

    public Vital(
        IStat maxValue,
        float minValue,
        float value,
        bool useRatio = false,
        float ratio = 0)
    {
        MaxValue = maxValue;
        MinValue = minValue;

        MaxValue.OnValueChanged += OnMaxValueChanged;

        if (useRatio)
        {
            if (ratio < 0) throw new ArgumentOutOfRangeException(nameof(ratio), "Ratio must be greater than 0");
            Value = maxValue.Value * ratio;
            UpdateLastRatio();
            return;
        }
        Value = value;

        UpdateLastRatio();
    }

    public void SetMaxValue(IStat stat)
    {
        MaxValue.OnValueChanged -= OnMaxValueChanged;
        
        var currentRatio = GetRatio();
        MaxValue = stat;
        SetValueByRatio(currentRatio);
        UpdateLastRatio();
        
        MaxValue.OnValueChanged += OnMaxValueChanged;
    }

    public void SetMinValue(float value)
    {
        if (value > MaxValue.Value)
            throw new ArgumentOutOfRangeException(nameof(value), "MinValue must be less than MaxValue");
        MinValue = value;
        SetValueByRatio(_lastRatio);
        UpdateLastRatio();
    }

    private void OnMaxValueChanged(float value)
    {
        SetValueByRatio(_lastRatio);
        UpdateLastRatio();
    }

    public void SetValueByRatio(float ratio)
    {
        if (ratio < 0) throw new ArgumentOutOfRangeException(nameof(ratio), "Ratio must be greater than 0");
        // SetValue(MaxValue.Value * ratio);
        SetValue(MinValue + (MaxValue.Value - MinValue) * ratio);
    }

    public void SetValue(float value)
    {
        Value = Math.Clamp(value, MinValue, MaxValue.Value);
        OnValueChanged.Invoke(Value);
        UpdateLastRatio();
        if (Value.Equals(MaxValue.Value)) OnValueToMax.Invoke();
        if (Value.Equals(MinValue)) OnValueToMin.Invoke();
    }

    public void Increase(float value) => SetValue(Value + value);
    public void Decrease(float value) => SetValue(Value - value);
    public void ResetToMax() => SetValue(MaxValue.Value);
    public void ResetToMin() => SetValue(MinValue);

    public float GetRatio()
    {
        if (MaxValue.Value == 0) return 1f;
        var result = (Value - MinValue) / (MaxValue.Value - MinValue);
        return (float)Math.Round(result, 2);
    }
    
    private float UpdateLastRatio() => _lastRatio = GetRatio();
}