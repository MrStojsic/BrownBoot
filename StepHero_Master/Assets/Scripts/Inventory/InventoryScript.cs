using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Canvas_Inventory_TEMP.
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class InventoryScript : MonoBehaviour
{

    private static InventoryScript instance;
    public static InventoryScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    // DEBUGGING ONLY DELETE LATER.
    [SerializeField] private Item[] items;

    private void Awake()
    {
        // DEBUGGING ONLY DELETE LATER.
        Bag bag = (Bag)Instantiate(items[0], transform);
        bag.Initialize(16);
        bag.Use();
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
