using System.Collections;
using System.Collections.Generic;
using Npcs.Occupation;
using UnityEngine;

namespace Npcs
{
    [CreateAssetMenu(fileName = "Citizen", menuName = "Npc/Citizen", order = 1)]
    public class Npc : ScriptableObject, IInteractable
    {
        [SerializeField] private string title = "New NPC Name";
        [SerializeField] private string greetingText = "Hello Adventurer";


        public void Interact()
        {
            Debug.Log($"{greetingText}, my name is {title}, nice to meet you.");
        }

        public void StopIneract()
        {
         
        }
    }

}
