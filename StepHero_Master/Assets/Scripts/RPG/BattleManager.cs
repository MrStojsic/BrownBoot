using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use the below video for help when setting up the enemies health UI. its not spot on what we need but its close and good inspiration.
// https://www.youtube.com/watch?v=lcYDf70nDHo&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=20
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BattleManager>();
            }
            return instance;
        }
    }



[SerializeField]private Enemy[] enemies;
    private int currentEnemyIndex = 0;



    private void Awake()
    {
        CreateInstance();
        SelectEnemyTarget(currentEnemyIndex);
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEnemyTarget(int targetIndex)
    {
        if (currentEnemyIndex <= enemies.Length)
        {
            enemies[currentEnemyIndex].Deselect();
        }

        currentEnemyIndex = targetIndex;
        enemies[currentEnemyIndex].Select();

    }

}
