using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDetailSlot : MonoBehaviour
{
    
    // DATA.
    [SerializeField]
    private Slot _displayedSlot;

    [SerializeField] private InventoryPageManager _inventoryPageManager = null;


    public void DisplayItem(Slot slot)
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
