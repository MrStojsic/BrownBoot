using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventoryManager : PlayerInventory
{
    // HACK This just sets up the players inventory to the debug item list for debugging, remove later.
    // DEBUG LISTS TO POPULATE INVENTORY POCKETS TILL SAVING / LOADING IS IMPLIMENTED.
    [SerializeField]
    private InventoryTypePocket _debugInventoryTypePockets = default;

    // HACK This just sets up the players inventory to the debug item list for debugging, remove later.
    public void SetDebugPlayerinventoryPockets()
    {
        for (int i = 0; i < InventoryTypePockets.Length-1; i++)
        {
            InventoryTypePockets[i].Initialise((ItemType)i);
        }

        for (int i = 0; i < _debugInventoryTypePockets.Count; i++)
        {
            AttemptAddItem(_debugInventoryTypePockets.storedItems[i].Item, _debugInventoryTypePockets.storedItems[i].NumberOfItem);
        }
    }
    public void Awake()
    {
        // HACK This just sets up the players inventory to the debug item list for debugging, remove later.

        SetDebugPlayerinventoryPockets();
        //InventoryPageManager.Instance.SetNonPlayerInventory(this);
        InventoryPageManager.Instance.ChangeFocusedInventory(true);
        // TOHERE
    }
}
