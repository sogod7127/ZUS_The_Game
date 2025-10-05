using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] LayoutGroup box;
    [SerializeField] StatsUISlot slot;

    public void UpdateVisuals()
    {
        for (int i = box.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(box.transform.GetChild(i).gameObject);
        }

        var statsMngr = FindAnyObjectByType<StatsManager>();
        foreach (var stat in statsMngr.GetStatistics())
        {
            var obj = Instantiate(slot, box.transform);

            obj.UpdateVisual(stat.statistic, stat.count);
        }
    }
}
