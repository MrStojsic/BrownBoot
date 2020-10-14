using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthPotion : Item, IUseable
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
}
