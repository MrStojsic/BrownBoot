using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField] private Item _item;

    [SerializeField] private float _dropChance;

    public Item Item
    {
        get
        {
            return _item;
        }
    }

    public float DropChance
    {
        get
        {
            return _dropChance;
        }
    }
}
