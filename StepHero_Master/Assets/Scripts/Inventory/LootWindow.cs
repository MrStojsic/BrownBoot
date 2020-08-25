using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// https://www.youtube.com/watch?v=T6ZjSpy3JrI&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=73

public class LootWindow : MonoBehaviour
{
    private static LootWindow _instance;
    public static LootWindow Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LootWindow>();
            }
            return _instance;
        }
    }


    [SerializeField] private SelectorGroup itemSelectorGroup;

    [SerializeField] List<ItemSelectorButton> itemSelectorButtons = new List<ItemSelectorButton>();


    [SerializeField] List<Item> lootItems = new List<Item>();

    // TODO: If we end up having all enemies loot divided up by one loot table in future we can pass the dropped loot list into a function on lootwindow,
    //       set lootItems = passedDropList of items and if we remove items from lootItems it will remove it from the original passedDropList from the lootTable.
    //       this is because settign a list to = another list is a refercne to that list, they are actually refering to the same data now.
    //       so we can use this idea later if item duplication becomes an issue.
    // https://youtu.be/V8MoOaYyA5g?t=1639


    [SerializeField] private Text infoTitle;
    [SerializeField] private Image infoIcon;
    [SerializeField] private Text infoDesciption;
    [SerializeField] private Text infoItemInventoryCount;

    [SerializeField] private ItemSelectorButton itemEntryPrefab;

    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void AddLoot(List<Item> lootToAdd)
    {
        lootItems.AddRange(lootToAdd);
    }

    public void PopulateLootList()
    {
        if(itemSelectorButtons.Count >0)
        {
            itemSelectorButtons.Clear();
        }
       
        int index = 0;
        if (itemSelectorGroup.transform.childCount > 0)
        {
            
            for (; index < lootItems.Count && index < itemSelectorGroup.transform.childCount; index++)
            {
                itemSelectorButtons.Add(itemSelectorGroup.transform.GetChild(index).GetComponent<ItemSelectorButton>());
                itemSelectorButtons[index].SetupButtonDisplay(lootItems[index]);
                itemSelectorButtons[index].ResetForUse();
                int indexCopy = index;
                itemSelectorButtons[index].SetupButtonFunction(itemSelectorGroup, () => SetItemInfo(itemSelectorButtons[indexCopy]));

            }
        }
        if (itemSelectorButtons.Count < lootItems.Count)
        {
            for (; index < lootItems.Count; index++)
            {
                itemSelectorButtons.Add(Instantiate(itemEntryPrefab, itemSelectorGroup.transform) as ItemSelectorButton);
                itemSelectorButtons[index].SetupButtonDisplay(lootItems[index]);
                int indexCopy = index;
                itemSelectorButtons[index].SetupButtonFunction(itemSelectorGroup, () => SetItemInfo(itemSelectorButtons[indexCopy]));
            }
        }
        
        if (itemSelectorGroup.transform.childCount > lootItems.Count)
        {
            for (; index < itemSelectorGroup.transform.childCount; index++)
            {
                itemSelectorGroup.transform.GetChild(index).gameObject.SetActive(false);
            }
        }
       // print(index);
        itemSelectorGroup.SelectSelectorViaIndex(0);

        OpenClose();
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
        if (itemSelectorButtons[itemSelectorGroup.selectedIndex].gameObject.activeInHierarchy == true &&
            InventoryScript.Instance.AddItem(lootItems[itemSelectorGroup.selectedIndex]))
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

    public void OpenClose()
    {
        if (canvasGroup.alpha == 0)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            return;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        lootItems.Clear();
    }

}
