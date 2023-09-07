using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUiManager : SlotUiManager
{
	private static QuestLogUiManager _instance;
	public static QuestLogUiManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<QuestLogUiManager>();
			}
			return _instance;
		}
	}

	// HACK - this is just so when we hit play the window is set as it would be ingame.
	public void Start()
	{
		Show();
	}

	public void UpdateDisplayedQuest()
	{
		_displayDetailSlot.DisplayDetail(_existingSlots[selectedSlotIndex]);
	}

	protected override void SetSpecializedSlotFunctionality(Slot newSlot, int slotIndex)
	{
		(newSlot as QuestSlot).Initialise(ISS_QuestLog.Instance.QuestTypePockets[selectedPocketIndex].storedQuests[slotIndex], slotIndex);
	}

	public override void ClearEmptySlot(Slot slot)
	{
		base.ClearEmptySlot(slot);
	}

	public override void Show()
	{
		InitaliseSlotPockets();
		base.Show();
	}

	public void InitaliseSlotPockets()
	{
		slotPockets = ISS_QuestLog.Instance.QuestTypePockets;
		InitialisePockets();
		InitialiseSlotsInPockets();
	}




}
