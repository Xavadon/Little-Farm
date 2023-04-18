using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerFarming : MonoBehaviour
{
    [SerializeField] protected AnimatorHandler _animatorHandler;
    [SerializeField] private Button[] _setPlantsButton;
    [SerializeField] private Button[] _getPlantsButton;

    protected List<Plant> _fruits = new List<Plant>(0);
    private bool _canSet;
    private bool _canGet;
    protected int _indexUI;

    protected void Start()
    {
        StartCoroutine(InteractWithPlants());
        GetUI();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Plant fruit))
        {
            _fruits.Add(fruit);
        }
        if (other.TryGetComponent(out Farm farm))
        {
            if (_setPlantsButton.Length > 0) _setPlantsButton[_indexUI].onClick.AddListener(farm.SetPlants);
            if (_setPlantsButton.Length > 0) _getPlantsButton[_indexUI].onClick.AddListener(farm.GetPlants);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Farm farm))
        {
            _canSet = farm.CanSet;
            _canGet = farm.CanGet;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Plant fruit))
        {
            _fruits.Remove(fruit);
        }
        if (other.TryGetComponent(out Farm farm))
        {
            _canSet = false;
            _canGet = false;
            _setPlantsButton[_indexUI].onClick.RemoveListener(farm.SetPlants);
            _getPlantsButton[_indexUI].onClick.RemoveListener(farm.GetPlants);
        }
    }

    protected void GetUI()
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
    }

    protected virtual IEnumerator InteractWithPlants()
    {
        if (_fruits.Count > 0 && _canSet || _canGet)
        {
            if (_animatorHandler != null)
                _animatorHandler.PlayTargetAnimation("Attack", 0.10f, true);

            foreach (var item in _fruits)
            {
                item.SetPlant();
                item.GetPlant();
            }
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(InteractWithPlants());
    }

    public void ActiveSetButton(bool value)
    {
        if(_setPlantsButton.Length > 0) _setPlantsButton[_indexUI].gameObject.SetActive(value);
    }

    public void ActiveGetButton(bool value)
    {
        if (_setPlantsButton.Length > 0) _getPlantsButton[_indexUI].gameObject.SetActive(value);
    }
}
