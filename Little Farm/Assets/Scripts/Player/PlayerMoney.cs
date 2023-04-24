using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _moneyText = new TextMeshProUGUI[2];

    public static int Money { get; private set; }

    private void OnEnable()
    {
        Shop.OnSell += IncreaseMoney;
        YG.YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        Shop.OnSell -= IncreaseMoney;
        YG.YandexGame.GetDataEvent -= GetLoad;
    }

    private void GetLoad()
    {
        int money = YG.YandexGame.savesData.money;
        Debug.Log(money);

        if (money >= 0)
        {
            Money = money;
            UpdateUI();
        }
    }

    private void SaveProgress()
    {
        YG.YandexGame.savesData.money = Money;
        //YG.YandexGame.SaveProgress();
    }

    private void IncreaseMoney(int value)
    {
        Money += value;
        Debug.Log($"Money: {Money}");
        UpdateUI();
        SaveProgress();
    }

    public void DecreaseMoney(int value)
    {
        if ((Money - value) >= 0)
        {
            Money -= value;
            Debug.Log($"Money: {Money}");
            UpdateUI();
            SaveProgress();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _moneyText.Length; i++)
        {
            _moneyText[i].text = Money.ToString();
        }
    }
}
