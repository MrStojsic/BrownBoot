using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wotato", menuName = "Items/Wotato", order = 1)]
public class Wotato : Item, IUseable
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
        return string.Format("This weird looking root vegetable grows underground in dry deset regions, it's spikey skin deters animals from eating its sugary sweet flesh. Restores {0} health when eaten.", health);
    }
}