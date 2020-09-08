using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField] private Item _item = default;

    [SerializeField] private float _dropChance = default;

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
