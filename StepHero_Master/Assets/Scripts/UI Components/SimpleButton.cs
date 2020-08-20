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
    private Color buttonDisabledColour;

    public bool IsInteractable { get { return _isInteractable; } }
    private bool _isInteractable = true;

    public UnityEvent onClicked;

    // Start is called before the first frame update
    protected void Awake()
    {
        background = GetComponent<Image>();

        buttonDisabledColour = buttonDefaultColour * 7.5f;
        buttonDefaultColour.a = 1;
    }

     void OnEnable()
    {
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
        }
        else
        {
            background.color = buttonDefaultColour*.75f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
            OnClicked();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonPressedColour;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
        }
    }

    public void OnClicked()
    {
        if (onClicked != null)
        {
            onClicked.Invoke();
        }
    }

    public void SetInteractable(bool able)
    {
        _isInteractable = able;

        OnEnable();
    }
}