using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerFarming : MonoBehaviour
{
    [SerializeField] private AnimatorHandler _animatorHandler;
    [SerializeField] private Button[] _setPlantsButton;
    [SerializeField] private Button[] _getPlantsButton;

    private List<Plant> _fruits = new List<Plant>(0);
    private bool _canSet;
    private bool _canGet;
    private int _indexUI;

    private void Start()
    {
        StartCoroutine(InteractWithPlants());
        GetUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Plant fruit))
        {
            _fruits.Add(fruit);
        }
        if (other.TryGetComponent(out Farm farm))
        {
            _setPlantsButton[_indexUI].onClick.AddListener(farm.SetPlants);
            _getPlantsButton[_indexUI].onClick.AddListener(farm.GetPlants);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Farm farm))
        {
            _canSet = farm.CanSet;
            _canGet = farm.CanGet;
        }
    }

    private void OnTriggerExit(Collider other)
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

    private void GetUI()
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

    private IEnumerator InteractWithPlants()
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
        _setPlantsButton[_indexUI].gameObject.SetActive(value);
    }

    public void ActiveGetButton(bool value)
    {
        _getPlantsButton[_indexUI].gameObject.SetActive(value);
    }
}
