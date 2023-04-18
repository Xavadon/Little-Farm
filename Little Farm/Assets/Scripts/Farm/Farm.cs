using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private Plant[] _fruits;

    private PlayerFarming _playerFarming;
    private bool _playerIsHere;
    private bool _workerIsHere;

    public event Action OnAllPlantsGrown;
    public event Action OnAllPlantsGet;

    public bool AllSet { get; private set; }
    public bool AllGrown { get; private set; }
    public bool AllGet { get; private set; }
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

            if (AllGet || !AllSet)
                playerFarming.ActiveSetButton(true);
            if (AllGrown)
                playerFarming.ActiveGetButton(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerFarming playerFarming))
        {
            if (other.TryGetComponent(out Worker worker))
            {
                _workerIsHere = true;
            }
            else 
                _playerIsHere = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerFarming playerFarming))
        {
            _playerIsHere = false;
            playerFarming.ActiveSetButton(false);
            playerFarming.ActiveGetButton(false);
        }

        if (other.TryGetComponent(out Worker worker))
        {
            _workerIsHere = false;
        }
    }

    private void Start()
    {
        AllGet = true;
    }

    private void CheckPlantsSet()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsSet)
                return;
        }

        AllGet = false;
        AllSet = true;
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
        AllSet = false;
        AllGrown = true;
        if (_workerIsHere) OnAllPlantsGrown?.Invoke();
        if (_playerIsHere) _playerFarming.ActiveGetButton(true);
    }


    private void CheckPlantsGet()
    {
        for (int i = 0; i < _fruits.Length; i++)
        {
            if (!_fruits[i].IsGet)
                return;
        }

        AllGet = true;
        AllSet = false;
        AllGrown = false;
        CanGet = false;
        if (_workerIsHere) OnAllPlantsGet?.Invoke();
        if (_playerIsHere) _playerFarming.ActiveSetButton(true);
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
