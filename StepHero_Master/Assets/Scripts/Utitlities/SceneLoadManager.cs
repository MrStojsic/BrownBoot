using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This will take in a the name of the current location and store it between scene loading to pass onto the town scene.
    public void LoadTownMap(string locationName)
    {
        // Once Scene is loaded we want to check resources for a Location data with the passed name,
        // get its NPC data and set the TownMap details including the map image, NPC icon locations.
    }
}
