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

    private void Start()
    {
        switch (YandexSDK.YaSDK.instance.currentPlatform)
        {
            case YandexSDK.Platform.desktop:
                _desktopUI.gameObject.SetActive(true);
                _text.text = "Desktop";
                break;
            case YandexSDK.Platform.phone:
                _phoneUI.gameObject.SetActive(true);
                _text.text = "Mobile";
                break;
            default:
                _phoneUI.gameObject.SetActive(true);
                _text.text = "Mobile";
                break;
        }
    }}
