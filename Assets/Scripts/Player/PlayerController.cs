using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Stats playerStats;

    public event Action<int> OnHealthChange;

    public Stat Health;
    public Stat Speed;
    public Stat Damage;

    void Start()
    {
        Health = new Stat(100, 100);
    }
    
    void Update()
    {

    }

    public int GetHealth()
    {
        return Health.Value;
    }

    public void ApplyDamage(int damage)
    {
        if (Health != null)
        {
            Health.Value -= damage;
        }

        if (OnHealthChange != null)
        {
            OnHealthChange.Invoke(damage);
        }
    }
}
