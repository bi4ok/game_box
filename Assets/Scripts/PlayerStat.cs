using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using UnityEngine;

public class PlayerStat
{
    private float baseValue;
    public readonly string name;

    public readonly ReadOnlyCollection<StatModifier> statModifiers;
    private readonly List<StatModifier> _statModifiers;

    private bool _isDirty = true;
    private float _value;
    

    public PlayerStat(float startValue, string statName)
    {
        baseValue = startValue;
        _statModifiers = new List<StatModifier>();
        name = statName;
        statModifiers = _statModifiers.AsReadOnly();
    }

    public float Value
    {
        get
        {
            if (_isDirty)
            {
                _value = CalculateFinalValue();
                _isDirty = false;
            }
            return _value;
        }
    }

    public void AddModifier(StatModifier mod)
    {
        _isDirty = true;
        _statModifiers.Add(mod);
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if (_statModifiers.Remove(mod))
        {
            _isDirty = true;
            return true;
        }
        return false;
    }

    private float CalculateFinalValue()
    {
        List<float> finalStat = new List<float>() { baseValue, 0 };
        for (int i = 0; i < _statModifiers.Count; i++)
        {
            StatModifier mod = _statModifiers[i];
            finalStat = mod.AddEffect(finalStat[0], finalStat[1]);
        }
        float finalValue = finalStat[0] * (1 + finalStat[1]/100);

        return (float)Math.Round(finalValue, 4);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = _statModifiers.Count - 1; i >= 0; i--)
        {
            if (_statModifiers[i].source == source)
            {
                _isDirty = true;
                didRemove = true;
                _statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    public List<StatModifier> giveAllMods()
    {
        return _statModifiers;
    }
}
