using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText;
    public float scrollSpeed = 50f;

    private float screenHeight;
    private RectTransform subtitleRect;

    private void Start()
    {
        screenHeight = Screen.height;

        subtitleRect = subtitleText.rectTransform;

        subtitleText.text = string.Format(subtitleText.text, PlayerEndData.turndTrain);

        subtitleRect.anchoredPosition = new Vector2(0, -screenHeight);

        TitleAnyKeyInput.Instance.OnAnyKeyDownHandler += () => { SceneManager.LoadScene("TitleScene"); };
    }

    private void Update()
    {
        subtitleRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (subtitleRect.anchoredPosition.y > screenHeight)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
