﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Look into this loot table setup https://hyperfoxstudios.com/category/tutorial/

public class Loot_InventoryManager : MonoBehaviour
{
    private static Loot_InventoryManager _instance;
    public static Loot_InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Loot_InventoryManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[15];

    public int gold;

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
        //IIM.SetOtherInventoryTypePockets(inventoryTypePockets, InventoryInteractionManager.InventoryType.LOOT_TAKE );
    }
}
