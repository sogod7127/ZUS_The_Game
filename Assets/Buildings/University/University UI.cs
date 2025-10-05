using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniversityUI : MonoBehaviour
{
    private UniversityManager universityManager;
    public TextMeshProUGUI remaingingText;
    [SerializeField] TextMeshProUGUI costText;

    void Start()
    {
        universityManager = FindAnyObjectByType<UniversityManager>();

        applyButton.interactable = !universityManager.IsStudying;
        dropOutButton.interactable = universityManager.IsStudying;
    }

    [SerializeField] Button applyButton;
    [SerializeField] Button dropOutButton;

    public void Apply()
    {
        if (!universityManager.Apply()) return;

        applyButton.interactable = false;
        dropOutButton.interactable = true;
        remaingingText.gameObject.SetActive(true);
        costText.gameObject.SetActive(false);
    }

    public void DropOut()
    {
        universityManager?.DropOut();

        applyButton.interactable = true;
        dropOutButton.interactable = false;
        remaingingText.gameObject.SetActive(false);
        costText.gameObject.SetActive(true);
    }

    void Update()
    {
        remaingingText.text = $"Time to graduate: {universityManager.timeToGraduate} years";
        costText.text = $"Cost: {universityManager.priceForDegree[universityManager.StatsManager.GetStatisticCount(universityManager.education) + 1]}";
    }
}
