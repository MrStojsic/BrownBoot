using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Canvas_Inventory_TEMP.
// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public class InventoryScript : MonoBehaviour
{

    private static InventoryScript _instance;
    public static InventoryScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryScript>();
            }
            return _instance;
        }
    }

    [SerializeField] private BagButton[] bagButtons;

    // DEBUGGING ONLY DELETE LATER.
    [SerializeField] private Item[] items;

    private List<Bag> bags = new List<Bag>();

    //  NOT NEEDED.
    public bool CanAddBag
    {
        get { return bags.Count < 5; }
    }

    private void Awake()
    {
        /*
        // DEBUGGING ONLY DELETE LATER.
        Bag bag = (Bag)Instantiate(items[0], transform);
        bag.Initialize(16);
        bag.Use();
        bags.Add(bag);*/
    }

    // DEBUGGING ONLY DELETE LATER.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            bag.Use();
            bags.Add(bag);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealthPotion healthPotion = (HealthPotion)Instantiate(items[1]);
            AddItem(healthPotion);

        }
    }

    // Start is called before the first frame update
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.Bag == null)
            {
                bagButton.Bag = bag;
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.BagScript.AddItem(item))
            {
                return;
            }
        }
    }
}
