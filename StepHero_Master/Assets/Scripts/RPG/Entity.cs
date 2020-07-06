using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private string title = "Orc";
    private int level = 1;
    private float health = 100f;
   
    public string Title {
        get { return title; }
        set { title = value; }
    }
    public  int Level {
        get { return level; }
        set { level = value; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }


    [SerializeField]
    protected Stat testStat;
    [SerializeField]
    protected float maxTestStat;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        testStat.Initialize(maxTestStat, maxTestStat);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
