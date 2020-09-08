using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomPathEvent : MonoBehaviour
{

    [SerializeField]List<InteractableEvent> possibleEvents = new List<InteractableEvent>();


    public InteractableEvent Initialise()
    {
        float eventChance = Random.Range(0, 100);

        //print("eventChance = "+ eventChance);
        for (int i = 0; i < possibleEvents.Count; i++)
        {
            eventChance -= possibleEvents[i].percentageChanceOfEvent;
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
