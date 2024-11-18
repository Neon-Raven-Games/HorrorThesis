using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueOption : MonoBehaviour
{
    public string text;
    public string firstOption;
    public string secondOption;
    public Transform focus;
    public UnityEvent firstOptionEvent;
    public UnityEvent secondOptionEvent;
    [SerializeField] DialogueScript dialogueScript;
    public void Update()
    {
    }
}
