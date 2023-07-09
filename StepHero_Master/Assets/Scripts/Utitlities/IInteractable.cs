using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    void StopInteract();

    bool IsInteracting
    {
        get;
        set;
    }

}
