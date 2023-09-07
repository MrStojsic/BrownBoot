using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=cAiYebAR8Hk&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=100")]
public class ISS_QuestScript : MonoBehaviour
{
    public ISS_Quest Quest { get; set; }

    private bool isMarkedCompleted = false;

    [SerializeField] protected Image _icon = default;
    public Image Icon
    {
        get { return _icon; }
    }
    [SerializeField] protected Text _title = default;
    public Text Title
    {
        get { return _title; }
    }

    [SerializeField] private SelectorButton _selectorButton = default;
    public SelectorButton SelectorButton
    {
        get { return _selectorButton; }
    }

    // ONLY TO MATCH TUTORIAL, will replace later.
    public void Select()
    {
        Debug.Log("Selected");

    }
    // ONLY TO MATCH TUTORIAL, will replace later.
    public void Deselect()
    {
        Debug.Log("Deselected");
    }

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
