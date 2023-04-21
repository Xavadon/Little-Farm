using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleCanvasEnabler : MonoBehaviour
{
    [SerializeField] private Isle[] _islesBuyToSetNextLevel;
    [SerializeField] private Isle[] _islesToEnableCanvas;

    private void OnEnable()
    {
        for (int i = 0; i < _islesBuyToSetNextLevel.Length; i++)
        {
            _islesBuyToSetNextLevel[i].OnIslandBought += CheckIslesBought;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _islesBuyToSetNextLevel.Length; i++)
        {
            _islesBuyToSetNextLevel[i].OnIslandBought -= CheckIslesBought;
        }
    }

    private void CheckIslesBought()
    {
        for (int i = 0; i < _islesBuyToSetNextLevel.Length; i++)
        {
            if (!_islesBuyToSetNextLevel[i].IsBought)
                return;
        }

        EnableIslesToBuy();
    }

    private void EnableIslesToBuy()
    {
        for (int i = 0; i < _islesToEnableCanvas.Length; i++)
        {
            _islesToEnableCanvas[i].EnableCanvas();
        }
    }
}
