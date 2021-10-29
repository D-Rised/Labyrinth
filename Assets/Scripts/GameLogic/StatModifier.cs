using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
    Permanent,
    Temporal
}

public class StatModifier : MonoBehaviour
{
    public readonly int Value;
    public readonly StatModifierType Type;
    public readonly int Seconds;

    public StatModifier(int value, StatModifierType type, int seconds)
    {
        Value = value;
        Type = type;
        Seconds = seconds;
    }

    public StatModifier(int value, StatModifierType type) : this(value, type, 0) { }
}
