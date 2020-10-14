using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radish", menuName = "Items/Radish", order = 1)]
public class Radish : Item, IUseable
{
    [SerializeField] private int health = default;

    public bool Use()
    {

        if (Player.Instance.TestStat.MyCurrentValue < Player.Instance.TestStat.MyMaxValue)
        {
            Player.Instance.TestStat.MyCurrentValue += health;
            return true;
        }
        return false;
    }
    public override string GetShortDescription()
    {
        return string.Format("Restores {0} health when used.", health);
    }
    public override string GetLongDescription()
    {
        return string.Format("A spicy young radish, the spiciness intensifies as they mature so they are usually picked quite early.", health);
    }
}