using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST_TEXT_COUNT : MonoBehaviour
{
    public Text textt;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        print(textt.cachedTextGenerator.lineCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
