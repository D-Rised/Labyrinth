using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : IStats
{
    private List<Stat> _stats;

    public IEnumerable<Stat> GetStats()
    {
        return _stats;
    }
}
