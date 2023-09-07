using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTypePocket : ICountable
{
	[SerializeField]
	public List<ISS_Quest> storedQuests;

    private QuestType _pocketQuestType;
    public QuestType PocketQuestType
    {
        get => _pocketQuestType;
	}

    public void Initialise(QuestType questType)
    {
        storedQuests = new List<ISS_Quest>();
        _pocketQuestType = questType;
    }

    private int _sizeLimit = 50;
    public int SizeLimit
    {
        get
        {
            return _sizeLimit;
        }
        set
        {
            _sizeLimit = value;
        }
    }

    public int Count
    {
        get { return storedQuests.Count; }
    }

    public bool IsEmpty
    {
        get { return storedQuests.Count == 0; }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || Count < _sizeLimit)
            {
                return false;
            }
            return true;
        }
    }
}
