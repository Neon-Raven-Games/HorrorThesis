using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartActionOnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent eventOnStart;
    private void Start() => eventOnStart.Invoke();
}
