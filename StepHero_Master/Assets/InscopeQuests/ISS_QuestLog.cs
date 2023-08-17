using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]
// Added Objectives in https://www.youtube.com/watch?v=FLXzG-bGHOA&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=101
public class ISS_QuestLog : UiWindow
{
    private static ISS_QuestLog _instance;
    public static ISS_QuestLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ISS_QuestLog>();
            }
            return _instance;
        }
    }

    [SerializeField] private ISS_Quest selectedQuest;

    [SerializeField]
    private ISS_QuestScript questPrefeb = null;
    [SerializeField]
    private SelectorGroup _questSlotSelectorGroup = default;


    private List<ISS_QuestScript> questScripts = new List<ISS_QuestScript>();

    private List<ISS_Quest> _AcceptedQuests = new List<ISS_Quest>();

    public List<ISS_Quest> AcceptedQuests
    {
        get { return _AcceptedQuests; }
        set { _AcceptedQuests = value; }
        }

    // HACK
    public Text descriptionText;
    public Text titleText;
    // <

    public void AcceptQuest(ISS_Quest quest)
    {
        // HACK - This is just so we cant pick up the same quest twice for now,
        //        will make a better fix fo this later once quests are finalised.
        if (HasQuest(quest))
        {
            return;
        }
        // <

        foreach (CollectionObjective o in quest.CollectionObjectives)
        {
            PlayerInventory.Instance.itemCountChanged += new ItemCountChanged(o.UpdateCollectedItemCount);
            o.UpdateCollectedItemCount();
        }

        ISS_QuestScript newMenuQuest;
        newMenuQuest = Instantiate(questPrefeb, _questSlotSelectorGroup.selectorButtonsParent);
        newMenuQuest.SelectorButton.selectorGroup = _questSlotSelectorGroup;

        newMenuQuest.Quest = quest;

        AcceptedQuests.Add(quest);
        questScripts.Add(newMenuQuest);

        // HACK
        newMenuQuest.SelectorButton.AddListenerActionToOnSelected(() => ShowDescription(newMenuQuest.Quest));
        // <


        //HACK
        newMenuQuest.GetComponentInChildren<Text>().text = quest.Title;
        // <

        //- Check if any of the tasks have bee n completed already.
        CheckCompletion();

    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Pressed Q - Clost the QuestLog.");
            Hide();
        }
    }

    public void AbandonQuest()
    {
        //- Removes quest from the quest log.
        //- Remember if we go with dialoge based quests
        //  this needs to undo that dialoge making the quest availible again.
    }

    // HACK
    public void UpdateUi()
    {
        if (selectedQuest != null)
        {
            ShowDescription(selectedQuest);
        }
    }
    // <

    // HACK
    public void ShowDescription(ISS_Quest quest)
    {
        if (quest != null)
        {
            titleText.text = quest.Title;

            // HACK -This is the current lazy way to show a quest is complete in the description.
            if (HasQuest(quest) && quest.IsComplete)
            { titleText.text = "(C) " + quest.Title; }
            // <

            string objectives = "\n";
            foreach (Objective o in quest.CollectionObjectives)
            {
                objectives += (o.Item.Title + ": " + o.CurrentAmount + "/" + o.Amount + "\n");
            }

            descriptionText.text = quest.Description;
            descriptionText.text += objectives;

            selectedQuest = quest;
        }

    }
    //<
    public void CheckCompletion()
    {
        foreach (ISS_QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    //- Takes in a quest and checks our list of accepted quests to see if we the quest has already been accepted.
    public bool HasQuest(ISS_Quest quest)
    {
        //- Check if the quest exists in the list of accepted quests.
        return _AcceptedQuests.Exists(x => x.Title == quest.Title);
    }
}
