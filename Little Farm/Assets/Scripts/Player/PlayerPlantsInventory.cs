using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPlantsInventory : PlantsInventory
{
    [SerializeField] private bool _savesEnabled;
    [SerializeField] private TextMeshProUGUI[] _wheatCountText;
    [SerializeField] private TextMeshProUGUI[] _carrotCountText;
    [SerializeField] private TextMeshProUGUI[] _radishCountText;
    [SerializeField] private TextMeshProUGUI[] _potatoCountText;

    private WaitForSeconds _wait;

    private void OnEnable()
    {
        YG.YandexGame.GetDataEvent += GetProgress;
    }

    private void OnDisable()
    {
        YG.YandexGame.GetDataEvent -= GetProgress;
    }

    private void Start()
    {
        UpdateUI();
        _wait = new WaitForSeconds(5.1f);
        
    }

    private void GetProgress()
    {
        _wheatCount = YG.YandexGame.savesData.wheat;
        _carrotCount = YG.YandexGame.savesData.carrot;
        _radishCount = YG.YandexGame.savesData.radish;
        _potatoCount = YG.YandexGame.savesData.potato;

        UpdateUI();

        if(_savesEnabled) StartCoroutine(SaveProgress());
    }

    private IEnumerator SaveProgress()
    {

        yield return _wait;

        YG.YandexGame.savesData.wheat = _wheatCount;
        YG.YandexGame.savesData.carrot = _carrotCount;
        YG.YandexGame.savesData.radish = _radishCount;
        YG.YandexGame.savesData.potato = _potatoCount;


        UpdateUI();


        YG.YandexGame.SaveProgress();

        StartCoroutine(SaveProgress());
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < _wheatCountText.Length; i++)
        {
            _wheatCountText[i].text = _wheatCount.ToString();
        }

        for (int i = 0; i < _wheatCountText.Length; i++)
        {
            _carrotCountText[i].text = _carrotCount.ToString();
        }

        for (int i = 0; i < _wheatCountText.Length; i++)
        {
            _radishCountText[i].text = _radishCount.ToString();
        }

        for (int i = 0; i < _wheatCountText.Length; i++)
        {
            _potatoCountText[i].text = _potatoCount.ToString();
        }
    }

}
