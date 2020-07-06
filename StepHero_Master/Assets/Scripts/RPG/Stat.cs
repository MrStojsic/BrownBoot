using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=8StwNBJ5fE8&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=6
public class Stat : MonoBehaviour
{
    private Image content;

    private float currentFillAmount;

    [SerializeField]
    private float lerpSpeed = 10f; 


    public float MyMaxValue { get; set; }

    private float currentValue;
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFillAmount = currentValue / MyMaxValue;
            text.text = currentValue + "/" + MyMaxValue;

        }
    }
    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        text.text = currentValue + "/" + MyMaxValue;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentFillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFillAmount, Time.deltaTime * lerpSpeed);
        }


    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
