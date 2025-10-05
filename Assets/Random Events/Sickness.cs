using UnityEngine;

[CreateAssetMenu(menuName = "Random Events/Sickness")]
public class Sickness : RandomEvent
{
    [SerializeField] Statistic health;

    public override void Trigger()
    {
        var statsManager = FindAnyObjectByType<StatsManager>();
        // nigger nigger nigger
        var damage = Random.Range(10, 15);

        if (statsManager.GetStatisticCount(health) < damage) statsManager.SetStatCount(health, 0);
        else statsManager.AddStatCount(health, -damage);
    }
}
