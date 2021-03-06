﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class SelectorButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{
    // REFERENCES.
    public SelectorGroup selectorGroup;
    public Image background;

    public Color buttonSelectedColour;

    [SerializeField] protected UnityEvent onSelected;
    [SerializeField] protected UnityEvent onDeselected;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        if (selectorGroup.selectorButtonsParent != transform.parent)
        {
            Debug.LogError(this.name + " SelectorButton MUST be a child of SelectorGroup " + selectorGroup.selectorButtonsParent + " to work.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selectorGroup.OnButtonSelected(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selectorGroup.OnButtonDown(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectorGroup.OnButtonExit(this);
    }

    public void Select()
    {
        background.color = selectorGroup.ReturnSelectedColour();
        if (onSelected != null)
        {
            onSelected.Invoke();
        }
    }

    public void Deselect()
    {
        background.color = selectorGroup.buttonDefaultColour;
        if (onDeselected != null)
        {
            onDeselected.Invoke();
        }
    }

    public void AddListenerActionToOnSelected(UnityAction action)
    {
        onSelected.AddListener(action);
    }

    public void RemoveListenerActionToOnSelected(UnityAction action)
    {
        onSelected.RemoveListener(action);
    }
}