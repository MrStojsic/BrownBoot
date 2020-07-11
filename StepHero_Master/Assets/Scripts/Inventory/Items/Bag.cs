using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bags" , order = 1)]
public class Bag : Item, IUseable
{
    private int _numberOfSlots;

    public int _NumberOfSlots
    {
        get { return _numberOfSlots; }
    }



    [SerializeField] private GameObject bagPrefab;

    public BagScript BagScript { get; set; }


    public void Initialize(int numberOfSlots)
    {
        this._numberOfSlots = numberOfSlots;
    }

    public void Use()
    {
        if (InventoryScript.Instance.CanAddBag)
        {
            Remove();

            BagScript = Instantiate(bagPrefab, InventoryScript.Instance.transform).GetComponent<BagScript>();
            BagScript.AddSlots(_numberOfSlots);

            InventoryScript.Instance.AddBag(this);
        }
    }
}
