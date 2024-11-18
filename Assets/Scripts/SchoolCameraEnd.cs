using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolCameraEnd : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private float delaySeconds = 2.5f;
    [SerializeField] private SceneLoader _sceneLoader;
    void OnEnable()
    {
        StartCoroutine(DelayDeactivate());
    }

    private IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(delaySeconds);
        _sceneLoader.HouseTwo();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
