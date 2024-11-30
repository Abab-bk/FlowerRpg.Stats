# FlowerRpg.Stats

A stats system for games, and it's a part of FlowerRPG.

## Install

FlowerRpg.Stats is available as [Nutget]("[NuGet Gallery | FlowerRpg.Stats 1.0.0](https://www.nuget.org/packages/FlowerRpg.Stats/)") pack.

```
<PackageReference Include="FlowerRpg.Stats" Version="1.0.0" />
```

## Usage

Show your code:

```csharp
    public void HowToUse()
    {
        var stat = new Stat(
            baseValue: 100f
            );
        
        var flatModifier = new Modifier(
            type: ModifierType.Flat,
            value: 10f
            );
        
        var percentAddModifier = new Modifier(
            type: ModifierType.PercentAdd,
            value: 0.1f
            );
        
        var percentMultModifier = new Modifier(
            type: ModifierType.PercentMult,
            value: 0.1f
            );
        
        stat.AddModifier(flatModifier);
        
        var value = stat.Value; // value = 100 + 10 = 110
        
        // vital has max and min value
        var vital = new Vital(
            maxValue: new Stat(100f),
            minValue: 0f,
            value: 33f
            );
        
        // create with ratio
        var vital2 = new Vital(
            maxValue: new Stat(100f),
            minValue: 0f,
            value: 0f,
            useRatio: true,
            ratio: 0.5f
        );
        
        var vitalValue = vital.Value; // 30
        var vitalValue2 = vital2.Value; // 100 * 0.5 = 50
    }
```
