using System.Collections.Generic;

public class Item
{
    private float[] _damageMod;
    private float[] _attackSpeedMod;
    private float[] _rangeMod;
    private float[] _healthMod;
    private float[] _energyMod;
    private float[] _energyPowerMod;
    private float[] _movementSpeed;

    public Dictionary<string, float[]> statsOut { get; }

    public Item(float damagePlus = 0, float damagePercent = 0, 
        float attackSpeedPlus = 0, float attackSpeedPercent = 0,
        float attackRangePlus = 0, float attackRangePercent = 0,
        float healthPlus = 0, float healthPercent = 0,
        float energyPlus = 0, float energyPercent = 0,
        float energyPowerPlus = 0, float energyPowerPercent = 0,
        float movementSpeedPlus = 0, float movementSpeedPercent = 0)
    {
        _damageMod = new float[] { damagePlus, damagePercent };
        _attackSpeedMod = new float[] { attackSpeedPlus, attackSpeedPercent };
        _rangeMod = new float[] { attackRangePlus, attackRangePercent };
        _healthMod = new float[] { healthPlus, healthPercent };
        _energyMod = new float[] { energyPlus, energyPercent };
        _energyPowerMod = new float[] { energyPowerPlus, energyPowerPercent };
        _movementSpeed = new float[] { movementSpeedPlus, movementSpeedPercent };
        statsOut = new Dictionary<string, float[]>() {
            {"health", _healthMod},
            {"energy", _energyMod},
            {"damage", _damageMod},
            {"range", _rangeMod},
            {"attackSpeed", _attackSpeedMod},
            {"energyPower", _energyPowerMod},
            {"movementSpeed", _movementSpeed}
        };
    }
    public void Equip(Character character)
    {
        var characterStats = character.statsOut;
        foreach (var stat in statsOut)
        {
            characterStats[stat.Key].AddModifier(new StatModifier(stat.Value[0], stat.Value[1], this));
        }
    }

    public void Unequip(Character character)
    {
        var characterStats = character.statsOut;
        foreach (var stat in statsOut)
        {
            characterStats[stat.Key].RemoveAllModifiersFromSource(this);
        }
    }
}
