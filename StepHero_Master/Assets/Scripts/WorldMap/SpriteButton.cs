using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class SpriteButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Color buttonDefaultColour;
    public Color buttonPressedColour;

    public UnityEvent onClicked;

    // Start is called before the first frame update
    protected void Awake()
    {
        if(spriteRenderer == null)
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        spriteRenderer.color = buttonDefaultColour;
    }

    public void OnMouseUpAsButton()
    {
        spriteRenderer.color = buttonDefaultColour;
        OnClicked();
    }
    public void OnMouseDown()
    {
        spriteRenderer.color = buttonPressedColour;
    }

    public void OnMouseExit()
    {
        spriteRenderer.color = buttonDefaultColour;
    }

    public void OnClicked()
    {
        if (onClicked != null)
        {
            onClicked.Invoke();
        }
    }
}
