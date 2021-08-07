using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Look into this loot table setup https://hyperfoxstudios.com/category/tutorial/

public class Loot_InventoryManager : MonoBehaviour
{
    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[15];

    public int gold;

    [SerializeField]
    private InventoryInteractionManager inventoryInteractionManager = default;

    [SerializeField] private List<InventoryItem> debugLootItems = new List<InventoryItem>();

    public void AddLoot(List<InventoryItem> lootToAdd)
    {
        print("ADD LOOT");
        inventoryTypePockets[14].Initialise(ItemType.ANY);

        for (int i = 0; i < lootToAdd.Count; i++)
        {
            inventoryTypePockets[14].SafeForceAddItem(lootToAdd[i]);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // HACK
        AddLoot(debugLootItems);
        // TOHERE
        //inventoryInteractionManager.SetNonPlayerInventoryTypePockets(inventoryTypePockets, InventoryInteractionManager.InventoryType.LOOT_TAKE);
    }
}
