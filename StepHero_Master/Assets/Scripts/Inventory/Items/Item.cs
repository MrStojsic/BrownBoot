using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    FOOD,       // 0
    POTION,     // 1
    MATERIALS,  // 2
    MAIN_HAND,  // 3
    OFF_HAND,   // 4
    TWO_HAND,   // 5
    HELMET,     // 6
    SHOULDER,   // 7
    CHEST,      // 8
    LEGGING,    // 9
    BOOTS,      // 10
    GLOVES,     // 11
    NECKLACE,   // 12
    RING,       // 13
    ANY,        // 14
}

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

    /// <summary>
    /// The size of the stack, less than 2 is not stackable
    /// </summary>
     private int _stackSize = default;

    [SerializeField] private int _price = default;

    /// <summary>
    /// The item's rarity
    /// </summary>
    [SerializeField] private Rarity _rarity = default;


    /// <summary>
    /// Property for accessing the _title
    /// </summary>
    public string Title
    {
        get => _title;
    }

    /// <summary>
    /// Property for accessing the _icon
    /// </summary>
    public Sprite Icon
    {
        get => _icon;
    }

    public ItemType ItemType
    {
        get => _itemType;
    }
    
    /// <summary>
    /// Property for accessing the _stacksize
    /// </summary>
    public virtual int StackSize
    {
        get => _stackSize;
    }

    public int Price
    {
        get
        {
#if UNITY_EDITOR
            if (_price < 1)
            {
                Debug.LogError("Price CANNOT be 0g");
            }
#endif
            return _price;
        }
    }


    /// <summary>
    /// Property for accessing the _rarity
    /// </summary>
    public Rarity Rarity
    {
        get => _rarity;
    }

    [SerializeField] private string _longDescription = default;
    public string LongDescription
    {
        get { return _longDescription; }
    }
    [SerializeField] private string _shortDescription = default;
    public string ShortDescription
    {
        get { return _shortDescription; }
    }

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