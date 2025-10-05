using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [SerializeField] Statistic social;
    [SerializeField] Statistic money;
    [SerializeField] Statistic married; // nowa statystyka
    [SerializeField] Button button;
    [SerializeField, Range(0, 1)] float girlfriendChance = 0.05f;

    public void SpendNight()
    {
        var statsMngr = FindAnyObjectByType<StatsManager>();
        if (statsMngr == null) return;

        // --- p³acisz 400 kasy ---
        int currentMoney = statsMngr.GetStatisticCount(money);
        if (currentMoney < 400)
        {
            Debug.Log("Nie masz kasy na noc w barze!");
            return;
        }
        statsMngr.AddStatCount(money, -400);

        // po zap³aceniu blokujemy przycisk na jedn¹ noc
        button.interactable = false;

        var samicaMngr = FindAnyObjectByType<SamicaManager>();
        if (samicaMngr.IsSamicad)
        {
            // podbijamy social
            var socialBump = Random.Range(3, 15);
            int newSocial = Mathf.Min(statsMngr.GetStatisticCount(social) + socialBump, 100);
            statsMngr.SetStatCount(social, newSocial);
        }
        else
        {
            // szansa na zdobycie samicy
            if (Random.value <= girlfriendChance)
            {
                samicaMngr.IsSamicad = true;
                print("You got samica");
            }
        }

        // --- ustawiamy married stat ---
        if (samicaMngr.IsSamicad)
            statsMngr.SetStatCount(married, 1);
        else
            statsMngr.SetStatCount(married, 0);
    }
}
