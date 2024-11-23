using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image buttonImage;
    private bool hovering;
    
    private IEnumerator HoverButton()
    {
        while(hovering) 
        {
            buttonImage.color = Color.Lerp(Color.black, Color.gray, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        StartCoroutine(HoverButton());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        StopCoroutine(HoverButton());
        buttonImage.color = Color.black;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hovering = false;
        StopCoroutine(HoverButton());
        buttonImage.color = Color.black;
    }
}
