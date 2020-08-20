using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// https://www.youtube.com/watch?v=T6ZjSpy3JrI&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=73

public class LootWindow : MonoBehaviour
{
    [SerializeField] private SelectorGroup itemSelectorGroup;

    

    [SerializeField] List<ItemSelectorButton> itemSelectorButtons = new List<ItemSelectorButton>();

    // DEBUGGING ONLY.
    [SerializeField] List<Item> debugLootItems = new List<Item>();

    [SerializeField] List<Item> lootItems = new List<Item>();


    [SerializeField] private Text infoTitle;
    [SerializeField] private Image infoIcon;
    [SerializeField] private Text infoDesciption;
    [SerializeField] private Text infoItemInventoryCount;

    [SerializeField] private GameObject itemEntryPrefab;


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
        for (int i = 0; i < lootItems.Count; i++)
        {
            itemSelectorButtons.Add(Instantiate(itemEntryPrefab, itemSelectorGroup.transform).GetComponent<ItemSelectorButton>());
            itemSelectorButtons[i].SetupButtonDisplay(lootItems[i]);
            int index = i;
            itemSelectorButtons[i].SetupButtonFunction(itemSelectorGroup, ()=>SetItemInfo(itemSelectorButtons[index]));
        }

        itemSelectorGroup.SelectSelectorViaIndex(0);
    }

    public void SetItemInfo(ItemSelectorButton itemSelectorButton)
    {
        // Set info icons.
        infoIcon.sprite = itemSelectorButton.Item.Icon;

        // Set info title.
        string title = string.Format("<color={0}>{1}</color>", RarityColours.Colors[itemSelectorButton.Item.Rarity], itemSelectorButton.Item.Title);
        infoTitle.text = title;

        // Set info description.
        infoDesciption.text = itemSelectorButton.Item.GetDescription();

        // Set item inventory count.
        infoItemInventoryCount.text = "TODO";// TODO.


        // TODO.
        // If inventory cant accept more of this item, grey it out on the list.
        // and only enable drop button
    }

    public void TakeItem()
    {
        if (InventoryScript.Instance.AddItem(lootItems[itemSelectorGroup.selectedIndex]))
        {
            itemSelectorButtons[itemSelectorGroup.selectedIndex].gameObject.SetActive(false);
        }
    }

    public void TakeAllItems()
    {
        // NEEDS LOOKING OVER IN CASE INVENTORY FULL.
        // aslo needs rejigging as we shouldt remove the button refereence.
        // TODO.
        for (int i = 0; i < lootItems.Count; i++)
        {
            InventoryScript.Instance.AddItem(lootItems[i]);
            itemSelectorButtons.RemoveAt(i);
            // Disable loot slot.
            itemSelectorButtons[i].gameObject.SetActive(true);
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
