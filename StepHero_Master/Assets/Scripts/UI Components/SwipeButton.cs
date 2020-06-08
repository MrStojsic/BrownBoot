using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public SwipeGroup swipeGroup;

    public UnityEvent onSwipeLeft;
    public UnityEvent onSwipeRight;
    public UnityEvent onReset;

    private Vector2 _lastPosition = Vector2.zero;

    private bool _isSwiping = false;
    public bool IsSwiping { get => _isSwiping; }

    public int tap;

    void Awake()
    {

        swipeGroup = transform.parent.GetComponentInParent<SwipeGroup>();
        if (swipeGroup == null)
        {
            Debug.LogError(this.name + " SwipeButton MUST be a child of SwipeGroup " + swipeGroup + " to work.");
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        swipeGroup.ResetSelectedSwipeButton();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        swipeGroup.ResetSelectedSwipeButton();
        _lastPosition = eventData.position;
        Debug.Log("Drag begin");
        swipeGroup.mainScroll.OnBeginDrag(eventData);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _isSwiping = false;
        Debug.Log("Drag End");
        swipeGroup.mainScroll.OnEndDrag(eventData);
    }

    
   public void OnDrag(PointerEventData eventData)
   {

       Vector2 direction = eventData.position - _lastPosition;

       if (_isSwiping == false)
       {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && Mathf.Abs(direction.x) > swipeGroup.minsSwipeLenght)
            {
                _isSwiping = true;
                swipeGroup.SetSelectedSwipeButton(this);
                if (direction.x > 0)
                {
                    OnSwipeRight();

                }
                else
                {
                    OnSwipeLeft();

                }
            }
            else
            {
                swipeGroup.mainScroll.OnDrag(eventData);
            }
       }
       _lastPosition = eventData.position;
       
}



public void OnSwipeLeft()
    {
        print("LEFT");
        if (onSwipeLeft != null)
        {
            onSwipeLeft.Invoke();
        }
    }

    public void OnSwipeRight()
    {
        print("RIGHT");
        if (onSwipeRight != null)
        {
            onSwipeRight.Invoke();
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

