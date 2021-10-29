using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    Speed,
    Ghost
}

[Serializable]
public class Stat
{
    public StatType Type;
    public int Value;
    public int MaxValue;
    
    public Stat(StatType type, int value, int max)
    {
        Type = type;
        Value = value;
        MaxValue = max;
    }

    public int GetValue()
    {
        return Value;
    }

    public int GetMaxValue()
    {
        return MaxValue;
    }
}
