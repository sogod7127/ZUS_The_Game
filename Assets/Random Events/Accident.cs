using UnityEngine;

[CreateAssetMenu(fileName = "Accident", menuName = "Random Events/Accident")]
public class Accident : RandomEvent
{
    [SerializeField] Statistic health;

    public override void Trigger()
    {
        var statsManager = FindAnyObjectByType<StatsManager>();
        // nigger nigger nigger
        var damage = Random.Range(10, 25);

        if (statsManager.GetStatisticCount(health) < damage) statsManager.SetStatCount(health, 0);
        else statsManager.AddStatCount(health, -damage);
    }
}
