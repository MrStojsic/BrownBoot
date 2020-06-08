using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Image))]
public class SimpleButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image background;

    public Sprite sprite;

    public Color buttonDefaultColour;
    public Color buttonPressedColour;

    public UnityEvent onClicked;

    // Start is called before the first frame update
    protected void Awake()
    {
        background = GetComponent<Image>();
    }
    void OnEnable()
    {
        background.color = buttonDefaultColour;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        background.color = buttonDefaultColour;
        OnClicked();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        background.color = buttonPressedColour;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.color = buttonDefaultColour;
    }

    public void OnClicked()
    {
        if (onClicked != null)
        {
            onClicked.Invoke();
        }
    }
}