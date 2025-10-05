using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HospitalUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text HealthText;
    public TMP_Text HealCostText;
    public Button HealButton;


[Header("Stats")]
    [SerializeField] private Statistic money;
    [SerializeField] private Statistic health;

    private StatsManager statsManager;
    private int healCost = 1000;
    private int healAmount = 20;
    private int maxHealth = 100;

    void Start()
    {
        statsManager = FindAnyObjectByType<StatsManager>();

        // ustawiamy tekst na przycisku
        HealCostText.text = $"Heal (+{healAmount} HP) - Cost: ${healCost}";
        HealButton.onClick.AddListener(Heal);

        UpdateUI();
    }

    public void Heal()
    {
        int currentMoney = statsManager.GetStatisticCount(money);
        int currentHealth = statsManager.GetStatisticCount(health);

        if (currentMoney >= healCost && currentHealth < maxHealth)
        {
            // pobieramy pieni¹dze
            statsManager.AddStatCount(money, -healCost);

            // dodajemy zdrowie
            int newHealth = currentHealth + healAmount;
            if (newHealth > maxHealth) newHealth = maxHealth;

            statsManager.SetStatCount(health, newHealth); // poprawiona metoda
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        int currentHealth = statsManager.GetStatisticCount(health);
        int currentMoney = statsManager.GetStatisticCount(money);

        HealthText.text = $"Health: {currentHealth}/{maxHealth}";
        HealCostText.text = $"Heal (+{healAmount} HP) - Cost: ${healCost}";

        // dezaktywuj przycisk jeœli nie staæ lub zdrowie pe³ne
        HealButton.interactable = currentMoney >= healCost && currentHealth < maxHealth;
    }

}
