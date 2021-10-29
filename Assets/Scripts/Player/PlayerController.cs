using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Stats playerStats;

    public event Action<int> OnHealthChange;

    public Stats PlayerStats;

    public Stat Health;
    public Stat Speed;
    public Stat Damage;

    public Item item;

    public bool underEffect;

    public PlayerEffects playerEffects;

    void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();

        List<Stat> stats = new List<Stat>();
        Health = new Stat(StatType.Health, 100, 100);
        Damage = new Stat(StatType.Damage, 0, 0);
        Speed = new Stat(StatType.Speed, 100, 100);

        stats.Add(Health);
        stats.Add(Damage);
        stats.Add(Speed);
        
        PlayerStats = new Stats(stats);
    }
    
    void Update()
    {

    }

    public void ApplyDamage(int damage)
    {
        if (OnHealthChange != null)
        {
            Health.Value -= damage;
            OnHealthChange.Invoke(Health.Value);
        }
    }

    public void ApplyEffect(StatModifier effect, Stat stat)
    {
        playerEffects.AddEffect(effect, stat);
    }
}
