using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Look into this loot table setup https://hyperfoxstudios.com/category/tutorial/

public class Loot_InventoryManager : MonoBehaviour
{
    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[15];

    public int gold;

    [SerializeField] private List<InventoryItem> debugLootItems = new List<InventoryItem>();

    public void AddLoot(List<InventoryItem> lootToAdd)
    {
        inventoryTypePockets[14].Initialise(ItemType.ANY);

        for (int i = 0; i < lootToAdd.Count; i++)
        {
            // TODO needs re implimenting
            //inventoryTypePockets[14].UnsafeForceAddItem(lootToAdd[i]);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // HACK
        AddLoot(debugLootItems);
        // TOHERE
        //InventoryInteractionManager.Instance.SetInventoryTypePockets(this, InventoryInteractionManager.InventoryType.LOOT_TAKE);
    }
}
