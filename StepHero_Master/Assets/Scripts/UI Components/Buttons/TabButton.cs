using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{

    public TabGroup tabGroup;
    public Image background;

    public Color tabSelectedColour;

    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    // Start is called before the first frame update
    void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
        if (tabGroup.transform != transform.parent)
        {
            Debug.LogError(this.name + " TabButton MUST be a child of TabGroup " + tabGroup + " to work.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tabGroup.OnTabDown(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
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
