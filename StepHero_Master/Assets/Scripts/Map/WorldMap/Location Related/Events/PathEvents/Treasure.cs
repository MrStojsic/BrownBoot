using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Treasure : IInteractable
{
    [SerializeField] private bool _isInteracting = false;


    [SerializeField] Item item;

    public bool IsInteracting
    {
        get { return _isInteracting; }
        set { _isInteracting = value; }
    }

    public void Interact()
    {
        if (IsInteracting)
        {
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
        Debug.Log("Treasure Closed");
    }
}
