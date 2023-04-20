using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] UIToEnable = new GameObject[2];
    [SerializeField] private TextMeshProUGUI[] _wheatPriceText;
    [SerializeField] private TextMeshProUGUI[] _carrotPriceText;
    [SerializeField] private TextMeshProUGUI[] _radishPriceText;
    [SerializeField] private TextMeshProUGUI[] _potatoPriceText;

    [Space(height:10)]
    [SerializeField] private TextMeshProUGUI[] _wheatPriceTextAD;
    [SerializeField] private TextMeshProUGUI[] _carrotPriceTextAD;
    [SerializeField] private TextMeshProUGUI[] _radishPriceTextAD;
    [SerializeField] private TextMeshProUGUI[] _potatoPriceTextAD;

    [Space(height:10)]
    [SerializeField] private TextMeshProUGUI[] _allPrice;
    [SerializeField] private TextMeshProUGUI[] _allPriceAD;

    [Space(height:10)]
    [SerializeField] private int _wheatPrice = 4;
    [SerializeField] private int _carrotPrice = 3;
    [SerializeField] private int _radishPrice = 2;
    [SerializeField] private int _potatoPrice = 1;

    private PlayerPlantsInventory _playerPlantsInventory;
    private int _wheat;
    private int _carrot;
    private int _radish;
    private int _potato;
    private int _indexUI;

    public static event Action<int> OnSell;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerPlantsInventory playerPlantsInventory))
        {
            _playerPlantsInventory = playerPlantsInventory;
            UpdatePrices();
            UIToEnable[_indexUI].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerPlantsInventory playerPlantsInventory))
        {
            _playerPlantsInventory = null;
            UIToEnable[_indexUI].SetActive(false);
        }
    }

    private void Start()
    {
        SetUI();
    }

    private void UpdatePrices()
    {
        for (int i = 0; i < _wheatPriceText.Length; i++)
        {
            _wheatPriceText[i].text = $"{_playerPlantsInventory.WheatCount * _wheatPrice}";
            _wheatPriceTextAD[i].text = $"{_playerPlantsInventory.WheatCount * _wheatPrice * 2}";
        }

        for (int i = 0; i < _carrotPriceText.Length; i++)
        {
            _carrotPriceText[i].text = $"{_playerPlantsInventory.CarrotCount * _carrotPrice}";
            _carrotPriceTextAD[i].text = $"{_playerPlantsInventory.CarrotCount * _carrotPrice * 2}";
        }

        for (int i = 0; i < _radishPriceText.Length; i++)
        {
            _radishPriceText[i].text = $"{_playerPlantsInventory.RadishCount * _radishPrice}";
            _radishPriceTextAD[i].text = $"{_playerPlantsInventory.RadishCount * _radishPrice * 2}";
        }

        for (int i = 0; i < _potatoPriceText.Length; i++)
        {
            _potatoPriceText[i].text = $"{_playerPlantsInventory.PotatoCount * _potatoPrice}";
            _potatoPriceTextAD[i].text = $"{_playerPlantsInventory.PotatoCount * _potatoPrice * 2}";
        }

        for (int i = 0; i < _allPrice.Length; i++)
        {
            var price = _playerPlantsInventory.WheatCount * _wheatPrice
                + _playerPlantsInventory.CarrotCount * _carrotPrice
                + _playerPlantsInventory.RadishCount * _radishPrice
                + _playerPlantsInventory.PotatoCount * _potatoPrice;
            _allPrice[i].text = $"{price}";
            _allPriceAD[i].text = $"{price * 2}";
        }
    }

    private void SetUI()
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

        UIToEnable[_indexUI].SetActive(false);
    }

    public void TrySellWheat()
    {
        _wheat = _playerPlantsInventory.WheatCount;
        
        if (_wheat > 0)
        {
            OnSell?.Invoke(_wheat * _wheatPrice);
            _playerPlantsInventory.WheatCount = -_wheat;
            UpdatePrices();
        }
    }
    
    public void TrySellWheatAD()
    {
        _wheat = _playerPlantsInventory.WheatCount;
        
        if (_wheat > 0)
        {
            YandexSDK.YaSDK.instance.ShowRewarded("1");
            OnSell?.Invoke(_wheat * _wheatPrice * 2);
            _playerPlantsInventory.WheatCount = -_wheat;
            UpdatePrices();
        }
    }

    public void TrySellCarrot()
    {
        _carrot = _playerPlantsInventory.CarrotCount;
        
        if (_carrot > 0)
        {
            OnSell?.Invoke(_carrot * _carrotPrice);
            _playerPlantsInventory.CarrotCount = -_carrot;
            UpdatePrices();
        }
    }
    
    public void TrySellCarrotAD()
    {
        _carrot = _playerPlantsInventory.CarrotCount;
        
        if (_carrot > 0)
        {
            YandexSDK.YaSDK.instance.ShowRewarded("1");
            OnSell?.Invoke(_carrot * _carrotPrice * 2);
            _playerPlantsInventory.CarrotCount = -_carrot;
            UpdatePrices();
        }
    }

    public void TrySellRadish()
    {
        _radish = _playerPlantsInventory.RadishCount;

        if (_radish > 0)
        {
            OnSell?.Invoke(_radish * _radishPrice);
            _playerPlantsInventory.RadishCount = -_radish;
            UpdatePrices();
        }
    }
    
    public void TrySellRadishAD()
    {
        _radish = _playerPlantsInventory.RadishCount;

        if (_radish > 0)
        {
            YandexSDK.YaSDK.instance.ShowRewarded("1");
            OnSell?.Invoke(_radish * _radishPrice * 2);
            _playerPlantsInventory.RadishCount = -_radish;
            UpdatePrices();
        }
    }

    public void TrySellPotato()
    {
        _potato = _playerPlantsInventory.PotatoCount;

        if (_potato > 0)
        {
            OnSell?.Invoke(_potato * _potatoPrice);
            _playerPlantsInventory.PotatoCount = -_potato;
            UpdatePrices();
        }
    }
    
    public void TrySellPotatoAD()
    {
        _potato = _playerPlantsInventory.PotatoCount;

        if (_potato > 0)
        {
            YandexSDK.YaSDK.instance.ShowRewarded("1");
            OnSell?.Invoke(_potato * _potatoPrice * 2);
            _playerPlantsInventory.PotatoCount = -_potato;
            UpdatePrices();
        }
    }

    public void TrySellAll(bool ad)
    {
        var plantCount =
            _playerPlantsInventory.WheatCount
            + _playerPlantsInventory.CarrotCount
            + _playerPlantsInventory.RadishCount
            + _playerPlantsInventory.PotatoCount;

        if(plantCount> 0)
        {
            var price = 
                _playerPlantsInventory.WheatCount * _wheatPrice
                + _playerPlantsInventory.CarrotCount * _carrotPrice
                + _playerPlantsInventory.RadishCount * _radishPrice
                + _playerPlantsInventory.PotatoCount * _potatoPrice;

            if (ad)
            {
                YandexSDK.YaSDK.instance.ShowRewarded("1");
                price *= 2;
            }

            OnSell?.Invoke(price);

            _playerPlantsInventory.WheatCount = -_playerPlantsInventory.WheatCount;
            _playerPlantsInventory.CarrotCount = -_playerPlantsInventory.CarrotCount;
            _playerPlantsInventory.RadishCount = -_playerPlantsInventory.RadishCount;
            _playerPlantsInventory.PotatoCount = -_playerPlantsInventory.PotatoCount;

            UpdatePrices();
        }

    }
}
