using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance;


    
    [SerializeField]private Enemy[] enemies;
    private int currentEnemyIndex = 0;



    private void Awake()
    {
        CreateInstance();
        SelectEnemyTarget(currentEnemyIndex);
    }

    void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
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
