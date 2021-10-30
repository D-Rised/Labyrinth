using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    public GameObject HealthBar;
    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        player.OnHealthChange += UpdateValue;
    }

    private void OnDisable()
    {
        player.OnHealthChange -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        float healthValue = (float)value / (float)player.Health.GetMaxValue();
        HealthBar.transform.localScale = new Vector3(healthValue, 1, 1);
    }
}
