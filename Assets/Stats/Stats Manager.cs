using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    private Dictionary<Statistic, int> stats;

    [SerializeField] StatisticCount[] startingStatistics;

    [SerializeField] StatsUI ui;

    [SerializeField] Statistic health;
    //[SerializeField] StatsSaver saver;

    void Start()
    {
        stats = new(startingStatistics.Length);

        foreach (var pair in startingStatistics)
        {
            SetStatCount(pair.statistic, pair.count);
        }

        //DontDestroyOnLoad(Instantiate(saver));
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

        if (statistic == health && count <= 0) Die();

        ui.UpdateVisuals();
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

    public void Die()
    {
        SceneManager.LoadScene("kill yourself now");
    }
}

[System.Serializable]
public class StatisticCount
{
    public Statistic statistic;
    public int count;
}