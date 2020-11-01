using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LongHoldButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image background;

    public Color buttonDefaultColour;
    public Color buttonPressedColour;
    private Color buttonDisabledColour;


    private bool _isInteractable = true;
    public bool IsInteractable { get { return _isInteractable; } }

    public UnityEvent onClicked;

    private float pressTime;
    private bool isHeld = false;
    private bool doHold = false;
    [SerializeField] private float heldDelayTimer = default;

    // Start is called before the first frame update
    protected void Awake()
    {
        buttonDisabledColour = buttonDefaultColour * 7.5f;
        buttonDefaultColour.a = 1;
    }

    void OnEnable()
    {
        isHeld = false;
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
        }
        else
        {
            background.color = buttonDefaultColour * .75f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
            isHeld = false;
            doHold = false;
            OnClicked();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonPressedColour;
            pressTime = Time.realtimeSinceStartup;
            isHeld = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            background.color = buttonDefaultColour;
            isHeld = false;
            doHold = false;
        }
    }

    void Update()
    {
        if (!isHeld) return;

        if (Time.realtimeSinceStartup - pressTime >= heldDelayTimer)
        {
            pressTime = Time.realtimeSinceStartup;
            print("Handle Long Tap");
            if (doHold)
            {
                OnClicked();
            }
            doHold = true;
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