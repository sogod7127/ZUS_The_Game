using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZUSManager : MonoBehaviour
{
    
    public static ZUSManager Instance;
    [SerializeField] public float pension;       // Emerytalna 9,76%
    [SerializeField] public float disability;    // Rentowa 1,5%
    [SerializeField] public float sickness;      // Chorobowa 2,45%
    [SerializeField] public float healthBase;    // Podstawa = brutto - (społeczne pracownika)
    [SerializeField] public float health;        // Zdrowotna 9% od healthBase

    
    void Awake()
    {
         
        gameObject.SetActive(true);

    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);

    }


}
