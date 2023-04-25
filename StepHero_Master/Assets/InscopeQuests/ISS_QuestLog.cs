using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]
// Added Objectives in https://www.youtube.com/watch?v=FLXzG-bGHOA&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=101
public class ISS_QuestLog : MonoBehaviour
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


    [SerializeField]
    private ISS_QuestScript questPrefeb;
    [SerializeField]
    private SelectorGroup _questSlotSelectorGroup = default;

    // HACK
    public Text descriptionText;
    public Text titleText;

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
        ISS_QuestScript newMenuQuest;
        newMenuQuest = Instantiate(questPrefeb, _questSlotSelectorGroup.selectorButtonsParent);
        newMenuQuest.SelectorButton.selectorGroup = _questSlotSelectorGroup;

        newMenuQuest.Quest = quest;
        // TODO - Complete this once we have the quest detail script running.
        //newMenuQuest.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuQuest));
        // HACK
        newMenuQuest.SelectorButton.AddListenerActionToOnSelected(() => ShowDescription(newMenuQuest.Quest));



        //HACK
        newMenuQuest.GetComponentInChildren<Text>().text = quest.Title;


    }
    // HACK
    public void ShowDescription(ISS_Quest quest)
    {
        titleText.text = quest.Title;

        string objectives = "\n";
        foreach (Objective o in quest.CollectionObjectives)
        {
            objectives += (o.Type + ": " + o.CurrentAmount + "/" + o.Amount + "\n");
        }

        descriptionText.text = quest.Description;
        descriptionText.text += objectives;


    }

}
