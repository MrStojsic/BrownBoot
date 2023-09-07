using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSlot : Slot
{
    [SerializeField] private Sprite currentObjectiveIcon;
    [SerializeField] private Color currentObjectiveColour;
    [SerializeField] private Sprite completedObjectiveIcon;
    [SerializeField] private Color completedObjectiveColour;

    public void SetObjectiveDetails(Objective o)
    {
        if (o.IsComplete == true)
        {
            Icon.sprite = completedObjectiveIcon;
            Icon.color = completedObjectiveColour;
        }
        else
		{
            Icon.sprite = currentObjectiveIcon;
            Icon.color = currentObjectiveColour;
        }
        Title.text = o.Description + " " + o.CurrentAmount + "/" + o.Amount;
    }
}
