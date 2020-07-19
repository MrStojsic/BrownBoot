﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

/// <summary>
/// Superclass for all items
/// </summary>
public abstract class Item : ScriptableObject, IMoveable
{
    /// <summary>
    /// The icon used to display this item, also used when moving and placing the items
    /// </summary>
    [SerializeField]
    private Sprite _icon;

    /// <summary>
    /// The size of the stack, less than 2 is not stackable
    /// </summary>
    [SerializeField]
    private int _stackSize;

    /// <summary>
    /// The item's title
    /// </summary>
    [SerializeField]
    private string _title;

    /// <summary>
    /// The item's rarity
    /// </summary>
    [SerializeField] private Rarity _rarity;

    /// <summary>
    /// A reference to the slot that this item is sitting on
    /// </summary>
    private SlotScript _slot;


    [SerializeField]
    private int price;


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

    public string Title
    {
        get
        {
            return _title;
        }
    }

    public int MyPrice
    {
        get
        {
            return price;
        }
    }
    
    /// <summary>
    /// Returns a description of this specific item
    /// </summary>
    /// <returns></returns>
    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", RarityColours.Colors[Rarity], Title);
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

