using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiPanel : MonoBehaviour
{
    public abstract void Initialise();

    public virtual void Hide() => gameObject.SetActive(false);

    public virtual void SHow() => gameObject.SetActive(true);
}
