using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion",order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField] private int health;

    public void Use()
    {
        if (Player.Instance.TestStat.MyCurrentValue < Player.Instance.TestStat.MyMaxValue)
        {
            Remove();

            Player.Instance.TestStat.MyCurrentValue += health;
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\nRestores {0} health when used.", health);
    }
}
