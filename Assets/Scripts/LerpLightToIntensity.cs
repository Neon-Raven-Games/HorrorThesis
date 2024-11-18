using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpLightToIntensity : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private float targetIntensity;
    [SerializeField] private float duration;
    public void LerpLightIntensity()
    {
        StartCoroutine(LerpLightIntensityCoroutine(light, targetIntensity, duration));
    }
    
private IEnumerator LerpLightIntensityCoroutine(Light light, float targetIntensity, float duration)
    {
        float time = 0;
        float startIntensity = light.intensity;
        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        light.intensity = targetIntensity;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        light.intensity = 0;   
    }
}
