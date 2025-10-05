using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUISlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI countText;

    public void UpdateVisual(Statistic statistic, int count)
    {
        icon.sprite = statistic.icon;
        countText.text = count.ToString();
    }
}
