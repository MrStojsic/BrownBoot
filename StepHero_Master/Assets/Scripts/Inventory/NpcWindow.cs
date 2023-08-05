using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npcs;

public class NpcWindow : UiWindow
{
    // TODO.. This isnt final, i dont know if many windows will need NPC data at all..
    private IInteractable interactable;

    public virtual void Show(IInteractable interactable)
    {
        //- Rememer we can pass in an interactable but access it as its final class eg Merchant
        //  by writing "(interactable as Merchant).functionality;"
        this.interactable = interactable;
        base.Show();
    }
    
    public override void Hide()
    {
        interactable.IsInteracting = false;
        base.Hide();
        interactable = null;

    }
    
}
