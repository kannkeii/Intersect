using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideshowManager : MonoBehaviour
{
    public List<CanvasGroup> slides;
    public float fadeDuration = 1.0f;
    public float slideDuration = 3.0f;

    private int currentSlideIndex = 0;
    private Coroutine transitionCoroutine;

    private void Start()
    {
        SetSlideAlpha(currentSlideIndex, 1.0f);

        StartCoroutine(StartSlideshow());
    }

    private void Update()
    {

    }

    private IEnumerator StartSlideshow()
    {
        while (true)
        {
            yield return StartCoroutine(FadeOut(currentSlideIndex));

            currentSlideIndex = (currentSlideIndex + 1) % slides.Count;

            yield return StartCoroutine(FadeIn(currentSlideIndex));

            yield return new WaitForSeconds(slideDuration);
        }
    }

    private IEnumerator FadeOut(int index)
    {
        float elapsedTime = 0;
        float startAlpha = slides[index].alpha;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            SetSlideAlpha(index, newAlpha);
            yield return null;
        }
        SetSlideAlpha(index, 0);
    }

    private IEnumerator FadeIn(int index)
    {
        float elapsedTime = 0;
        float startAlpha = slides[index].alpha;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1, elapsedTime / fadeDuration);
            SetSlideAlpha(index, newAlpha);
            yield return null;
        }
        SetSlideAlpha(index, 1);
    }

    private void SetSlideAlpha(int index, float alpha)
    {
        slides[index].alpha = alpha;
    }
}
