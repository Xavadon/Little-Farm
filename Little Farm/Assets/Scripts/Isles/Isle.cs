using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Isle : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private bool _enableCanvasOnStart;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _gameObjectToEnable;
    [SerializeField] private Collider[] _collidersToDisable;
    [SerializeField] private Isle[] _neighbours;

    private bool _isBought;
    private bool _enableGround;

    private void Start()
    {
        _canvas.gameObject.SetActive(_enableCanvasOnStart);
        _gameObjectToEnable.transform.localScale = Vector3.zero;
        if (_priceText.enabled) _priceText.text = _price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMoney playerMoney))
        {
            TryBuyIsland();
        }
    }

    private void FixedUpdate()
    {
        if(_enableGround) EnableGround();
    }

    private void TryBuyIsland()
    {
        if (PlayerMoney.Money >= _price)
        {
            _isBought = true;
            _enableGround = true;
            _canvas.gameObject.SetActive(false);

            for (int i = 0; i < _collidersToDisable.Length; i++)
            {
                _collidersToDisable[i].enabled = false;
            }

            for (int i = 0; i < _neighbours.Length; i++)
            {
                if (_neighbours != null) _neighbours[i].EnableCanvas();
            }
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
        if (!_isBought)
        {
            _canvas.gameObject.SetActive(true);
            _priceText.text = _price.ToString();
        }
    }
}
