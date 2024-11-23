using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeactivateAfterSeconds : MonoBehaviour
{
    [SerializeField] private float seconds = 2.5f;
    [SerializeField] private UnityEvent awakeEvent;
    [SerializeField] private Transform drawerFinalSpot;
    [SerializeField] private float moveSeconds = 1f;
    [SerializeField] private Transform moveTarget;
    private void OnEnable()
    {
        StartCoroutine(DelayDeactivate());
        StartCoroutine(MoveDrawerAnimation());
    }

    public void MoveDrawer()
    {
    }
    
    private IEnumerator MoveDrawerAnimation()
    {
        var startPos = moveTarget.transform.position;

        var endPos = drawerFinalSpot.position;

        var elapsedTime = 0f;

        while (elapsedTime < moveSeconds)
        {
            moveTarget.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveSeconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moveTarget.transform.position = endPos;
    }
    
    private IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
