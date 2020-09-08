using UnityEngine;
using System.Collections;
using UniPedometer;
using System;
using UnityEngine.UI;

public class Sample4 : MonoBehaviour
{
    //[SerializeField] Text totalStepsText = default;
    [SerializeField] Text todaysStepsText = default;
    //[SerializeField] InputField stepsInput = default;
    [SerializeField] Button queryButton = default;


    [SerializeField] DateTime newTime = default;
    [SerializeField] DateTime oldTime = default;

    [SerializeField] bool isNewDay = true;

    [SerializeField] int todaysSteps = 0;


    void Start()
    {

        LoadOldTime();

        queryButton.onClick.AddListener(() => {
#if UNITY_EDITOR_OSX

            TEMP_QueryAndShow();
#else
            QueryAndShow();
#endif

        });
    }


    public void TEMP_QueryAndShow()
    {
    
        print("oldTime: " + oldTime.ToString());

        todaysSteps += 100;

        SaveNewTime();
    }
    public void QueryAndShow()
    { 
        SaveNewTime();

        UniPedometerManager.IOS
            .QueryPedometerDataFromDate(
                oldTime,
                newTime,
                (CMPedometerData data, NSError error) => ShowPedometerData(data, error));
    }

    void ShowPedometerData(CMPedometerData data, NSError error)
    {
        if (error == null)
        {
            todaysStepsText.text = string.Format("start date: {0}\nend date: {1}\n number of steps: {2}\ndistance: {3}",
                data.StartDate, data.EndDate, data.NumberOfSteps, data.Distance);

            todaysSteps += data.NumberOfSteps;
        }
        else
        {
            todaysStepsText.text = error.LocalizedDescription;

        }
    }
    void SaveNewTime()
    {
        //Savee the current system time as a string in the player prefs class
        PlayerPrefs.SetString("oldTime", newTime.ToBinary().ToString());
        print("oldTime saved as: " + newTime);

        PlayerPrefs.SetInt("todaysSteps", todaysSteps);
        print("todaysSteps saved as: " + todaysSteps);
    }

    void LoadOldTime()
    {
        newTime = DateTime.Now;

        long oldTimeAsBinary = Convert.ToInt64(PlayerPrefs.GetString("oldTime","0"));

        oldTime = oldTimeAsBinary == 0 ? DateTime.Today : DateTime.FromBinary(oldTimeAsBinary);
        

        print("lastTimeChecked loaded as: " + oldTime);

        isNewDay = oldTime < DateTime.Today ? true : false;

        if (isNewDay == false)
        {
            todaysSteps = PlayerPrefs.GetInt("todaysSteps");
        }
    }
}
