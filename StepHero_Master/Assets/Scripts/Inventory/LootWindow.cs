using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=T6ZjSpy3JrI&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=73

public class LootWindow : MonoBehaviour
{
    [SerializeField] private SelectorGroup lootSelectorGroup;

    [SerializeField] List<LootSlot> lootSlots = new List<LootSlot>();

    // DEBUGGING ONLY.
    [SerializeField] List<Item> debugLootItems = new List<Item>();

    [SerializeField] List<Item> lootItems = new List<Item>();


    [SerializeField] private Text infoTitle;
    [SerializeField] private Image infoIcon;
    [SerializeField] private Text infoDesciption;
    [SerializeField] private Text infoItemInventoryCount;


    // Start is called before the first frame update
    void Start()
    {
        AddLoot(debugLootItems);

        PopulateLootList();
    }

    public void AddLoot(List<Item> lootToAdd)
    {
        lootItems.AddRange(lootToAdd);
    }

    private void PopulateLootList()
    {
        for (int i = 0; i < lootSlots.Count; i++)
        {
            if (i < lootItems.Count)
            {
                lootSlots[i].Item = lootItems[i];
                // Set loot slot.
                lootSlots[i].SetLootSlot();

                // Enable loot slot.
                lootSlots[i].gameObject.SetActive(true);
            }
            else
            {
                // Enable loot slot.
                lootSlots[i].gameObject.SetActive(false);
            }
        }

        lootSelectorGroup.SelectSelectorViaIndex(0);
    }

    public void SetItemInfo(LootSlot lootSlot)
    {
        // Set info icons.
        infoIcon.sprite = lootSlot.Item.Icon;

        // Set info title.
        string title = string.Format("<color={0}>{1}</color>", RarityColours.Colors[lootSlot.Item.Rarity], lootSlot.Item.Title);
        infoTitle.text = title;

        // Set info description.
        infoDesciption.text = lootSlot.Item.GetDescription();

        // Set item inventory count.
        infoItemInventoryCount.text = "TODO";// TODO.


        // TODO.
        // If inventory cant accept more of this item, grey it out on the list.
        // and only enable drop button
    }

    public void TakeItem()
    {
        if (InventoryScript.Instance.AddItem(lootItems[lootSelectorGroup.selectedIndex]))
        {
            lootSlots[lootSelectorGroup.selectedIndex].Item = null;
            lootSlots[lootSelectorGroup.selectedIndex].gameObject.SetActive(false);
        }
    }

    public void TakeAllItems()
    {
        // NEEDS LOOKING OVER IN CASE INVENTORY FULL.
        // TODO.
        for (int i = 0; i < lootItems.Count; i++)
        {
            InventoryScript.Instance.AddItem(lootItems[i]);
            lootSlots.RemoveAt(i);
            // Disable loot slot.
            lootSlots[i].gameObject.SetActive(true);
        }

    }

    private void SetSelectedAfterTake()
    {
        // TODO.
        // set the selected item to the to pmost active item in the list.

        // if only items that are inactive and greyed out select the top most greyed item.

        // if no more items in the list close the window.
    }


}
