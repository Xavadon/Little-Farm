using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class DeviceUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Canvas _desktopUI;
    [SerializeField] private Canvas _phoneUI;

    private WaitForSeconds _wait;
    private WaitForSeconds _waitCounter;
    private int _counter;

    public static int IndexUI;
    public static bool UIGet;

    private void Start()
    {
        _wait = new WaitForSeconds(0.5f);
        _waitCounter = new WaitForSeconds(1);
        StartCoroutine(GetUI());
        StartCoroutine(Counter());
    }

    private IEnumerator GetUI()
    {
        if (_counter > 3)
            StopCoroutine(GetUI());
        else
        {
            yield return _wait;
            StartCoroutine(GetUI());
            _counter++;
        }

        switch (YG.YandexGame.EnvironmentData.deviceType)
        {
            case "desktop":
                _desktopUI.gameObject.SetActive(true);
                _phoneUI.gameObject.SetActive(false);
                _text.text = "Desktop";
                IndexUI = 0;
                break;
            case "mobile":
                _desktopUI.gameObject.SetActive(false);
                _phoneUI.gameObject.SetActive(true);
                _text.text = "Mobile";
                IndexUI = 1;
                break;
            default:
                _desktopUI.gameObject.SetActive(false);
                _phoneUI.gameObject.SetActive(true);
                _text.text = "Mobile";
                IndexUI = 1;
                break;
        }
    }

    private IEnumerator Counter()
    {
        if (_counter > 3)
        {
            UIGet = true;
            StopCoroutine(Counter());
        }
        else
        {
            yield return _waitCounter;
            _counter++;
        }
    }
}
