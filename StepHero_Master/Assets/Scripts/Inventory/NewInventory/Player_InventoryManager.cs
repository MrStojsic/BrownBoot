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



    [SerializeField] public List<InventoryItem>[] inventoryPockets = new List<InventoryItem>[14];
    [SerializeField] private int[] inventoryPocketsSizeLimits = new int[14];

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


        print(debugFoodPocket[0].item.Title);
        inventoryPockets[0] = debugFoodPocket;
        inventoryPockets[1] = debugPotionPocket;
        inventoryPockets[2] = debugMaterialPocket;

        inventoryPockets[3] = debugMainHandPocket;
        inventoryPockets[4] = debugOffHandPocket;
        inventoryPockets[5] = debugTwoHandPocket;
        inventoryPockets[6] = debugHelmetPocket;
        inventoryPockets[7] = debugShoulderPocket;
        inventoryPockets[8] = debugChestPocket;
        inventoryPockets[9] = debugLeggingPocket;
        inventoryPockets[10] = debugBootsPocket;
        inventoryPockets[11] = debugGlovesPocket;
        inventoryPockets[12] = debugNeckacePocket;
        inventoryPockets[13] = debugRingPocket;
    }
    public void Awake()
    {
        SetDebugPlayerinventoryPockets();

    }

}
