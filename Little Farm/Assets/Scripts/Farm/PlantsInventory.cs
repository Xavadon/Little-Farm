using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsInventory : MonoBehaviour
{
    protected int _wheatCount;
    protected int _carrotCount;
    protected int _radishCount;
    protected int _potatoCount;

    public int WheatCount
    {
        get { return _wheatCount; }
        set
        {
            if (value >= 0)
                _wheatCount += value;
            else if (_wheatCount + value >= 0)
                _wheatCount += value;
            else
                throw new ArgumentException();

            Debug.Log("Wheat: " + _wheatCount + " " + gameObject.name);
            UpdateUI();
        }
    }

    public int CarrotCount
    {
        get { return _carrotCount; }
        set
        {
            if (value >= 0)
                _carrotCount += value;
            else if (_carrotCount + value >= 0)
                _carrotCount += value;
            else
                throw new ArgumentException();

            Debug.Log("Carrot: " + _carrotCount + " " + gameObject.name);
            UpdateUI();
        }
    }

    public int RadishCount
    {
        get { return _radishCount; }
        set
        {
            if (value >= 0)
                _radishCount += value;
            else if (_radishCount + value >= 0)
                _radishCount += value;
            else
                throw new ArgumentException();

            Debug.Log("Radish: " + _radishCount + " " + gameObject.name);
            UpdateUI();
        }
    }

    public int PotatoCount
    {
        get { return _potatoCount; }
        set
        {
            if (value >= 0)
                _potatoCount += value;
            else if (_potatoCount + value >= 0)
                _potatoCount += value;
            else
                throw new ArgumentException();

            Debug.Log("Potato: " + _potatoCount + " " + gameObject.name);
            UpdateUI();
        }
    }

    public virtual void UpdateUI()
    {

    }
}
