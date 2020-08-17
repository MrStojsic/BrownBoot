using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LootSlot : MonoBehaviour
{

    [SerializeField] private Item _item;
    public Item Item { get { return _item; } set { _item = value; } }

    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;

    // TODO [SerializeField] private int _quantity;

    public void SetLootSlot()
    {
        _icon.sprite = _item.Icon;

        // Set loot slot title.
        string title = string.Format("<color={0}>{1}</color>", RarityColours.Colors[_item.Rarity], _item.Title);
        _title.text = title;
    }



}
