using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public string tag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag)) 
            eventToTrigger?.Invoke();
    }
}
