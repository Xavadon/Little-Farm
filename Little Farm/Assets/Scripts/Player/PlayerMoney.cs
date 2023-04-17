using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _moneyText = new TextMeshProUGUI[2];

    private int _indexUI;

    public static int Money { get; private set; }

    private void OnEnable()
    {
        Shop.OnSell += IncreaseMoney;
    }

    private void OnDisable()
    {
        Shop.OnSell -= IncreaseMoney;
    }

    private void Start()
    {
        switch (YandexSDK.YaSDK.instance.currentPlatform)
        {
            case YandexSDK.Platform.desktop:
                _indexUI = 0;
                break;
            case YandexSDK.Platform.phone:
                _indexUI = 1;
                break;
            default:
                _indexUI = 1;
                break;
        }

        if (_moneyText.Length > 0) ;
            //_moneyText[_indexUI].text = "0";
    }

    private void IncreaseMoney(int value)
    {
        Money += value;
        Debug.Log($"Money: {Money}");
        UpdateUI();
    }

    private void DecreaseMoney(int value)
    {
        if ((Money - value) >= 0)
        {
            Money -= value;
            Debug.Log($"Money: {Money}");
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_moneyText.Length > 0) ;
            //_moneyText[_indexUI].text = Money.ToString();
    }
}
