using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ========================= PLAYER DATA =========================
[System.Serializable]
public class PlayerData
{
    public int level;
    public float health;
    public string title;
    public string saveTime;

    public PlayerData(Player player)
    {
        level = player.Level;
        health = player.Health;
        title = player.Title;
        saveTime = player.saveTime.ToBinary().ToString();
    }

}
