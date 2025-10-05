using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float yearDuration = 6f;
    public float YearDuration { get => yearDuration; }

    [SerializeField] private float startYear = 18f;
    private float _year = 18f;
    public float Year { get => _year; }

    public bool TimeStopped { get => false; } // UPDATE TO CHECK IF PAUSED

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI yearText; // pod��cz w inspectorze
    
    public EmployeeZUSContribution zusScript;

    void Start()
    {
        _year = startYear;
        UpdateYearUI();
    }

    void Update()
    {
        if (TimeStopped) return;

        _year += Time.deltaTime / yearDuration;
        zusScript.ComputeAndSave(Mathf.FloorToInt(_year));
        UpdateYearUI();
    }

    private void UpdateYearUI()
    {
        if (yearText != null)
            yearText.text = $"Rok: {Mathf.FloorToInt(_year)}"; // zaokr�glamy do liczby ca�kowitej
    }
}
