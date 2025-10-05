using UnityEngine;

[CreateAssetMenu(fileName = "Close Person Death", menuName = "Random Events/ClosePersonDeath")]
public class ClosePersonDeath : RandomEvent
{
    [SerializeField] Statistic health;
    [SerializeField] Statistic satisfaction;
    [SerializeField] Statistic socials;

    public override void Trigger()
    {
        var statsManager = FindAnyObjectByType<StatsManager>();
        // nigger nigger nigger
        var satisfactionDamage = Random.Range(40, 70);

        if (statsManager.GetStatisticCount(satisfaction) < satisfactionDamage) statsManager.SetStatCount(satisfaction, 0);
        else statsManager.AddStatCount(satisfaction, -satisfactionDamage);

        var socialDamage = satisfactionDamage / 3 - 10;
        if (statsManager.GetStatisticCount(socials) < socialDamage) statsManager.SetStatCount(socials, 0);
        else statsManager.AddStatCount(socials, -socialDamage);

        if (satisfactionDamage >= 60)
        {
            var damage = Random.Range(5, 20);
            if (statsManager.GetStatisticCount(health) < damage) statsManager.SetStatCount(health, 0);
            else statsManager.AddStatCount(health, -damage);
        }
    }
}
