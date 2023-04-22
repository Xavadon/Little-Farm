using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTest : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _device;

    private void FixedUpdate()
    {
        _device.text = YG.YandexGame.EnvironmentData.deviceType.ToString();
    }
}
