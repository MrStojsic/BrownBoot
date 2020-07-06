using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int id;

    [SerializeField] private CanvasGroup healthGroup;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public virtual void Select()
    {
        healthGroup.alpha = 1;
    }

    public virtual void Deselect()
    {
        healthGroup.alpha = 0;
    }
}
