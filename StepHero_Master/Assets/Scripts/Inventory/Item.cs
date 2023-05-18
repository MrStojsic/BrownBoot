using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    FOOD,       // 0
    POTION,     // 1
    MATERIALS,  // 2
    TWO_HAND,   // 3
    ONE_HAND,   // 4
    SHIELD,     // 5
    THROWABLE,  // 6
    HELMET,     // 7
    SHOULDER,   // 8
    CHEST,      // 9
    LEGGING,    // 10
    BOOTS,      // 11
    GLOVES,     // 12
    NECKLACE,   // 13
    RING,       // 14
    ANY,        // 15
}

/*
 * // TODO: Add item ID so searches and comparisons of items is faster.
//- If we decide to go with itemType plus Item ID to make each item id (so we can work out an items type based on its ID
//  EG. returned ID 1990 would be item type POTION + id 990 tellings us its a POTION of item type 990.)
//  to calculate the item type from he final 1990 use the below equation.

         int[] ints = new int[] {0991, 1490, 2454, 3999,4001,5808,10490,14890 };

        foreach (int i in ints)
        {
            int itemType = (i - i % 1000) / 1000;
            print(itemType + "\n");
        }

     */



// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public abstract class Item : ScriptableObject, IDescribable
{
    /// <summary>
    /// The item's title
    /// </summary>
    [SerializeField]
    private string _title = default;

    /// <summary>
    /// The icon used to display this item, also used when moving and placing the items
    /// </summary>
    [SerializeField]
    private Sprite _icon = default;

    [SerializeField]
    private ItemType _itemType = default;

    [SerializeField] private int _price = default;

    /// <summary>
    /// The item's rarity
    /// </summary>
    [SerializeField] private Rarity _rarity = default;

    /// <summary>
    /// The centre point of the items origin, this will likely be used to adjust the price of some items based on how far its bought or sold from its native region.
    /// </summary>
    [SerializeField] private Vector2 _regionOfOrigin = default;

    /// <summary>
    /// The size of the stack, less than 2 is not stackable
    /// </summary>
    private int stackSize = 99;

    [TextArea] [SerializeField] private string _shortDescription = default;
    [TextArea] [SerializeField] private string _longDescription = default;

    /// <summary>
    /// Property for accessing the _title
    /// </summary>
    public string Title
    {   get => _title;  }

    /// <summary>
    /// Property for accessing the _icon
    /// </summary>
    public Sprite Icon
    {   get => _icon;   }

    public ItemType ItemType
    {   get => _itemType;  }

    public int Price
    {
        get
        {
#if UNITY_EDITOR
            if (_price < 1)
            {
                Debug.LogError(Title + " price has not been set and CANNOT be 0g");
            }
#endif
            return _price;
        }
    }


    /// <summary>
    /// Property for accessing the _rarity
    /// </summary>
    public Rarity Rarity
    {   get => _rarity;   }

    public Vector2 RegionOfOrigin
    { get => _regionOfOrigin; }

    public virtual int StackSize
    { get => stackSize; }

    public string LongDescription
    {   get => _longDescription;    }

    public string ShortDescription
    {   get => _shortDescription;   }



    /// <summary>
    /// Returns a description of this specific item
    /// </summary>
    /// <returns></returns>
    public virtual string GetShortDescription()
    {
        return string.Format("This {0} is currently using a placeholder SHORT description as no description has been set.", _title); ;
    }

    public virtual string GetLongDescription()
    {
        return string.Format("This {0} is currently using a placeholder LONG description as no description has been set.", _title); ;
    }

    public virtual string GetTitle()
    {
        return string.Format("<color={0}>{1}</color>", RarityColours.Colors[Rarity], Title);
    }

}