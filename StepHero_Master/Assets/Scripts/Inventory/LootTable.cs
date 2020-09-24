using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField] private Loot[] possibleLoot = default;

    private List<Item> droppedItems = new List<Item>();

    public void AttemptLoot()
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

        for (int i = 0; i < possibleLoot.Length; i++)
        {
            int rollOutcome = Random.Range(0, 100);
            if (rollOutcome <= possibleLoot[i].DropChance)
            {
                droppedItems.Add(possibleLoot[i].Item);
            }
        }
    }
}
