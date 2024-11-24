using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioClip gooseNeck;
    [SerializeField] private AudioClip sadPiano;
    [SerializeField] private AudioClip cinematicMusic;
    
    private float _volume;
    [SerializeField] private float fadeDuration = 1.5f;
    private Coroutine crossfadeCoroutine; 
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _volume = audioSource.volume; 
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        crossfadeCoroutine = StartCoroutine(CrossfadeRoutine(cinematicMusic));
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 1) CrossFadeTracks(gooseNeck);
        else if (arg0.buildIndex == 2) CrossFadeTracks(sadPiano);
        else if (arg0.buildIndex == 9) CrossFadeTracks(cinematicMusic);
    }

    private void CrossFadeTracks(AudioClip toClip)
    {
        if (crossfadeCoroutine != null) StopCoroutine(crossfadeCoroutine);

        crossfadeCoroutine = StartCoroutine(CrossfadeRoutine(toClip));
    }

    private IEnumerator CrossfadeRoutine(AudioClip toClip)
    {
        var elapsedTime = 0f;

        backgroundSource.clip = toClip;
        if (toClip == cinematicMusic) backgroundSource.time = 19;
        backgroundSource.Play();
        
        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(_volume, 0f, elapsedTime / fadeDuration);
            backgroundSource.volume = Mathf.Lerp(0f, _volume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.clip = toClip;
        audioSource.time = backgroundSource.time;
        audioSource.volume = _volume;
        backgroundSource.Stop();
        audioSource.Play();
    }
    private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;
    
}
