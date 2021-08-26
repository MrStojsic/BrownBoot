using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventoryManager : Inventory
{

    // HACK This just sets up the players inventory to the debug item list for debugging, remove later.
    // DEBUG LISTS TO POPULATE INVENTORY POCKETS TILL SAVING / LOADING IS IMPLIMENTED.
    [SerializeField] private List<InventoryItem> debugFoodPocket = default;
    [SerializeField] private List<InventoryItem> debugPotionPocket = default;
    [SerializeField] private List<InventoryItem> debugMaterialPocket = default;

    [SerializeField] private List<InventoryItem> debugMainHandPocket = default;
    [SerializeField] private List<InventoryItem> debugOffHandPocket = default;
    [SerializeField] private List<InventoryItem> debugTwoHandPocket = default;
    [SerializeField] private List<InventoryItem> debugHelmetPocket = default;
    [SerializeField] private List<InventoryItem> debugShoulderPocket = default;
    [SerializeField] private List<InventoryItem> debugChestPocket = default;
    [SerializeField] private List<InventoryItem> debugLeggingPocket = default;
    [SerializeField] private List<InventoryItem> debugBootsPocket = default;
    [SerializeField] private List<InventoryItem> debugGlovesPocket = default;
    [SerializeField] private List<InventoryItem> debugNeckacePocket = default;
    [SerializeField] private List<InventoryItem> debugRingPocket = default;

    // HACK This just sets up the players inventory to the debug item list for debugging, remove later.
    public void SetDebugPlayerinventoryPockets()
    {
        for (int i = 0; i < InventoryTypePockets.Length-1; i++)
        {
            InventoryTypePockets[i].Initialise((ItemType)i);
        }

        InventoryTypePockets[0].storedItems = debugFoodPocket;
        InventoryTypePockets[1].storedItems = debugPotionPocket;
        InventoryTypePockets[2].storedItems = debugMaterialPocket;

        InventoryTypePockets[3].storedItems = debugMainHandPocket;
        InventoryTypePockets[4].storedItems = debugOffHandPocket;
        InventoryTypePockets[5].storedItems = debugTwoHandPocket;
        InventoryTypePockets[6].storedItems = debugHelmetPocket;
        InventoryTypePockets[7].storedItems = debugShoulderPocket;
        InventoryTypePockets[8].storedItems = debugChestPocket;
        InventoryTypePockets[9].storedItems = debugLeggingPocket;
        InventoryTypePockets[10].storedItems = debugBootsPocket;
        InventoryTypePockets[11].storedItems = debugGlovesPocket;
        InventoryTypePockets[12].storedItems = debugNeckacePocket;
        InventoryTypePockets[13].storedItems = debugRingPocket;
    }
    public void Awake()
    {
        // HACK This just sets up the players inventory to the debug item list for debugging, remove later.
        SetDebugPlayerinventoryPockets();
        InventoryInteractionManager.Instance.SetInventory(this,InventoryInteractionManager.InteractionType.PLAYER_USE);
        InventoryInteractionManager.Instance.ChangeFocustedInventory(true);
        // TOHERE
    }
}
