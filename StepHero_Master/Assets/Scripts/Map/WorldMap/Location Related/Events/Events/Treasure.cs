using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Treasure : IInteractable
{
    [SerializeField] bool isInteracting = false;
    public void Interact()
    {
        if (isInteracting)
        {
            StopIneract();
        }
        else
        {
            isInteracting = true;
            Debug.Log("Treasure Opened");
        }


    }

    public void StopIneract()
    {
        isInteracting = false;
        Debug.Log("Treasure Closed");
    }
}
