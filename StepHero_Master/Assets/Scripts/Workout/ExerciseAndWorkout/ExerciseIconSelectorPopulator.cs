using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseIconSelectorPopulator : MonoBehaviour
{
    [SerializeField] SelectorButton prefabIconSelector = default;
    [SerializeField] SelectorGroup selectorGroup = default;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ExerciseManager.instance.spriteArray.Length; i++)
        {
            SelectorButton go = Instantiate(prefabIconSelector, transform);
            go.GetComponent<Image>().sprite = ExerciseManager.instance.spriteArray[i];
            go.selectorGroup = selectorGroup;

        }
    }
}
