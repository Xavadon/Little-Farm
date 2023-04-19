using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantsInventory : MonoBehaviour
{
    private static int _wheatCount;
    private static int _carrotCount;
    private static int _radishCount;
    private static int _potatoCount;

    public static int WheatCount
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

            Debug.Log("Wheat: " + _wheatCount);
        }
    }

    public static int CarrotCount
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

            Debug.Log("Carrot: " + _carrotCount);
        }
    }

    public static int RadishCount
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

            Debug.Log("Radish: " + _radishCount);
        }
    }

    public static int PotatoCount
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

            Debug.Log("Potato: " + _potatoCount);
        }
    }
}
