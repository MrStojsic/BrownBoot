using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    [SerializeField] private int intelegence = default;
    [SerializeField] private int strength = default;
    [SerializeField] private int agility = default;

    public override string GetShortDescription()
    {
        string stats = string.Empty;

        if (strength > 0)
        {
            stats += string.Format("\n +{STR}", strength);
        }
        if (agility > 0)
        {
            stats += string.Format("\n +{AGI}", agility);
        }
        if (intelegence > 0)
        {
            stats += string.Format("\n +{INT}", intelegence);
        }

        return base.GetShortDescription() + stats;
    }
}
