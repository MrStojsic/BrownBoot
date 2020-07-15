using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public abstract class Item : ScriptableObject, IMoveable
{
    [SerializeField] private Sprite _icon;
    public Sprite Icon
    {
        get { return _icon; }
    }

    [SerializeField] private int _stackSize;
    public int StackSize
    {
        get { return _stackSize; }
    }

    private SlotScript _slot;
    public SlotScript Slot
    {
        get { return _slot; }
        set { _slot = value; }
    }

    public void Remove()
    {
        // this is the same as if(slot != null) {Slot.RemoveItem(this);}
        Slot?.RemoveItem(this);
    }
}
