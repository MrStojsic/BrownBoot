using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]

public class SelectorButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{

    public SelectorGroup selectorGroup;
    public Image background;

    public Color buttonSelectedColour;

    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        if (selectorGroup.transform != transform.parent)
        {
            Debug.LogError(this.name + " SelectorButton MUST be a child of SelectorGroup " + selectorGroup + " to work.");
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
        if (onSelected != null)
        {
            onSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onDeselected != null)
        {
            onDeselected.Invoke();
        }
    }
}
