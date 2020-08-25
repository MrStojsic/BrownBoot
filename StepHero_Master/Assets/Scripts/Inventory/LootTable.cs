using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField] private Loot[] possibleLoot;

    private List<Item> droppedItems = new List<Item>();

    public void OfferLoot()
    {
        RollLoot();
       
        if (droppedItems.Count > 0)
        {
            LootWindow.Instance.AddLoot(droppedItems);
            LootWindow.Instance.PopulateLootList();
        }
    }

    private void RollLoot()
    {
        if (droppedItems.Count > 0)
        {
            droppedItems.Clear();
        }
        foreach (Loot item in possibleLoot)
        {
            int rollOutcome = Random.Range(0, 100);
            if (rollOutcome <= item.DropChance)
            {
                droppedItems.Add(item.Item);
            }
        }
    }
}
