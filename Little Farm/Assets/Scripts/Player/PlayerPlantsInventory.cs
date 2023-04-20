using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPlantsInventory : PlantsInventory
{
    [SerializeField] private TextMeshProUGUI[] _wheatCountText;
    [SerializeField] private TextMeshProUGUI[] _carrotCountText;
    [SerializeField] private TextMeshProUGUI[] _radishCountText;
    [SerializeField] private TextMeshProUGUI[] _potatoCountText;

    private void Start()
    {
        UpdateUI();
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
