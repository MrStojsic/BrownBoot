using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sweet Wotato", menuName = "Items/Wotato", order = 1)]
public class Wotato : Item, IUseable
{
    [SerializeField] private int health = default;

    public bool Use()
    {

        if (Player.Instance.TestStat.MyCurrentValue < Player.Instance.TestStat.MyMaxValue)
        {
            Remove();

            Player.Instance.TestStat.MyCurrentValue += health;
            return true;
        }
        return false;
    }
    public override string GetShortDescription()
    {
        return string.Format("Restores {0} health when eaten.", health);
    }
    public override string GetLongDescription()
    {
        return string.Format("This weird looking root vegetable grows underground in dry deset regions, it's spikey skin deters animals from eating its sugary sweet flesh.", health);
    }
}