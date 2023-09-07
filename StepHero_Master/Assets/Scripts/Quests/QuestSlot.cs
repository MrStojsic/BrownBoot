using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=cAiYebAR8Hk&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=100")]
public class QuestSlot : Slot
{
    private ISS_Quest _quest;
    public ISS_Quest Quest
    {
        get { return _quest; }
        set
        {	if (value != null)
            {
                _quest = value;
                _title.text = value.Title;
                _icon.sprite = value.Icon;
            }
            else
            {
                gameObject.SetActive(false);
                QuestLogUiManager.Instance.PoolSlot(this);
			}
		}
	}

    public void Initialise(ISS_Quest quest, int index)
    {
        if (quest != null)
        {
            Quest = quest;
            Index = index;
		}
	}

    private bool isMarkedCompleted = false;

    public void IsComplete()
    {
        if (!isMarkedCompleted && Quest.IsComplete)
        {
            print("Completed Quest");
            // TODO - Add process to mark complated quest and handle next actions.
            isMarkedCompleted = true;
        }
        else if (!Quest.IsComplete)
        {
            print("UN-Completed Quest");
            // TODO - Add process to unmark complated quest.
            isMarkedCompleted = false;
        }
    }
}
