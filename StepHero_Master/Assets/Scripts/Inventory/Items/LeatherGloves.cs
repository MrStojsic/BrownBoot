using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeatherGloves", menuName = "Equipment/LeatherGloves", order = 1)]
public class LeatherGloves : Equipment
{
    public override string GetDescription()
    {
        return string.Format("A pair of soft leather gloves, they still have that new leather smell.");
    }
}
