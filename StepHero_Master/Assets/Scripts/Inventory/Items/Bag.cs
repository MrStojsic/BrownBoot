using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bags" , order = 1)]
public class Bag : Item, IUseable
{
    private int numberOfSlots;

    public int NumberOfSlots
    {
        get { return numberOfSlots; }
    }



    [SerializeField] private GameObject bagPrefab;

    public BagScript BagScript { get; set; }


    public void Initialize(int numberOfSlots)
    {
        this.numberOfSlots = numberOfSlots;
    }

    public void Use()
    {
        BagScript = Instantiate(bagPrefab, InventoryScript.Instance.transform).GetComponent<BagScript>();
        BagScript.AddSlots(numberOfSlots);
    }
}
