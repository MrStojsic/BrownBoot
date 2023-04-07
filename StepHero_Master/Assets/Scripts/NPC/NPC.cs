using System.Collections;
using System.Collections.Generic;
using Npcs.Occupation;
using UnityEngine;

namespace Npcs
{
    [CreateAssetMenu(fileName = "Citizen", menuName = "Npc/Citizen", order = 1)]
    public class Npc : ScriptableObject, IInteractable
    {
        [SerializeField] protected string title = "New NPC Name";
        [SerializeField] protected string greetingText = "Hello Adventurer";


        [SerializeField] private Vector2 _positionPercentage;
        public Vector2 PositionPercentage
        {
            get{ return _positionPercentage; }
        }

        public virtual void Interact()
        {
            Debug.Log($"{greetingText}, my name is {title}, nice to meet you.");
        }

        public virtual void StopIneract()
        {
         
        }
    }

}
