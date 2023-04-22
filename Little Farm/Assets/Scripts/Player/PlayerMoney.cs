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
        if (_moneyText.Length > 0)
        {
            for (int i = 0; i < _moneyText.Length; i++)
            {
                _moneyText[i].text = "0";
            }
        }
    }

    private void Update()
    {
        if (!DeviceUIController.UIGet)
            _indexUI = DeviceUIController.IndexUI;
    }

    private void IncreaseMoney(int value)
    {
        Money += value;
        Debug.Log($"Money: {Money}");
        UpdateUI();
    }

    public void DecreaseMoney(int value)
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
        if (_moneyText.Length > 0)
            _moneyText[_indexUI].text = Money.ToString();
    }
}
