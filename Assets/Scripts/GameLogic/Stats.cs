using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : IStats
{
    private List<Stat> _stats;

    public Stats(List<Stat> stats)
    {
        _stats = stats;
    }

    public IEnumerable<Stat> GetStats()
    {
        return _stats;
    }

    public Stat GetStatsByType(StatType type)
    {
        return _stats.Find(x => x.GetStatType() == type);
    }
}
