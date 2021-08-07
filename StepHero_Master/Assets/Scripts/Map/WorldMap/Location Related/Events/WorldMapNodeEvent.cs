using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapNodeEvent : MonoBehaviour
{

    [Range(0, 100)]
    [SerializeField] private int _overallChanceOfAnEncounter = 0;
    public int OverallChanceOfAnEncounter { set { _overallChanceOfAnEncounter = value; } }

    // IDEA - Can also make a forced encounter variable that if not null will force the encounter when the player enters the area, this can be used for quests or tutorials.
    // This encounter would need to be saved in the quests section though so it can be saved and loaded in.

    [SerializeField] List<WorldMapEvent> possibleEncounters = new List<WorldMapEvent>();
    [SerializeField] private int[] encounterWeights;

    // This is used to add a reference to this script in the objects AStar node when this component is added to an object.
    // NOTE: Reset is called when adding a component to an object.
    private void Reset()
    {
        GetComponent<AStarNode>().WorldMapNodeEncounter = this;
    }

    public WorldMapEvent RollEvent()
    {
        if (Random.Range(0, 101) < _overallChanceOfAnEncounter)
        {
            if (possibleEncounters.Count != encounterWeights.Length)
            {
                print("ERROR in " + this.name + " not all encounters have probabilty");
                return null;
            }

            if (possibleEncounters.Count == 1)
            {
                return possibleEncounters[0];
            }

            // Calculate total encounter weights.
            int totalEncounterWeight = 0;
            for (int i = 0; i < encounterWeights.Length; i++)
            {
                totalEncounterWeight += encounterWeights[i];
            }

            int encounterChance = Random.Range(0, totalEncounterWeight + 1);


            for (int i = 0; i < possibleEncounters.Count; i++)
            {
                encounterChance -= encounterWeights[i];
                if (encounterChance <= 0)
                {
                    print("event at index " + i);
                    return possibleEncounters[i];
                }
            }
        }
        print("No event chosen");
        return null;
    }


}
