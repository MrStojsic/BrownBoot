using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventoryManager : MonoBehaviour
{
    private static Player_InventoryManager _instance;
    public static Player_InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player_InventoryManager>();
            }
            return _instance;
        }
    }
    [SerializeField]
    private InventoryInteractionManager IIM;

    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[14];

    // DEBUG LISTS TO POPULATE INVENTORY POCKETS TILL SAVING / LOADING IS IMPLIMENTED.
    [SerializeField] private List<InventoryItem> debugFoodPocket;
    [SerializeField] private List<InventoryItem> debugPotionPocket;
    [SerializeField] private List<InventoryItem> debugMaterialPocket;

    [SerializeField] private List<InventoryItem> debugMainHandPocket;
    [SerializeField] private List<InventoryItem> debugOffHandPocket;
    [SerializeField] private List<InventoryItem> debugTwoHandPocket;
    [SerializeField] private List<InventoryItem> debugHelmetPocket;
    [SerializeField] private List<InventoryItem> debugShoulderPocket;
    [SerializeField] private List<InventoryItem> debugChestPocket;
    [SerializeField] private List<InventoryItem> debugLeggingPocket;
    [SerializeField] private List<InventoryItem> debugBootsPocket;
    [SerializeField] private List<InventoryItem> debugGlovesPocket;
    [SerializeField] private List<InventoryItem> debugNeckacePocket;
    [SerializeField] private List<InventoryItem> debugRingPocket;

    public void SetDebugPlayerinventoryPockets()
    {
        for (int i = 0; i < inventoryTypePockets.Length-1; i++)
        {
            inventoryTypePockets[i].Initialise((ItemType)i);
        }

        inventoryTypePockets[0].storedItems = debugFoodPocket;
        inventoryTypePockets[1].storedItems = debugPotionPocket;
        inventoryTypePockets[2].storedItems = debugMaterialPocket;

        inventoryTypePockets[3].storedItems = debugMainHandPocket;
        inventoryTypePockets[4].storedItems = debugOffHandPocket;
        inventoryTypePockets[5].storedItems = debugTwoHandPocket;
        inventoryTypePockets[6].storedItems = debugHelmetPocket;
        inventoryTypePockets[7].storedItems = debugShoulderPocket;
        inventoryTypePockets[8].storedItems = debugChestPocket;
        inventoryTypePockets[9].storedItems = debugLeggingPocket;
        inventoryTypePockets[10].storedItems = debugBootsPocket;
        inventoryTypePockets[11].storedItems = debugGlovesPocket;
        inventoryTypePockets[12].storedItems = debugNeckacePocket;
        inventoryTypePockets[13].storedItems = debugRingPocket;
    }
    public void Awake()
    {
        SetDebugPlayerinventoryPockets();
        IIM.InitialiseInventorySlots(inventoryTypePockets,InventoryInteractionManager.InventoryType.PLAYER_USE);
    }
}
