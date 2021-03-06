using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public event Action<float> EffectUpdater;

    private GameManager gameManager;
    private PlayerController player;
    private List<StatModifier> temporalEffectList;

    void Start()
    {
        gameManager = GameManager.instance;
        player = GetComponent<PlayerController>();
        temporalEffectList = new List<StatModifier>();
    }
    
    void Update()
    {

    }

    public void AddEffect(StatModifier effect, Stat stat)
    {
        if (effect.TimeType == StatModifierTimeType.Temporal)
        {
            temporalEffectList.Add(effect);

            if (effect.ValueType == StatModifierValueType.Current)
            {
                stat.SetValue(Mathf.Clamp(stat.GetValue() + effect.Value, 0, stat.GetMaxValue()));
            }
            else if (effect.ValueType == StatModifierValueType.Max)
            {
                stat.SetMaxValue(stat.GetMaxValue() + effect.Value);
            }
            else if (effect.ValueType == StatModifierValueType.Both)
            {
                stat.SetMaxValue(stat.GetMaxValue() + effect.Value);
                stat.SetValue(Mathf.Clamp(stat.GetValue() + effect.Value, 0, stat.GetMaxValue()));
            }

            StartCoroutine(EffectDuration(effect, stat));
            StartCoroutine(EffectStatusUpdater(effect, stat));
        }
        else if (effect.TimeType == StatModifierTimeType.Permanent)
        {
            if (effect.ValueType == StatModifierValueType.Current)
            {
                stat.SetValue(Mathf.Clamp(stat.GetValue() + effect.Value, 0, stat.GetMaxValue()));
            }
            else if (effect.ValueType == StatModifierValueType.Max)
            {
                stat.SetMaxValue(stat.GetMaxValue() + effect.Value);
            }
            else if (effect.ValueType == StatModifierValueType.Both)
            {
                stat.SetMaxValue(stat.GetMaxValue() + effect.Value);
                stat.SetValue(Mathf.Clamp(stat.GetValue() + effect.Value, 0, stat.GetMaxValue()));
            }
        }
    }

    public void RemoveEffect(StatModifier effect)
    {
        temporalEffectList.Remove(effect);
    }

    private IEnumerator EffectDuration(StatModifier effect, Stat stat)
    {
        yield return new WaitForSeconds(effect.Seconds);

        if (effect.ValueType == StatModifierValueType.Current)
        {
            stat.SetValue(Mathf.Clamp(stat.GetValue() - effect.Value, 0, stat.GetMaxValue()));
        }
        else if (effect.ValueType == StatModifierValueType.Max)
        {
            stat.SetMaxValue(stat.GetMaxValue() - effect.Value);
        }
        else if (effect.ValueType == StatModifierValueType.Both)
        {
            stat.SetMaxValue(stat.GetMaxValue() - effect.Value);
            stat.SetValue(Mathf.Clamp(stat.GetValue() - effect.Value, 0, stat.GetMaxValue()));
        }

        RemoveEffect(effect);
    }

    private IEnumerator EffectStatusUpdater(StatModifier effect, Stat stat)
    {
        DateTime effectEndTime = DateTime.Now.AddSeconds(effect.Seconds);

        EffectUpdater.Invoke(1);

        for (int i = 0; i < effect.Seconds; i++)
        {
            yield return new WaitForSeconds(1);
            TimeSpan effectDuration = effectEndTime - DateTime.Now;
            while (gameManager.GetGameState() == GameState.Stop)
            {
                yield return new WaitForSeconds(0.2f);
                effectEndTime = DateTime.Now.AddSeconds(effectDuration.TotalSeconds);
            }
            float value = (float)effectDuration.TotalSeconds / (float)(effect.Seconds);
            EffectUpdater.Invoke(value);
        }
    }
}
