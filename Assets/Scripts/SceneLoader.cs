using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private UnityEngine.UI.Image fadeImage;

    public void SchoolTwo()
    {
        StartCoroutine(FadeScreen(4));
    }

    public void EndGame()
    {
        StartCoroutine(FadeScreen(-1));
    }

    public void LoadGroupTherapy()
    {
        StartCoroutine(FadeScreen(5));
    }

    public void SchoolOne()
    {
        StartCoroutine(FadeScreen(2));
    }

    public void HouseTwo()
    {
        StartCoroutine(FadeScreen(3));
    }

    private IEnumerator FadeScreen(int scene)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            var fadeImageColor = fadeImage.color;
            fadeImageColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            fadeImage.color = fadeImageColor;
            yield return null;
        }

        fadeImage.color = Color.black;
        if (scene != -1) SceneManager.LoadScene(scene);
    }
}