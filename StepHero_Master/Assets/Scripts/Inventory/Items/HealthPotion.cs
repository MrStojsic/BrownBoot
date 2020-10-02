using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField] private int health = default;

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
        return string.Format("Restores {0} health when used.", health);
    }
}
