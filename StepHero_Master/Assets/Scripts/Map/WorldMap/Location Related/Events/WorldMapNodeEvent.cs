using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapNodeEvent : MonoBehaviour
{
    [SerializeField] List<WorldMapEvent> possibleEvents = new List<WorldMapEvent>();
    [SerializeField] int[] eventProbabilites;

    private void OnValidate()
    {
        GetComponent<AStarNode>().WorldMapNodeEvent = this;
    }

    public WorldMapEvent RollEvent()
    {
        if (possibleEvents.Count != eventProbabilites.Length)
        {
            print("ERROR in " + this.name + " not all events have probabilty");
            return null;
        }


        if (possibleEvents.Count == 1)
        {
            return possibleEvents[0];
        }

        int eventChance = Random.Range(0, 101);


        for (int i = 0; i < possibleEvents.Count; i++)
        {
            eventChance -= eventProbabilites[i];
            if (eventChance <= 0)
            {
                print("event at index " + i);
                return possibleEvents[i];
            }
        }
        print("No event chosen");
        return null;
    }


}
