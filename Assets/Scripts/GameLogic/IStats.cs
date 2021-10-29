using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    IEnumerable<Stat> GetStats();
}
