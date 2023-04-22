using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeviceSceneLoader : MonoBehaviour
{
    private void Start()
    {
        switch (YG.YandexGame.EnvironmentData.deviceType)
        {
            case "desktop":
                break;
            case "mobile":
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(1);
                break;
        }
    }
}
