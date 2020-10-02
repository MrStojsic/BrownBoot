using System.Collections;
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
    private InventoryInteractionManager IIM = null;


    [SerializeField]
    public InventoryTypePocket[] inventoryTypePockets = new InventoryTypePocket[15];

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
        //AddLoot(debugLootItems);
        //IIM.InitialiseInventorySlots(inventoryTypePockets, InventoryInteractionManager.InventoryType.LOOT );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
