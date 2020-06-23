using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TEST_DistanceCalculator : MonoBehaviour
{

    public Vector2 pointA;
    public Vector2 pointB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       print( Vector2.Distance(pointA, pointB));
    }
}
