using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Isle : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private bool _enableCanvasOnStart;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _gameObjectToEnable;
    [SerializeField] private Farm _farmToEnable;
    [SerializeField] private NavMeshAgent _workerToEnable;
    [SerializeField] private Storage _storageToEnable;
    [SerializeField] private Collider[] _collidersToDisable;
    [SerializeField] private Isle[] _neighbours;

    private bool _enableGround;

    public bool IsBought { get; private set; }

    public event Action OnIslandBought;

    private void Start()
    {
        _canvas.gameObject.SetActive(_enableCanvasOnStart);
        _gameObjectToEnable.transform.localScale = Vector3.zero;

        if (_farmToEnable != null) _farmToEnable.gameObject.SetActive(false);
        if (_workerToEnable != null) _workerToEnable.gameObject.SetActive(false);
        if (_storageToEnable != null) _storageToEnable.gameObject.SetActive(false);

        if (_priceText.enabled) _priceText.text = _price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMoney playerMoney))
        {
            TryBuyIsland(playerMoney);
        }
    }

    private void FixedUpdate()
    {
        if(_enableGround) EnableGround();
    }

    private void TryBuyIsland(PlayerMoney playerMoney)
    {
        if (PlayerMoney.Money >= _price && _canvas.gameObject.activeSelf)
        {
            playerMoney.DecreaseMoney(_price);

            IsBought = true;
            _enableGround = true;
            _canvas.gameObject.SetActive(false);

            if (_farmToEnable != null) _farmToEnable.gameObject.SetActive(true);
            if (_workerToEnable != null) _workerToEnable.gameObject.SetActive(true);
            if (_storageToEnable != null) _storageToEnable.gameObject.SetActive(true);


            for (int i = 0; i < _collidersToDisable.Length; i++)
            {
                _collidersToDisable[i].enabled = false;
            }

            for (int i = 0; i < _neighbours.Length; i++)
            {
                if (_neighbours.Length > 0) _neighbours[i].EnableCanvas();
            }

            OnIslandBought?.Invoke();
        }
    }

    private void EnableGround()
    {
        if (_gameObjectToEnable.transform.localScale.x < 1)
        {
            Vector3 scale = _gameObjectToEnable.transform.localScale;
            float value = scale.x + 3 * Time.deltaTime;
            scale = new Vector3(value, value, value);
            _gameObjectToEnable.transform.localScale = scale;
        }
        else
        {
            _gameObjectToEnable.transform.localScale = Vector3.one;
            _enableGround = false;
        }
    }

    public void EnableCanvas()
    {
        if (!IsBought)
        {
            _canvas.gameObject.SetActive(true);
            _priceText.text = _price.ToString();
        }
    }
}
