using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UiWindow : MonoBehaviour
{
    [InspectorButton("ToggleHidden")]
    public string HideWindow;

    public void ToggleHidden() 
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        if (canvasGroup)
        {
            if (canvasGroup.alpha == 0)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

    }

    [SerializeField]
    protected CanvasGroup canvasGroup;

    // -If canvasGroup wasnt set, set it and eitherway hide it.
    public void Awake()
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    // NOTE: This is here to remind you that some inheriting classes may require setting up the window before displaying it.
    //        Its best practice to call it Initialise() with what ever parameters are required passed in.
    //public abstract void Initialise();

    public virtual void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}