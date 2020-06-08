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
        level = player.level;
        health = player.health;
        title = player.title;
        saveTime = player.saveTime.ToBinary().ToString();
    }

}
