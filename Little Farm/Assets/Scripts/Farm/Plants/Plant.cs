using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    [SerializeField] private Farm _parentFarm;

    private Vector3 _scaleVector;
    private Vector3 _startScaleVector;
    private float _maxScaleValue;
    private float _scaleValue;
    private bool _canSet;
    private bool _canGet;

    public bool IsGrown { get; private set; }
    public bool IsGet { get; private set; }
    public bool IsSet { get; private set; }

    public static event Action OnPlantGrown;
    public static event Action OnPlantGet;
    public static event Action OnPlantSet;

    private void OnEnable()
    {
        _parentFarm.OnAllPlantsGrown += CanGetPlants;
        _parentFarm.OnAllPlantsGet += CanSetPlants;
    }
    
    private void OnDisable()
    {
        _parentFarm.OnAllPlantsGrown -= CanGetPlants;
        _parentFarm.OnAllPlantsGet -= CanSetPlants;
    }

    private void Start()
    {
        _canSet = true;
        _maxScaleValue = transform.localScale.x;
        _startScaleVector = new Vector3(_maxScaleValue * 0.3f, _maxScaleValue * 0.3f, _maxScaleValue * 0.3f);
        transform.localScale = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (IsSet && !IsGrown)
            Grow();
    }

    private void CanSetPlants()
    {
        _canSet = true;
    }
    
    private void CanGetPlants()
    {
        _canGet = true;
    }

    public void SetPlant()
    {
        if (_canSet)
        {
            IsSet = true;
            _canSet = false;
            IsGet = false;
            transform.localScale = _startScaleVector;

            OnPlantSet?.Invoke();
        }
    }

    public void GetPlant()
    {
        if (_canGet)
        {
            IsSet = false;
            _canGet = false;
            IsGrown = false;
            IsGet = true;
            transform.localScale = Vector3.zero;

            AddPlantToInventory();
            OnPlantGet?.Invoke();
        }
    }

    protected abstract void AddPlantToInventory();

    private void Grow()
    {
        if (transform.localScale.x < _maxScaleValue)
        {
            _scaleVector = transform.localScale;
            _scaleValue = _scaleVector.x + 0.05f * Time.deltaTime;
            _scaleVector = new Vector3(_scaleValue, _scaleValue, _scaleValue);
            transform.localScale = _scaleVector;
        }
        else
        {
            transform.localScale = new Vector3(_maxScaleValue, _maxScaleValue, _maxScaleValue);
            IsGrown = true;
            OnPlantGrown?.Invoke();
        }
    }
}
