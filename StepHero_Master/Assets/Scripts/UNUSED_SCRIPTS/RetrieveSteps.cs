using UnityEngine;
using System.Collections;
using UniPedometer;
using System;
using UnityEngine.UI;

public class RetrieveSteps : MonoBehaviour
{
    [SerializeField] Text text = default;
    [SerializeField] Button queryButton = default;
    [SerializeField] DateTime lastTimeChecked = default;

    void Start()
    {
        queryButton.onClick.AddListener(() => {

            Debug.Log("RAN TEST");
            QueryAndShow();
        });
    }

    public void QueryAndShow()
    {
        DateTime fromTime;
        if (lastTimeChecked <= DateTime.Today)
        {
            fromTime = DateTime.Today;
        }
        else
        {
            fromTime = lastTimeChecked;

        }

        UniPedometerManager.IOS
            .QueryPedometerDataFromDate(
                fromTime,
                DateTime.Now,
                (CMPedometerData data, NSError error) => ShowPedometerData(data, error));
    }

    void ShowPedometerData(CMPedometerData data, NSError error)
    {
        if (error == null)
        {
            // Call SaveNewTimeChecked() as we were successful and need to make sure we dont add already counted steps if checked later on same day.
            SaveNewTimeChecked();
            // Here we will call to display a pop up showing the number of additional steps taken that day.
            text.text = string.Format("start date: {0}\nend date: {1}\n number of steps: {2}\ndistance: {3} Your man can walk: {4}meters.",
                data.StartDate, data.EndDate, data.NumberOfSteps, data.Distance, data.NumberOfSteps* 0.762f);
        }
        else
        {
            text.text = error.LocalizedDescription;
        }


    }

    void SaveNewTimeChecked()
    {
        lastTimeChecked = DateTime.Now;
        // Here we will save the time and date we just checked, to make sure we dont add already counted steps if checked later on same day.
        // we will use this time as our 
    }
}
