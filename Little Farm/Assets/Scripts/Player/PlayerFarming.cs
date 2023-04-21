using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerFarming : MonoBehaviour
{
    [SerializeField] protected AnimatorHandler _animatorHandler;
    [SerializeField] protected Button[] _setPlantsButton;
    [SerializeField] protected Button[] _getPlantsButton;

    protected List<Plant> _fruits = new List<Plant>(0);
    protected bool _canSet;
    protected bool _canGet;
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
            if (_setPlantsButton.Length > 0) _setPlantsButton[_indexUI].onClick.RemoveListener(farm.SetPlants);
            if (_setPlantsButton.Length > 0) _getPlantsButton[_indexUI].onClick.RemoveListener(farm.GetPlants);
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
            if (_animatorHandler != null && !_animatorHandler.animator.GetBool("IsAttacking"))
                _animatorHandler.PlayTargetAnimation("Attack", 0.10f, true);

            foreach (var item in _fruits)
            {
                item.SetPlant();
                item.GetPlant();
            }
        }
        else
        {
            if (_animatorHandler != null)
                _animatorHandler.animator.SetBool("IsAttacking", false);
        }

        yield return new WaitForSeconds(0.15f);
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
