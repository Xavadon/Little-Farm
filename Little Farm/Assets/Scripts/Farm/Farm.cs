using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private Plant[] _fruits;

    public event Action OnAllPlantsGrown;
    public event Action OnAllPlantsGet;

    private void OnEnable()
    {
        Plant.OnPlantGrown += CheckPlantsGrown;
        Plant.OnPlantGet += CheckPlantsGet;
    }

    private void OnDisable()
    {
        Plant.OnPlantGrown -= CheckPlantsGrown;
        Plant.OnPlantGet -= CheckPlantsGet;
    }

    private void CheckPlantsGrown()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsGrown)
                return;
        }

        OnAllPlantsGrown?.Invoke();
    }

    private void CheckPlantsGet()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsGet)
                return;
        }

        OnAllPlantsGet?.Invoke();
    }

}
