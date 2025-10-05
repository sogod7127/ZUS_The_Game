using System.Collections;
using UnityEngine;

public class UniversityManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Statistic money;
    public Statistic education;

    [Header("Managers")]
    [SerializeField] StatsManager statsManager;
    [SerializeField] TimeManager timeManager;
    public StatsManager StatsManager { get => statsManager; }

    [Header("Degrees Data")]
    [SerializeField] int[] yearsToGraduate;
    public int[] priceForDegree;

    public int timeToGraduate { get; private set; }

    public bool IsStudying { get; protected set; } = false;

    private Coroutine graduationTimer;

    public bool Apply()
    {
        var educationLevel = statsManager.GetStatisticCount(education);
        var desiredLevel = educationLevel + 1;
        if (educationLevel >= 3) return false;
        if (statsManager.GetStatisticCount(money) < priceForDegree[desiredLevel]) return false;

        statsManager.AddStatCount(money, -priceForDegree[desiredLevel]);
        IsStudying = true;

        graduationTimer = StartCoroutine(StartStudying(desiredLevel));

        return true;
    }

    public void DropOut()
    {
        IsStudying = false;
        if (graduationTimer != null) StopCoroutine(graduationTimer);
        graduationTimer = null;
    }

    IEnumerator StartStudying(int desiredLevel)
    {
        timeToGraduate = yearsToGraduate[desiredLevel];

        for (int i = 0; i < yearsToGraduate[desiredLevel]; i++)
        {
            yield return new WaitForSeconds(timeManager.YearDuration);
            timeToGraduate--;
        }

        var mngr = FindAnyObjectByType<StatsManager>();
        mngr.AddStatCount(education, 1);

        FindAnyObjectByType<UniversityUI>()?.DropOut();
    }
}




