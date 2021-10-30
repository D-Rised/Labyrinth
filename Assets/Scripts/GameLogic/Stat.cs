using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    Speed
}

[Serializable]
public class Stat
{
    private StatType Type;
    private int Value;
    private int MaxValue;
    
    public Stat(StatType type, int value, int max)
    {
        Type = type;
        Value = value;
        MaxValue = max;
    }

    public StatType GetStatType()
    {
        return Type;
    }

    public int GetValue()
    {
        return Value;
    }

    public int GetMaxValue()
    {
        return MaxValue;
    }

    public void SetValue(int newValue)
    {
        Value = newValue;
    }

    public void SetMaxValue(int newMaxValue)
    {
        MaxValue = newMaxValue;
    }
}
