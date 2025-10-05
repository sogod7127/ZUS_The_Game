using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BankUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text saldoPortfelText;
    [SerializeField] private TMP_Text saldoBankText;
    [SerializeField] private TMP_InputField kwotaInput;
    [SerializeField] private Button wplacButton;
    [SerializeField] private Button wyplacButton;
    [SerializeField] private Button zamknijButton;

    [Header("Odniesienie do globalnych statystyk")]
    [SerializeField] private Statistic money;  
    [SerializeField] private Statistic Bankmoney;  
    
    void Awake()
    {
        RefreshTexts();
        gameObject.SetActive(true);
        wplacButton.onClick.AddListener(OnDeposit);
        wyplacButton.onClick.AddListener(OnWithdraw);
        zamknijButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        RefreshTexts();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        kwotaInput.text = "";
    }

    private void OnDeposit()
    {
        int amount = ParseAmount();
        if (amount <= 0) { RefreshTexts(); return; }

        int wallet = GetWallet();
        int real = Mathf.Min(amount, wallet);
        if (real <= 0) { RefreshTexts(); return; }

        SetWallet(wallet - real);

        SetBankBalance(GetBankBalance() + real);

        RefreshTexts();
    }

    private void OnWithdraw()
    {
        int amount = ParseAmount();
        if (amount <= 0) { RefreshTexts(); return; }

        int real = Mathf.Min(amount, GetBankBalance());
        if (real <= 0) { RefreshTexts(); return; }

        SetBankBalance(GetBankBalance() - real);
        SetWallet(GetWallet() + real);

        RefreshTexts();
    }

    private int ParseAmount()
    {
        if (int.TryParse(kwotaInput.text, out int v))
            return Mathf.Max(0, v);
        return 0;
    }

    private void RefreshTexts()
    {
        saldoPortfelText.text = $"Portfel: {GetWallet()}";
        saldoBankText.text    = $"Bank: {GetBankBalance()}";
    }
    
    private int GetWallet()
    {
        var statsManager = FindAnyObjectByType<StatsManager>();
        if (statsManager == null || money == null) return 0;
        return statsManager.GetStatisticCount(money);
    }

    private void SetWallet(int value)
    {
        var statsManager = FindAnyObjectByType<StatsManager>();
        if (statsManager == null || money == null) return;
        statsManager.SetStatCount(money, value);
    }
    
    private int GetBankBalance()
    {
        var bankManager = FindAnyObjectByType<BankManager>();
        if (bankManager == null || Bankmoney == null) return 0;
        return bankManager.GetStatisticCount(Bankmoney);
        
    }
    
    private void SetBankBalance(int value)
    {
        var bankManager = FindAnyObjectByType<BankManager>();

        if (bankManager == null || Bankmoney == null) return;
        bankManager.SetStatCount(Bankmoney, value);
    }
}
