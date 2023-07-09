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
    private ISS_QuestScript questPrefeb;
    [SerializeField]
    private SelectorGroup _questSlotSelectorGroup = default;


    private List<ISS_QuestScript> questScripts = new List<ISS_QuestScript>();

    private List<ISS_Quest> _quests = new List<ISS_Quest>();
    public List<ISS_Quest> Quests
    {
        get { return _quests; }
        set { _quests = value; }
        }

    // HACK
    public Text descriptionText;
    public Text titleText;

    public override void Initialise()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptQuest(ISS_Quest quest)
    {
        foreach (CollectionObjective o in quest.CollectionObjectives)
        {
            PlayerInventory.Instance.itemCountChanged += new ItemCountChanged(o.UpdateCollectedItemCount);
            //o.UpdateCollectedItemCount();
        }

        ISS_QuestScript newMenuQuest;
        newMenuQuest = Instantiate(questPrefeb, _questSlotSelectorGroup.selectorButtonsParent);
        newMenuQuest.SelectorButton.selectorGroup = _questSlotSelectorGroup;

        newMenuQuest.Quest = quest;

        Quests.Add(quest);
        questScripts.Add(newMenuQuest);

        // TODO - Complete this once we have the quest detail script running.
        //newMenuQuest.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuQuest));
        // HACK
        newMenuQuest.SelectorButton.AddListenerActionToOnSelected(() => ShowDescription(newMenuQuest.Quest));
        // <


        //HACK
        newMenuQuest.GetComponentInChildren<Text>().text = quest.Title;
        // <


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
}
