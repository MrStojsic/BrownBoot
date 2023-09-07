using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestDetail : DisplaySlotDetail
{
	private ISS_Quest _quest;
	public ISS_Quest Quest
	{
		get { return _quest; }
		protected set
		{
			if(value != null)
			{
				_quest = value;
				_title.text = value.Title;
				_icon.sprite = value.Icon;
				SetLongOrShortDescription(true);
			}
			else
			{
				HideEntireDisplay();
			}
		}
	}

	[SerializeField] private Text _descriptionText = default;
	public Text DescriptionText
	{
		get { return _descriptionText; }
	}

	private bool descriptionIsShort = true;

	[SerializeField] private LayoutElement _descriptionAreaLayoutElement = null;

	[SerializeField] private ObjectiveSlotManager objectiveSlotManager = null;



	public override void DisplayDetail(Slot slot)
	{
		Quest = (slot as QuestSlot).Quest;
		objectiveSlotManager.InitialiseSlots(Quest);
		base.DisplayDetail(slot);
	}

	public void ToggleLongOrShortDescription()
	{
		SetLongOrShortDescription(!descriptionIsShort);
	}

	public void SetLongOrShortDescription(bool isShort)
	{
		descriptionIsShort = isShort;
		if (isShort)
		{
			_descriptionText.text = Quest.ShortDescription;
		}
		else
		{
			_descriptionText.text = Quest.LongDescription;
		}
		_descriptionAreaLayoutElement.minHeight = (_descriptionText.preferredHeight + _descriptionText.fontSize);
	}

	/*
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

			descriptionText.text = quest.ShortDescription;
			descriptionText.text += objectives;

			_selectedQuest = quest;
		}

	}*/
}
