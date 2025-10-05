using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    public static HospitalManager Instance;

[Header("Hospital Settings")]
    public int HealCost = 1000;
    public int HealAmount = 20;
    public int MaxHealth = 100;

    private StatsManager statsManager;
    [SerializeField] private Statistic money;
    [SerializeField] private Statistic health;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        statsManager = FindAnyObjectByType<StatsManager>();
    }

    public bool CanHeal()
    {
        int currentMoney = statsManager.GetStatisticCount(money);
        int currentHealth = statsManager.GetStatisticCount(health);

        return currentMoney >= HealCost && currentHealth < MaxHealth;
    }

    public void Heal()
    {
        if (!CanHeal()) return;

        int currentMoney = statsManager.GetStatisticCount(money);
        int currentHealth = statsManager.GetStatisticCount(health);

        // zabieramy pieni¹dze
        statsManager.AddStatCount(money, -HealCost);

        // dodajemy zdrowie
        int newHealth = currentHealth + HealAmount;
        if (newHealth > MaxHealth) newHealth = MaxHealth;

        statsManager.SetStatCount(health, newHealth);
    }

    public int GetCurrentHealth() => statsManager.GetStatisticCount(health);
    public int GetCurrentMoney() => statsManager.GetStatisticCount(money);


}
