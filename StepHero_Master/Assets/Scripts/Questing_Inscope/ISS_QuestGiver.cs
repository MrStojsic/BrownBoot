using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]

public class ISS_QuestGiver : MonoBehaviour
{
    [SerializeField]
    private ISS_Quest[] _quests = default;
    public ISS_Quest[] Quests { get { return _quests; } }

    [SerializeField]
    private int _questGiverId;
    public int QuestGiverId { get { return _questGiverId; } }

    private List<string> _completedQuests = new List<string>();
	public List<string> CompletedQuests
	{
		get { return _completedQuests; }
		set
		{
            _completedQuests = value;

			foreach (string questTitle in _completedQuests)
			{
				for (int i = 0; i < _quests.Length; i++)
				{
                    if (_quests[i] != null && _quests[i].Title == questTitle)
                    {
                        _quests[i] = null; 
					}
				}
			}
		}

	}


    private void Start()
	{
		foreach (ISS_Quest quest in _quests)
		{
            quest.questGiver = this;
		}
	}

	public void UpdateQuestStatus()
	{
        int count = 0;
		foreach (ISS_Quest quest in _quests)
		{
            if (quest != null)
            {
                if (quest.IsComplete && ISS_QuestLog.Instance.HasQuest(quest))
                {
                    // - A quest is complete and can be rewarded.
                    break;
                }
                else if (!ISS_QuestLog.Instance.HasQuest(quest))
                {
					// - A quest is available.
                    break;
                }
                else if (!quest.IsComplete && ISS_QuestLog.Instance.HasQuest(quest))
                {
                    // - A quest is active.
                }
            }
            else
            {
                count++;

                if (count == _quests.Length)
                {
					//- No quests availble at all.
                }
            }
        }

    }

    // DEBUG ONLY
    [SerializeField]
    private ISS_QuestLog tempLog = null;

    //HACK < HERE
    // here we need to accept a quest.
    //DEBUGGING ONLY.
    public void AcceptQuest0()
    {
        tempLog.AcceptQuest(_quests[0]);
    }
    public void AcceptQuest1()
    {
        tempLog.AcceptQuest(_quests[1]);
    }
    // < TO HERE.


}
