using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class ItemSelectorButton : SelectorButton
{
    // TODO.
    // change loot slot to be more of a ObtainableIttemSlot,
    // it stores an item but is also a type of button that simply sets its own ui to match the item
    // it should be able to check how many of its item the player has to tell what actions is can do.
    // design it to be reusable in shops, loot windows and item collection situations such as rewards for quests.

    [SerializeField] private Item _item;
    public Item Item { get { return _item; } set { _item = value; } }

    [SerializeField] private Image _icon = default;
    [SerializeField] private Text _title = default;

    public void SetupButtonDisplay(Item item)
    {
        _item = item;

        _icon.sprite = _item.Icon;

        // Set loot slot title.
        string title = string.Format("<color={0}>{1}</color>", RarityColours.Colors[_item.Rarity], _item.Title);
        _title.text = title;
    }

    public void SetupButtonFunction(SelectorGroup selectorGroup, UnityAction action)
    {
        this.selectorGroup = selectorGroup;
        this.AddListenerActionToOnSelected(action);
    }

    public void ResetForUse()
    {
        onSelected.RemoveAllListeners();
        gameObject.SetActive(true);
    }

}

