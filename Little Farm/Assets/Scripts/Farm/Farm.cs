using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private Plant[] _fruits;

    private PlayerFarming _playerFarming;
    private bool _allSet;
    private bool _allGet;

    public event Action OnAllPlantsGrown;
    public event Action OnAllPlantsGet;

    public bool CanSet { get; private set; }
    public bool CanGet { get; private set; }

    private void OnEnable()
    {
        Plant.OnPlantGrown += CheckPlantsGrown;
        Plant.OnPlantGet += CheckPlantsGet;
        Plant.OnPlantSet += CheckPlantsSet;
    }

    private void OnDisable()
    {
        Plant.OnPlantGrown -= CheckPlantsGrown;
        Plant.OnPlantGet -= CheckPlantsGet;
        Plant.OnPlantSet -= CheckPlantsSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerFarming playerFarming))
        {
            _playerFarming = playerFarming;

            if (!_allSet)
                playerFarming.ActiveSetButton(true);
            else if (!_allGet && _allSet)
                playerFarming.ActiveGetButton(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerFarming playerFarming))
        {
            playerFarming.ActiveSetButton(false);
            playerFarming.ActiveGetButton(false);
        }
    }
    private void CheckPlantsSet()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsSet)
                return;
        }

        _allSet = true;
        CanSet = false;
    }

    private void CheckPlantsGrown()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsGrown)
                return;
        }

        CanGet = false;
        CanSet = false;
        _playerFarming.ActiveGetButton(true);
    }


    private void CheckPlantsGet()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsGet)
                return;
        }

        _allGet = true;
        _allSet = false;
        CanGet = false;
        _playerFarming.ActiveSetButton(true);
    }

    public void SetPlants()
    {
        CanSet = true;
        OnAllPlantsGet?.Invoke(); // can set
    }

    public void GetPlants()
    {
        CanGet = true;
        OnAllPlantsGrown?.Invoke();
    }

}
