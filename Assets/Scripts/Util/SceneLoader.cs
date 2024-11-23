using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private Image fadeImage;
    [SerializeField] private TPSCharacter player;

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (player) player.SetControlInactive();
        StartCoroutine(FadeScreenIn());
    }
    
    private IEnumerator FadeScreenIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            var fadeImageColor = fadeImage.color;
            fadeImageColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            fadeImage.color = fadeImageColor;
            yield return null;
        }

        var endImage = fadeImage.color;
        endImage.a = 0;
        fadeImage.color = endImage;
        if (player) player.SetControlActive();
    }

    private IEnumerator FadeScreenAndLoadScene(int scene)
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
        SceneManager.LoadScene(scene);
    }
    public void SetFadeImage(Image image) => fadeImage = image;
    public void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    public void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    public void LoadHospital() => StartCoroutine(FadeScreenAndLoadScene(2));
    public void Restart() => StartCoroutine(FadeScreenAndLoadScene(1));
    public void LoadGroupTherapyTwo() => StartCoroutine(FadeScreenAndLoadScene(9));
    public void LoadSchoolThree() => StartCoroutine(FadeScreenAndLoadScene(10));
    public void LoadFirstJournal() => StartCoroutine(FadeScreenAndLoadScene(11));
    public void LoadSecondJournal() => StartCoroutine(FadeScreenAndLoadScene(12));
    public void LoadMemorial() => StartCoroutine(FadeScreenAndLoadScene(8));

    public void LoadHospice() => StartCoroutine(FadeScreenAndLoadScene(7));
    public void SchoolTwo() => StartCoroutine(FadeScreenAndLoadScene(5));
    public void LoadGroupTherapy() => StartCoroutine(FadeScreenAndLoadScene(6));
    public void SchoolOne() => StartCoroutine(FadeScreenAndLoadScene(3));
    public void HouseTwo() => StartCoroutine(FadeScreenAndLoadScene(4));
    public void LoadEndGame() => StartCoroutine(FadeScreenAndLoadScene(13));

}