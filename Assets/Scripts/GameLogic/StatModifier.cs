using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierTimeType
{
    Permanent,
    Temporal
}

public enum StatModifierValueType
{
    Current,
    Max,
    Both
}

public class StatModifier
{
    public readonly int Value;
    public readonly StatModifierTimeType TimeType;
    public readonly StatModifierValueType ValueType;
    public readonly int Seconds;

    public StatModifier(int value, StatModifierTimeType timeType, StatModifierValueType valueType, int seconds)
    {
        Value = value;
        TimeType = timeType;
        ValueType = valueType;
        Seconds = seconds;
    }

    public StatModifier(int value, StatModifierTimeType timeType, StatModifierValueType valueType) : this(value, timeType, valueType, 0) { }
}
