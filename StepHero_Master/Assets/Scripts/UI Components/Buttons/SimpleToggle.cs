using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class SimpleToggle : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image background;

    public Color buttonDefaultColour;
    public Color buttonPressedColour;

    public UnityEvent onTrue;
    public UnityEvent onFalse;

    public bool isOn;
    public bool defaultState = false;

    // Start is called before the first frame update
    protected void Awake()
    {
        background = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetToggleDisplay(false);
        isOn = !isOn;
        if (isOn)
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SetToggleDisplay(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetToggleDisplay(true);
    }

    public void Reset()
    {
        isOn = defaultState;
        SetToggleDisplay(true);

    }
    private void SetToggleDisplay(bool regularPressed)
    {
        if (regularPressed)
        {
            background.color = isOn ? buttonPressedColour : buttonDefaultColour;
        }
        else
        {
            background.color = isOn ? buttonDefaultColour : buttonPressedColour;

        }
    }

    public void OnTrue()
    {
        if (onTrue != null)
        {
            onTrue.Invoke();
        }
    }
    public void OnFalse()
    {
        if (onFalse != null)
        {
            onFalse.Invoke();
        }
    }
}