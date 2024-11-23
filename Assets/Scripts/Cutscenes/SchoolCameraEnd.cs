using System.Collections;
using UnityEngine;

public class SchoolCameraEnd : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 2.5f;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private UnityEngine.UI.Image fadeImage;
    private void OnEnable() => StartCoroutine(DelayDeactivate());

    private IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(delaySeconds);
        sceneLoader.SetFadeImage(fadeImage);
        sceneLoader.HouseTwo();
    }
}
