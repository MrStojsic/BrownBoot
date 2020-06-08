using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public LongHoldGroup longHoldGroup;

    public UnityEvent onLongHold;
    public UnityEvent onReset;

    public void OnPointerDown(PointerEventData eventData)
    {
        longHoldGroup.OnButtonDown(this);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        longHoldGroup.OnButtonUp(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        longHoldGroup.OnButtonExit(this);
    }


    public void OnLongHold()
    {
        print("LEFT");
        if (onLongHold != null)
        {
            onLongHold.Invoke();
        }
    }

    public void OnReset()
    {
        print("RESET");
        if (onReset != null)
        {
            onReset.Invoke();
        }
    }
}
