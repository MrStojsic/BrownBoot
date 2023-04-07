using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npcs;

[HelpURL("https://www.youtube.com/watch?v=EWJSu1kdIEc")] // Based off it.
public class QuestGiver : Npc
{
    //- Keeping assigned quest and helped outside of quest to keep NPC and quest modules sperate,
    //  this way the NPC jusy needs to know it was helped, not all the details of the quest at this point.
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    [SerializeField] private GameObject quests;
    // ALTERNATIVE
    //- This is the name of the script that is the quest we are after,
    //  so this relighs on each quest being its own unique script rather than a scriptable obj.
    //  May change later
    [SerializeField] private string questType;

    //- This is a reference to the quaet this NPC has assigned, so it can check the progress of the quest later.
    private Quest Quest { get; set; }



    // TODO
    //- IF we go ahead with this quest structure the video covers more detail on dialog etc,
    //  but we dont have a dialog system yet (he covers one in his other tutorials).
    //- He hasnt covered saving quests or their progress yet either (check other tutorials to see if he ever covers saving quests or merge with inscopes).
    public override void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            //- In this setup. the questGivers default dialogue would be his asking you to do the quest.
            base.Interact();
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            //DialogueStstem.Instance.AddNewDialogue(new string[] { "Thanks for helping me with that quest that onne time.", name );

        }
    }

    void AssignQuest()
    {
        //- We take the name of the script ref in questType string, and add that as a component to this questGiver.
        Quest = (Quest)quests.AddComponent( System.Type.GetType(questType) );
        AssignedQuest = true;
    }
    void CheckQuest()
    {
        if (Quest.IsCompleted)
        {
            Helped = Quest.GiveReward();
            AssignedQuest = false;
            //DialogueStstem.Instance.AddNewDialogue(new string[] { "Thanks, heres your reward.", "Take care.", name );
        }
        else
        {
            //DialogueStstem.Instance.AddNewDialogue(new string[] { "You're still in the middle of my quest, come back when you're finished.", name );

        }
    }
}
