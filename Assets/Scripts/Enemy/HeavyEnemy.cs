using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    public Stat Speed;

    void Awake()
    {
        List<Stat> stats = new List<Stat>();
        Health = new Stat(StatType.Health, 100, 100);
        Damage = new Stat(StatType.Damage, 50, 50);
        Speed = new Stat(StatType.Speed, 3, 3);

        stats.Add(Health);
        stats.Add(Damage);
        stats.Add(Speed);

        EnemyStats = new Stats(stats);
    }
}
