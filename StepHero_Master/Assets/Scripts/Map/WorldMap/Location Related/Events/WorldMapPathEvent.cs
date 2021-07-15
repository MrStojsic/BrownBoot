using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMapPathEvent: WorldMapEvent
{
    [SerializeField] WorldMapEvent worldMapEvent;
    [SerializeField] public float TriggerDistanceAlongPath;

}
