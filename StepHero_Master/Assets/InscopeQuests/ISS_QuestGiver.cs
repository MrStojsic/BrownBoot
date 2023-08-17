using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]

public class ISS_QuestGiver : MonoBehaviour
{
    [SerializeField]
    private ISS_Quest[] _quests;

    public ISS_Quest[] Quests { get { return _quests; } }


    // DEBUG ONLY
    [SerializeField]
    private ISS_QuestLog tempLog = null;



    //HACK < HERE
    // here we need to accept a quest.
    //DEBUGGING ONLY.
    public void AcceptQuest0()
    {
        tempLog.AcceptQuest(Quests[0]);
    }
    public void AcceptQuest1()
    {
        tempLog.AcceptQuest(Quests[1]);
    }
    // < TO HERE.
}
