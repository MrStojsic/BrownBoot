using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMapEvent : IInteractable
{
    public void Interact()
    {
        Debug.Log("Enteracting");
        throw new System.NotImplementedException();
    }

    public void StopIneract()
    {
        Debug.Log("Stopped Enteracting");
        throw new System.NotImplementedException();
    }
}
