using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Collections.Generic;
using System;

public static class SaveSystem
{
    static readonly string[] paths =
    {
        "Player.balls",
        "CustomExerciseData.balls",
        "DefaultExerciseTotalTimeTrained.balls",
        "WorkoutData.balls"
    };
    // ========================= SAVE SYSTEM =========================

    // ------------------------- DELETE (GENERAL) -------------------------
    /// <summary>
    ///  Path Index are as follows:
    ///  0 = "Player.balls",
    ///  1 = "CustomExerciseData.balls",
    ///  2 = "DefaultExerciseTotalTimeTrained.balls",
    ///  3 =  "WorkoutData.balls"
    /// </summary>
    public static void DeleteSaveFile(int pathIndex)
    {
        string path = Path.Combine(Application.persistentDataPath, paths[pathIndex]);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Successfully deleted " + path);
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
        }
    }

    // ------------------------- PLAYER -------------------------
    public static void SavePlayer(Player _player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, paths[0]);

        Debug.Log("Saved PlayerData to : " + Application.persistentDataPath);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData playerDataToSave = new PlayerData(_player);

        formatter.Serialize(fileStream, playerDataToSave);

        fileStream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, paths[0]);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerData playerData = formatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            return playerData;
        }
        else
        {
            return null;
        }
    }

    // -------------------- USER MADE EXERCISE DATA --------------------
    public static void SaveCustomExerciseData(Dictionary<int,ExerciseData> _exerciseData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, paths[1]);

        Debug.Log("Saved userMadeExercisesData to : " + Application.persistentDataPath);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, _exerciseData);

        fileStream.Close();
    }

    public static Dictionary<int, ExerciseData> LoadCustomExerciseData()
    {
        string path = Path.Combine(Application.persistentDataPath, paths[1]);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Dictionary<int, ExerciseData> exerciseDatas = new Dictionary<int, ExerciseData>();
            exerciseDatas = formatter.Deserialize(fileStream) as Dictionary<int, ExerciseData>;
            fileStream.Close();

            return exerciseDatas;
        }
        else
        {
            return null;
        }
    }

    // -------------------- DEFAULT EXERCISE TOTAL TIME --------------------
    public static void SaveDefaultExerciseTotalTime(List<TimeSpan> _timeSpans)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, paths[2]);

        Debug.Log("Saved DefaultExerciseTotalTimeTrainedList to : " + Application.persistentDataPath);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, _timeSpans);

        fileStream.Close();
    }

    public static List<TimeSpan> LoadDefaultExerciseTotalTime()
    {
        string path = Path.Combine(Application.persistentDataPath, paths[2]);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            timeSpans = formatter.Deserialize(fileStream) as List<TimeSpan>;
            fileStream.Close();

            return timeSpans;
        }
        else
        {
            //Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
    // -------------------- WORKOUT DATA --------------------
    public static void SaveWorkoutData(List<WorkoutData> workoutData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, paths[3]);

        Debug.Log("Saved WorkoutLinkedList to : " + Application.persistentDataPath);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        formatter.Serialize(fileStream, workoutData);

        fileStream.Close();
    }

    public static List<WorkoutData> LoadWorkoutData()
    {
        string path = Path.Combine(Application.persistentDataPath, paths[3]);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            List<WorkoutData> workoutData = new List<WorkoutData>();
            workoutData = formatter.Deserialize(fileStream) as List<WorkoutData>;
            fileStream.Close();

            return workoutData;
        }
        else
        {
            return null;
        }
    }
}
