using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSlotManager : MonoBehaviour
{
	[SerializeField]
	private ObjectiveSlot _slotPrefab = default;

	[SerializeField]
	private List<ObjectiveSlot> _existingSlots = new List<ObjectiveSlot>();

	[SerializeField]
	private Transform _pooledSlotHolder = default;


	// TODO IMPLIMENT COLOURS TO OBJECTIVE ICONS


    public void InitialiseSlots(ISS_Quest quest)
    {
        int index = 0;

        for (; index < quest.CollectionObjectives.Length; index++)
        {
            if (_existingSlots.Count > index)
            {
                _existingSlots[index].SetObjectiveDetails(quest.CollectionObjectives[index]);
                _existingSlots[index].transform.SetParent(this.transform);
                _existingSlots[index].transform.SetSiblingIndex(index);
                _existingSlots[index].gameObject.SetActive(true);
            }
            else
            {
                CreateNewSlot(index, quest.CollectionObjectives[index]);
            }
        }

        if (quest.CollectionObjectives.Length < _existingSlots.Count)
        {
            for (; index < _existingSlots.Count; index++)
            {
                PoolSlot(_existingSlots[index]);
            }
        }
    }

    void CreateNewSlot(int slotIndex, Objective objective)
    {
        ObjectiveSlot newSlot;
        newSlot = Instantiate(_slotPrefab,this.transform);
        // - Set Objectives completion status.
        newSlot.SetObjectiveDetails(objective);

        newSlot.transform.name += _existingSlots.Count.ToString();
        _existingSlots.Add(newSlot);
    }

    public void PoolSlot(ObjectiveSlot emptySlot)
    {
        emptySlot.transform.SetParent(_pooledSlotHolder);
    }
}
