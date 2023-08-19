using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSlot : Slot
{
    public void SetObjectiveDetails(Objective o, Sprite tickBox = null)
    {
        if (tickBox != null)
        {
            Icon.sprite = tickBox;
        }
        Title.text = o.Description + " " + o.CurrentAmount + "/" + o.Amount;
    }
}
