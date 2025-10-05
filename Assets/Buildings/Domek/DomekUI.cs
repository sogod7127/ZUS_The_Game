using UnityEngine;
using TMPro;

public class DomekUI : MonoBehaviour
{
    [SerializeField] private TMP_Text LifeText;
    [SerializeField] private TMP_Text RelationText;


[SerializeField] private Statistic life;     // stat �ycia
    [SerializeField] private Statistic relation; // stat relacji
    private StatsManager statsManager;

    void Start()
    {
        statsManager = FindAnyObjectByType<StatsManager>();

        if (statsManager == null)
        {
            Debug.LogError("Brak StatsManager w scenie!");
            return;
        }

        UpdateUI();
    }

    public void RestButton()
    {
        if (statsManager == null) return;

        // +5 �ycia
        statsManager.AddStatCount(life, 5);

        // -5 relacji
        statsManager.AddStatCount(relation, -5);

        Debug.Log($"Klik Rest: Life={statsManager.GetStatisticCount(life)}, Relation={statsManager.GetStatisticCount(relation)}");

        UpdateUI();
    }

    void UpdateUI()
    {
        if (statsManager == null) return;

        LifeText.text = $"Life: {statsManager.GetStatisticCount(life)}";
        RelationText.text = $"Relations: {statsManager.GetStatisticCount(relation)}";
    }

}
