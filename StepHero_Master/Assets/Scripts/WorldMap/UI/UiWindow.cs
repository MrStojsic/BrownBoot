using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
public abstract class UiWindow : MonoBehaviour
{
    public abstract void Initialise();

    public virtual void Hide() => gameObject.SetActive(false);

    public virtual void Show() => gameObject.SetActive(true);
}
