using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeatherGloves", menuName = "Equipment/LeatherGloves", order = 1)]
public class LeatherGloves : Equipment
{
    public override string GetShortDescription()
    {
        return string.Format("A pair of leather gloves with a silky linen lining.");
    }
    public override string GetLongDescription()
    {
        return string.Format("A pair of soft leather gloves, typically worn by the wealthy citizens, they still have their new leather smell.");
    }
}
