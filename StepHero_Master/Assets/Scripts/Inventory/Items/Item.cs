using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=OJsWnf8B-Zo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=49

public abstract class Item : ScriptableObject
{
    [SerializeField] private Sprite icon;
    public Sprite Icon
    {
        get { return Icon; }
    }

    [SerializeField] private int stackSize;
    public int StackSize
    {
        get { return stackSize; }
    }

    private SlotScript slot;
    protected SlotScript Slot
    {
        get { return slot; }
        set { slot = value; }
    }
}
