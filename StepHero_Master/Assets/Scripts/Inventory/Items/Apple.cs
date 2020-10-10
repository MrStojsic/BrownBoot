using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Apple", menuName = "Items/Apple", order = 1)]
public class Apple : Item, IUseable
{
    [SerializeField] private int health = default;

    public bool Use()
    {

        if (Player.Instance.TestStat.MyCurrentValue < Player.Instance.TestStat.MyMaxValue)
        {
            Player.Instance.TestStat.MyCurrentValue += health;
            Debug.Log(Player.Instance.TestStat.MyCurrentValue);
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
        return string.Format("A rosey red apple with a waxy shine, no signs of worms. These are native to the southern regions and harvested all year round.", health);
    }

}
