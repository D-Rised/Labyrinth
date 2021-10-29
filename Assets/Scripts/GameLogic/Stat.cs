using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat : MonoBehaviour
{
    public int Value;
    public int MaxValue;

    private bool isCalculated = false;

    private readonly List<StatModifier> statModifiers;
        
    public Stat(int value, int max)
    {
        Value = value;
        MaxValue = max;
        statModifiers = new List<StatModifier>();
    }

    public void ModifierAddCurrentValue(StatModifier modifier)
    {
        Value = Mathf.Clamp(Value + modifier.Value, 0, MaxValue);
        if (modifier.Type == StatModifierType.Temporal)
        {
            statModifiers.Add(modifier);
            StartCoroutine(ValueModifierTimer(modifier.Seconds, modifier));
        }
    }

    public void ModifierAddMaxValue(StatModifier modifier)
    {
        MaxValue += modifier.Value;
        if (modifier.Type == StatModifierType.Temporal)
        {
            MaxValue += modifier.Value;
            statModifiers.Add(modifier);
            StartCoroutine(ValueModifierTimer(modifier.Seconds, modifier));
        }
    }

    public void ModifierAddConstantValue(StatModifier modifier)
    {
        Value = Mathf.Clamp(Value + modifier.Value, 0, MaxValue);
        MaxValue += modifier.Value;
        if (modifier.Type == StatModifierType.Temporal)
        {
            statModifiers.Add(modifier);
            StartCoroutine(ValueModifierTimer(modifier.Seconds, modifier));
            StartCoroutine(MaxValueModifierTimer(modifier.Seconds, modifier));
        }
    }

    private IEnumerator ValueModifierTimer(float seconds, StatModifier modifier)
    {
        yield return new WaitForSeconds(seconds);
        Value = Mathf.Clamp(Value - modifier.Value, 1, MaxValue);
        statModifiers.Remove(modifier);
    }

    private IEnumerator MaxValueModifierTimer(float seconds, StatModifier modifier)
    {
        yield return new WaitForSeconds(seconds);
        MaxValue -= modifier.Value;
        statModifiers.Remove(modifier);
    }
}
