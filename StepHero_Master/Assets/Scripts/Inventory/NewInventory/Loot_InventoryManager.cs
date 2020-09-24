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


    [SerializeField]
    public List<InventoryItem> inventoryPockets = new List<InventoryItem>();

    [SerializeField] private List<InventoryItem> debugLootItems = new List<InventoryItem>();

    public void AddLoot(List<InventoryItem> lootToAdd)
    {
        for (int i = 0; i < lootToAdd.Count; i++)
        {
            inventoryPockets.Add(lootToAdd[i]);
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
