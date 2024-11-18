using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ClassExitScript : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private Transform cameraFinalPosition;
        [SerializeField] private UnityEngine.UI.Image fadeImage;
        [SerializeField] private float cameraPanDuration = 2.5f;
        [SerializeField] private float fadeDuration = 1.0f;
        
        public void EndScene()
        {
            StartCoroutine(PanAndZoomCamera());
            StartCoroutine(FadeScreen());
        }
        
        private IEnumerator PanAndZoomCamera()
        {
            Vector3 startPos = sceneCamera.transform.position;
            Quaternion startRot = sceneCamera.transform.rotation;

            Vector3 endPos = cameraFinalPosition.position;
            Quaternion endRot = cameraFinalPosition.rotation;

            float elapsedTime = 0f;

            while (elapsedTime < cameraPanDuration)
            {
                sceneCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / cameraPanDuration);
                sceneCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, elapsedTime / cameraPanDuration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            sceneCamera.transform.position = endPos;
            sceneCamera.transform.rotation = endRot;
        }

        private IEnumerator FadeScreen()
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
            SceneManager.LoadScene(2);
        }
    }
}