using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEquationTester : MonoBehaviour
{
    public GameObject prefab = null;
    public float incrementAmount = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeAsDecimal = incrementAmount;
        float totalTime = 0;
        for (int i = 1; i < 99; i++)
        {
            if (i == 1) { timeAsDecimal = incrementAmount; }

            else { 

            timeAsDecimal =i * incrementAmount; }

            //timeAsDecimal = Mathf.Pow(i* 2,0.33333f);
            totalTime += timeAsDecimal;
            Instantiate(prefab, new Vector2(i, timeAsDecimal), Quaternion.identity);
            Debug.Log(timeAsDecimal + " \t\t  " + i + "\t\t " + totalTime);
        }
    }
}
