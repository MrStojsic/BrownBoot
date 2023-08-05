using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UiWindow : MonoBehaviour
{

    // NOTE: This is here to remind you that some inheriting classes may require setting up the window before displaying it.
    //        Its best practice to call it PresetDisplay() with what ever parameters are required passed in.
    //public void PresetDisplay() { }

    public virtual void Hide() => gameObject.GetComponent<Canvas>().enabled=false;

    public virtual void Show() => gameObject.GetComponent<Canvas>().enabled = true;

    /*
{
    [SerializeField]
    protected CanvasGroup canvasGroup;
    [SerializeField]
    protected Canvas canvas;



    // NOTE: Try to keep inheriting class function order the same as here.

    public abstract void Initialise();

    // NOTE: This is here to remind you that some inheriting classes may require setting up the window before displaying it.
    //        Its best practice to call it PresetDisplay() with what ever parameters are required passed in.
    //public void PresetDisplay() { }

    // HACK
    //- This is purely for my sanity, instead of setting canvasGroup alpha and raycasts constantly duing deveopment,
    //  just toggle the canvas itself. Remember to undo this later and use the below 2 functions instead.
    public virtual void Show()
    {
        canvas.enabled = true;
    }

    public virtual void Hide()
    {
        canvas.enabled = false;
    }
    /*
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
    */

    // NOTE: Continue below with other functions below in inherited classes.







}