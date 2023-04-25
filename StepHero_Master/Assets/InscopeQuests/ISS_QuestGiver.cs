using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]

public class ISS_QuestGiver : MonoBehaviour
{
    [SerializeField]
    private ISS_Quest[] quests;

    // DEBUG ONLY
    [SerializeField]
    private ISS_QuestLog tempLog;

    private void Awake()
    {
        // here we need to accept a quest.
        //DEBUGGING ONLY.
        tempLog.AcceptQuest(quests[0]);
    }

    private void Update()
    {
        
    }
}
