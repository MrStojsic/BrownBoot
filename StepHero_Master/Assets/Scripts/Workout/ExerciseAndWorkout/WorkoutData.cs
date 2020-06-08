using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ========================= WORKOUT DATA =========================
[System.Serializable]
public class WorkoutData
{
    public bool isCustom;
    public int id;
    public DateTime completedTime;
    public TimeSpan duration;

    public WorkoutData(bool _isCustom, int _id, int _hours, int _mins)
    {
        isCustom = _isCustom;
        id = _id;
        completedTime = DateTime.Now;
        duration += TimeSpan.FromHours(_hours);
        duration += TimeSpan.FromMinutes(_mins);
    }
}
