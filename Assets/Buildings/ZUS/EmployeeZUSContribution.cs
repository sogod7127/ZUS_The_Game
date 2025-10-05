using System;
using UnityEngine;
using UnityEngine.Events;


public enum ContributionPolicy
{
    FullEmployee,   // pełne składki pracownika (UoP standard)
    YouthSimple     // uproszczenie dla <26 (społeczne=0, sama zdrowotna)
}


[Serializable]
//public class ContributionsEvent : UnityEvent<EmployeeContributions> {}
public class EmployeeZUSContribution : MonoBehaviour
{

    [Header("Polityka naliczania")]
    [SerializeField] private ContributionPolicy policy = ContributionPolicy.FullEmployee;
    [Tooltip("Czy przeliczyć automatycznie w Start()?")]
    [SerializeField] private bool computeOnStart = true;

    [Header("Stawki (po stronie PRACOWNIKA)")]
    [Range(0f, 1f)] public float PENSION_PCT = 0.0976f; // 9,76%
    [Range(0f, 1f)] public float DISAB_PCT   = 0.0150f; // 1,5%
    [Range(0f, 1f)] public float SICK_PCT    = 0.0245f; // 2,45%
    [Range(0f, 1f)] public float HEALTH_PCT  = 0.0900f; // 9,0%

    [Header("Zdarzenia")]
    // public ContributionsEvent OnCalculated; // wywoływane po przeliczeniu

    private WorkManager workManager;
    private ZUSManager zusManager;
    private StatsManager characterStatsManager;
    private TimeManager timeManager;

    private int currentAge;
    private void Awake()
    {
        if (!workManager)         workManager         = FindAnyObjectByType<WorkManager>();
        if (!zusManager)          zusManager          = FindAnyObjectByType<ZUSManager>();
        if (!characterStatsManager) characterStatsManager = FindAnyObjectByType<StatsManager>();
    }
    void Start()
    {
        
    }
    
    [ContextMenu("Compute And Save Now")]
    public void ComputeAndSave(int age)
    {
        
        currentAge = age;
        
      //  if (!EnsureStatistic()) return;
      
      Calculate(
          workManager.CurrentJob.Salary,
          currentAge,
            policy);

        //OnCalculated?.Invoke(result);
#if UNITY_EDITOR
        // Umożliwia zapis w edytorze bez PlayMode (gdy wywołasz z ContextMenu)
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    


    // === Core liczenia (tylko strona PRACOWNIKA) ===
    private void Calculate(float grossInput, int ageInput, ContributionPolicy pol)
    {
        float gross = Mathf.Max(0f, grossInput);
        int age = Mathf.Clamp(ageInput, 0, 150);
        Debug.Log(age.ToString());
        if (age > 26)
        { 
        // Przeprasazsam jest 3 w nocy xd
        zusManager.pension    += Round2(gross * PENSION_PCT);
        zusManager.disability += Round2(gross * DISAB_PCT);
        zusManager.sickness   += Round2(gross * SICK_PCT);
        var healthBasecalc = gross - (Round2(gross * PENSION_PCT) + Round2(gross * DISAB_PCT) + Round2(gross * SICK_PCT));
        zusManager.healthBase += Mathf.Max(0f, healthBasecalc);
        zusManager.health     += Round2(healthBasecalc * HEALTH_PCT);

        }
        else
        {
            // Uproszczenie: brak społecznych dla <26, zostaje zdrowotna
            zusManager.pension = zusManager.disability = zusManager.sickness = 0f;
            zusManager.healthBase += gross;
            zusManager.health += Round2(gross * HEALTH_PCT);
        }

    }

    private static float Round2(float v) => (float)Math.Round(v, 2, MidpointRounding.AwayFromZero);
}