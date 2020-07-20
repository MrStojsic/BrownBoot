using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum for declaring the Rarity of the item
/// </summary>
public enum Rarity { Common, Uncommon, Rare, Legendary }

public static class RarityColours
{
    private static Dictionary<Rarity, string> _colors = new Dictionary<Rarity, string>()
    {
        {Rarity.Common, "#ffffffff" },
        {Rarity.Uncommon, "#00D2ED" },
        {Rarity.Rare, "#B94BDF" },
        {Rarity.Legendary, "#FFA700" },

    };

    public static Dictionary<Rarity, string> Colors
    {
        get
        {
            return _colors;
        }
    }
}