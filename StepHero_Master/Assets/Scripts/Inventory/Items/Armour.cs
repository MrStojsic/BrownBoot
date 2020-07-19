using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmourType
{
    HELMET,
    CHEST,
    GLOVES,
    LEGGING,
    BOOTS
}

[CreateAssetMenu(fileName = "Armour", menuName = "Items/Armour", order = 2)]
public class Armour : Item
{
    [SerializeField] private ArmourType armourType;

    [SerializeField] private int intelegence;
    [SerializeField] private int strength;
    [SerializeField] private int agility;

    public override string GetDescription()
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

        return base.GetDescription() + stats;
    }
}
