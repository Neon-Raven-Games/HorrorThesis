using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private Transform mother;
    [SerializeField] private Transform daughter;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject oldMotherObject;
    [SerializeField] private Animator dadAnimator;

    [SerializeField] private Transform motherFirstWaypoint;
    [SerializeField] private Transform motherSecondWaypoint;

    [SerializeField] private Transform daughterFirstWaypoint;
    [SerializeField] private Transform daughterSecondWaypoint;

    [SerializeField] private Transform cameraFinalPosition;
    [SerializeField] private float motherSpeed = 1.0f;
    [SerializeField] private float daughterSpeed = 1.0f;
    private static readonly int _SStartCutscene = Animator.StringToHash("StartCutscene");
    private static readonly int _SStop = Animator.StringToHash("Stop");
    private float rotationSpeed = 5.0f;
    [SerializeField] private Transform dadPos;


    public void OnEnable()
    {
        dadAnimator.SetTrigger(_SStartCutscene);
        dadAnimator.transform.position = dadPos.position;
        playerObject.SetActive(false);
        oldMotherObject.SetActive(false);

        StartCoroutine(StartCutsceneCoroutine());
    }

    [SerializeField] private UnityEngine.UI.Image fadeImage;
    [SerializeField]  private Camera sceneCamera;
    [SerializeField] private float cameraPanDuration = 2.5f;
    [SerializeField] private float fadeDuration = 1.0f;
    private IEnumerator StartCutsceneCoroutine()
    {
        var currentMomTarget = motherFirstWaypoint;
        var currentDaughterTarget = daughterFirstWaypoint;

        while (Vector3.Distance(mother.position, motherFirstWaypoint.position) > 1f)
        {
            Vector3 momDirection = (currentMomTarget.position - mother.position).normalized;
            Quaternion momTargetRotation = Quaternion.LookRotation(new Vector3(momDirection.x, 0, momDirection.z));
            mother.rotation = Quaternion.Slerp(mother.rotation, momTargetRotation, Time.deltaTime * rotationSpeed);
            mother.position =
                Vector3.MoveTowards(mother.position, currentMomTarget.position, motherSpeed * Time.deltaTime);

            Vector3 daughterDirection = (currentDaughterTarget.position - daughter.position).normalized;
            Quaternion daughterTargetRotation =
                Quaternion.LookRotation(new Vector3(daughterDirection.x, 0, daughterDirection.z));
            daughter.rotation =
                Quaternion.Slerp(daughter.rotation, daughterTargetRotation, Time.deltaTime * rotationSpeed);
            daughter.position = Vector3.MoveTowards(daughter.position, currentDaughterTarget.position,
                daughterSpeed * Time.deltaTime);

            yield return null;
        }

        currentMomTarget = motherSecondWaypoint;
        currentDaughterTarget = daughterSecondWaypoint;

        while (Vector3.Distance(mother.position, motherSecondWaypoint.position) > 0.1f)
        {
            Vector3 momDirection = (currentMomTarget.position - mother.position).normalized;
            Quaternion momTargetRotation = Quaternion.LookRotation(new Vector3(momDirection.x, 0, momDirection.z));
            mother.rotation = Quaternion.Slerp(mother.rotation, momTargetRotation, Time.deltaTime * rotationSpeed);
            mother.position =
                Vector3.MoveTowards(mother.position, currentMomTarget.position, motherSpeed * Time.deltaTime);

            daughter.position = Vector3.MoveTowards(daughter.position, currentDaughterTarget.position,
                daughterSpeed * Time.deltaTime);
            var dadDirection = (dadAnimator.transform.position - mother.position).normalized;
            Quaternion dadTargetRotation = Quaternion.LookRotation(new Vector3(dadDirection.x, 0, dadDirection.z));
            mother.rotation = Quaternion.Slerp(mother.rotation, dadTargetRotation, Time.deltaTime * rotationSpeed);

            dadDirection = (dadAnimator.transform.position - daughter.position).normalized;
            dadTargetRotation = Quaternion.LookRotation(new Vector3(dadDirection.x, 0, dadDirection.z));
            daughter.rotation = Quaternion.Slerp(daughter.rotation, dadTargetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }


        mother.GetComponent<Animator>().SetTrigger(_SStop);
        yield return new WaitForSeconds(0.4f);
        daughter.GetComponent<Animator>().SetTrigger(_SStop);
        StartCoroutine(PanAndZoomCamera());
    }

    [SerializeField] private SceneLoader sceneLoader;
    private IEnumerator PanAndZoomCamera()
    {
        Vector3 startPos = sceneCamera.transform.position;
        Quaternion startRot = sceneCamera.transform.rotation;

        Vector3 endPos = cameraFinalPosition.position;
        Quaternion endRot = cameraFinalPosition.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < cameraPanDuration*0.75f)
        {
            sceneCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / cameraPanDuration);
            sceneCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, elapsedTime / cameraPanDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sceneLoader.SetFadeImage(fadeImage);
        sceneLoader.LoadHospital();
        
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
}