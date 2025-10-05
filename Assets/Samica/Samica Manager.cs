using UnityEngine;

public class SamicaManager : MonoBehaviour
{
    [SerializeField] Statistic social;

    private bool _isSamicad = false;
    public bool IsSamicad
    {
        get => _isSamicad;
        set
        {
            _isSamicad = true;
            FindAnyObjectByType<StatsManager>().AddStatCount(social, 10);
        }
    }

    
}
