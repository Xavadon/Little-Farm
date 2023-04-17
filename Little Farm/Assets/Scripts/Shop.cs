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
    [SerializeField] private int _wheatPrice = 4;
    [SerializeField] private int _carrotPrice = 3;
    [SerializeField] private int _radishPrice = 2;
    [SerializeField] private int _potatoPrice = 1;

    private int _wheat;
    private int _carrot;
    private int _radish;
    private int _potato;
    private int _indexUI;

    public static event Action<int> OnSell;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMoney playerMoney))
        {
            //UIToEnable[_indexUI].SetActive(true);
            TrySellAllPlants();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMoney playerMoney))
        {
            //UIToEnable[_indexUI].SetActive(false);
        }
    }

    private void Start()
    {
        SetUI();
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

        //UIToEnable[_indexUI].SetActive(false);
    }

    public void TrySellAllPlants()
    {
        _wheat = PlayerPlantsInventory.WheatCount;
        _carrot = PlayerPlantsInventory.CarrotCount;
        _radish = PlayerPlantsInventory.RadishCount;
        _potato = PlayerPlantsInventory.PotatoCount;

        if (_wheat > 0)
        {
            OnSell?.Invoke(_wheat * _wheatPrice);
            PlayerPlantsInventory.WheatCount = -_wheat;
        }

        if (_carrot > 0)
        {
            OnSell?.Invoke(_carrot * _carrotPrice);
            PlayerPlantsInventory.CarrotCount = -_carrot;
        }

        if (_radish > 0)
        {
            OnSell?.Invoke(_radish * _radishPrice);
            PlayerPlantsInventory.RadishCount = -_radish;
        }

        if (_potato > 0)
        {
            OnSell?.Invoke(_potato * _potatoPrice);
            PlayerPlantsInventory.PotatoCount = -_potato;
        }
    }
}
