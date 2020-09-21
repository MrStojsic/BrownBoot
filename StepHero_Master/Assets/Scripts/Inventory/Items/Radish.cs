using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radish", menuName = "Items/Radish", order = 1)]
public class Radish : Item, IUseable
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
        return string.Format("A spicy radish which restores {0} health when used.", health);
    }
}