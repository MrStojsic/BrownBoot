using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseIconSelectorPopulator : MonoBehaviour
{
    [SerializeField] GameObject prefabIconSelector;
    [SerializeField] SelectorGroup selectorGroup;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ExerciseManager.instance.spriteArray.Length; i++)
        {
            GameObject go = Instantiate(prefabIconSelector, transform);
            go.GetComponent<Image>().sprite = ExerciseManager.instance.spriteArray[i];
            go.GetComponent<SelectorButton>().selectorGroup = selectorGroup;
        }
    }
}
