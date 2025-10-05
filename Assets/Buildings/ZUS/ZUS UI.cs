using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZUSUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text pensionText;
    [SerializeField] private TMP_Text sicknessText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text disabilityText;
    
    private ZUSManager zusManager;
    void Awake()
    {
        zusManager = FindAnyObjectByType<ZUSManager>();
        RefreshTexts();
        gameObject.SetActive(true);
    }

    public void Open()
    {
        RefreshTexts();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void RefreshTexts()
    {
        pensionText.text = $"Emerytalna: {zusManager.pension} zł";
        sicknessText.text    = $"Chorobowa: {zusManager.sickness} zł";
        healthText.text = $"Zdrowotna: {zusManager.health} zł";
        disabilityText.text    = $"Rentowa: {zusManager.disability} zł";
    }
    
    

}
