using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [SerializeField] List<Item> lootItems;
    [SerializeField] List<Item> lootEquipment;

    public void AddLoot(List<Item> lootToAdd)
    {
        if (lootItems == null)
        {
            lootItems = new List<Item>();
        }
        if (lootEquipment == null)
        {
            lootEquipment = new List<Item>();
        }

        foreach (Item lootItem in lootToAdd)
        {
            if (lootItem.GetType() == typeof(Equipment))
            {
                lootEquipment.Add(lootItem);
            }
            else
            {
                lootItems.Add(lootItem);
            }
            // Fill Loot window loot items.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
