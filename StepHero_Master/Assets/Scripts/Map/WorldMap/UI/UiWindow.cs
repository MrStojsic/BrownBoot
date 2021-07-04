using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
public abstract class UiWindow : MonoBehaviour
{
    // NOTE: Try to keep inheriting class function order the same as here.

    public abstract void Initialise();

    // NOTE: This is here to remind you that some inheriting classes may require setting up the window before displaying it.
    //        Its best practice to call it PresetDisplay() with what ever parameters are required passed in.
    //public void PresetDisplay() { }

    public virtual void Hide() => gameObject.SetActive(false);

    public virtual void Show() => gameObject.SetActive(true);

    // NOTE: Continue below with other functions below in inherited classes.
}