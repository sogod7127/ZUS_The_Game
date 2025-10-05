using UnityEngine;
using TMPro;

public class WorkUI : MonoBehaviour
{
    public TMP_Text JobNameText;
    public TMP_Text SalaryText;
    public TMP_Text RequirementsText;
    public TMP_Text ExperienceText;
    public TMP_Text HealthText;


private WorkManager manager;

    [SerializeField] private Statistic money;
    [SerializeField] private Statistic education;
    [SerializeField] private Statistic health;

    private StatsManager statsManager;

    private int maxHealth = 100;
    private int workHealthCost = 10; // ile zdrowia zabiera praca

    void Start()
    {
        manager = WorkManager.Instance;
        statsManager = FindAnyObjectByType<StatsManager>();

        UpdateUI();
    }

    public void WorkButton()
    {
        int currentHealth = statsManager.GetStatisticCount(health);

        if (currentHealth <= 0)
            return; // nie może pracować bez zdrowia

        // dodaj miesiąc doświadczenia
        manager.CurrentJob.WorkMonth();
        manager.CurrentMonth++;

        // zabierz zdrowie
        statsManager.AddStatCount(health, -workHealthCost);
        if (statsManager.GetStatisticCount(health) < 0)
            statsManager.SetStatCount(health, 0);

        // dodaj pieniądze
        statsManager.AddStatCount(money, manager.CurrentJob.Salary);

        UpdateUI();
    }

    public void PromoteButton()
    {
        WorkManager.Job nextJob = null;
        int highestSalary = manager.CurrentJob.Salary;

        // pobieramy aktualny poziom wykształcenia z StatsManager
        int playerEducationLevel = statsManager.GetStatisticCount(education);

        foreach (var job in manager.Jobs.Values)
        {
            if (job.Salary > highestSalary && job.CanAdvance(playerEducationLevel, manager.CurrentJob.GainedExperience))
            {
                highestSalary = job.Salary;
                nextJob = job;
            }
        }

        if (nextJob != null)
        {
            nextJob.GainedExperience = manager.CurrentJob.GainedExperience;
            manager.CurrentJob = nextJob;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        JobNameText.text = $"Job: {manager.CurrentJob.Name}";
        SalaryText.text = $"Salary: ${manager.CurrentJob.Salary}";
        RequirementsText.text = $"Requirements: Education Level {manager.CurrentJob.RequiredEducationLevel}, {manager.CurrentJob.RequiredExperience} months experience";
        ExperienceText.text = $"Experience: {manager.CurrentJob.GainedExperience} months ({manager.CurrentMonth / 12} years)";

        int currentHealth = statsManager.GetStatisticCount(health);
        HealthText.text = $"Health: {currentHealth}/{maxHealth}";
    }


}
