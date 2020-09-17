using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

/// <summary>
/// Superclass for all items
/// </summary>
public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    /// <summary>
    /// The icon used to display this item, also used when moving and placing the items
    /// </summary>
    [SerializeField]
    private Sprite _icon = default;

    /// <summary>
    /// The size of the stack, less than 2 is not stackable
    /// </summary>
    [SerializeField]
    private int _stackSize = default;

    /// <summary>
    /// The item's title
    /// </summary>
    [SerializeField]
    private string _title = default;

    /// <summary>
    /// Property for accessing the _title
    /// </summary>
    public string Title
    {
        get
        {
            return _title;
        }
    }

    private string description = default;
    public string Description
    {
        get { return description; }
    }

    /// <summary>
    /// The item's rarity
    /// </summary>
    [SerializeField] private Rarity _rarity = default;

    /// <summary>
    /// A reference to the slot that this item is sitting on.
    /// Now obsolete as we no longer use it, only keeping it as a reference to show how scriptables objects acan still store other world objects.
    /// </summary>
    [SerializeField]
    private SlotScript _slot = default;

    


    [SerializeField] private int _price = default;


    /// <summary>
    /// Property for accessing the _icon
    /// </summary>
    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }

    /// <summary>
    /// Property for accessing the _stacksize
    /// </summary>
    public int StackSize
    {
        get
        {
            return _stackSize;
        }
    }

    /// <summary>
    /// Property for accessing the _slotscript
    /// </summary>
    
    public SlotScript Slot
    {
        get
        {
            return _slot;
        }

        set
        {
            _slot = value;
        }
    }

    /// <summary>
    /// Property for accessing the _rarity
    /// </summary>
    public Rarity Rarity
    {
        get
        {
            return _rarity;
        }
    }

    public int Price
    {
        get
        {
            return _price;
        }
    }
    
    /// <summary>
    /// Returns a description of this specific item
    /// </summary>
    /// <returns></returns>
    public virtual string GetDescription()
    {
        return string.Format("This {0} is currently a placeholder and has thus no description has been set.", _title); ;
    }

    public virtual string GetTitle()
    {
        return string.Format("<color={0}>{1}</color>", RarityColours.Colors[Rarity], _title);
    }

    /// <summary>
    /// Removes this item from the inventory
    /// </summary>
    public void Remove()
    {
        if (Slot != null)
        {
            // this is the same as if(slot != null) {Slot.RemoveItem(this);}
            Slot?.RemoveItem(this);
        }
    }
}


