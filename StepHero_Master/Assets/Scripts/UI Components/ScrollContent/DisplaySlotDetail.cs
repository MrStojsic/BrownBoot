using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisplaySlotDetail : Slot
{
    
    // DATA.
    [SerializeField]
    protected Slot _displayedSlot;


    public virtual void DisplayDetail(Slot slot)
    {
        if (transform.parent != slot.transform.parent)
        {
            transform.SetParent(slot.transform.parent);
        }
        transform.SetSiblingIndex(slot.Index);

        _displayedSlot = slot;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        Canvas.ForceUpdateCanvases();
    }

    public void HideEntireDisplay()
    {
        if (_displayedSlot != null)
        {
            _displayedSlot.SelectorButton.Deselect();
        }
        gameObject.SetActive(false);
        _displayedSlot = null;
    }
}
