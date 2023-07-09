using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMapEvent : IInteractable
{
    private bool _isInteracting;
    public bool IsInteracting
    {
        get { return _isInteracting; }
        set { _isInteracting = value; }
    }

    public void Interact()
    {
        if (IsInteracting)
        {
            Debug.Log("Enteracting");
            StopInteract();
        }
        else
        {
            IsInteracting = true;
            Debug.Log("Treasure Opened");
        }
    }

    public void StopInteract()
    {
        IsInteracting = false;
        Debug.Log("Stopped Enteracting");
    }
}
