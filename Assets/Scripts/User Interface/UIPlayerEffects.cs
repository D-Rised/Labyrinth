using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEffects : MonoBehaviour
{
    public GameObject EffectBar;
    private PlayerEffects playerEffects;

    private void Awake()
    {
        playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEffects>();
        EffectBar.transform.localScale = new Vector3(0, 1, 1);
    }

    private void OnEnable()
    {
        playerEffects.EffectUpdater += UpdateValue;
    }

    private void OnDisable()
    {
        playerEffects.EffectUpdater -= UpdateValue;
    }

    private void UpdateValue(float value)
    {
        EffectBar.transform.localScale = new Vector3(value, 1, 1);
    }
}
