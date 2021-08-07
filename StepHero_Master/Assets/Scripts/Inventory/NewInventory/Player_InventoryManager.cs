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
    private InventoryInteractionManager inventoryInteractionManager = default;

    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[14];

    public int gold = default;

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
        // HACK Leave this so inventory checks can still happen on Player!!!!
        SetDebugPlayerinventoryPockets();
        inventoryInteractionManager.ChangeFocustedInventoryTypePockets(true);
        // TOHERE
    }
}
