using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSizeController : MonoBehaviour
{
    private const float TARGET_ASPECT = 9.0f / 16.0f;

    private void Start()
    {
        int width = Screen.width;
        int height = Screen.height;
        float aspect = (float)width / (float)height;

        if (aspect != TARGET_ASPECT)
        {
            int newWidth = Mathf.RoundToInt(height * TARGET_ASPECT);
            Screen.SetResolution(newWidth, height, Screen.fullScreen);
        }

        AudioController.Instance.PlayMusic(0);
    }
}
