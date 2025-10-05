using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour
{
    private Dictionary<Statistic, int> stats;
    
    [SerializeField] StatisticCount[] startingStatistics;
    void Start()
    {
        stats = new(startingStatistics.Length);

        foreach (var pair in startingStatistics)
        {
            stats[pair.statistic] = pair.count;
        }
    }

    public List<StatisticCount> GetStatistics()
    {
        var result = new List<StatisticCount>();

        foreach (var pair in stats)
        {
            StatisticCount statCount = new()
            {
                statistic = pair.Key,
                count = pair.Value
            };

            result.Add(statCount);
        }

        return result;
    }

    public int GetStatisticCount(Statistic statistic)
    {
        if (stats.ContainsKey(statistic)) return stats[statistic];
        return 0;
    }

    public void SetStatCount(Statistic statistic, int count)
    {
        stats[statistic] = count;
    }

    public void AddStatCount(Statistic statistic, int count)
    {
        if (stats.ContainsKey(statistic))
        {
            var curCount = stats[statistic];
            SetStatCount(statistic, curCount + count);
        }
        else stats[statistic] = count;
    }
}
