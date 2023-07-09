using System.Collections;
using System.Collections.Generic;
using Npcs.Occupation;
using UnityEngine;

namespace Npcs
{
    [CreateAssetMenu(fileName = "Citizen", menuName = "Npc/Citizen", order = 1)]
    public class Npc : ScriptableObject, IInteractable
    {
        // DIALOG
        [SerializeField] protected string title = "New NPC Name";
        [SerializeField] protected string greetingText = "Hello Adventurer";



  
        // LOCATION
        [SerializeField] private Vector2 _positionPercentage;
        public Vector2 PositionPercentage
        {
            get{ return _positionPercentage; }
        }

        // UI
        [SerializeField]
        private NpcWindow npcWindow;

        public bool IsInteracting { get; set; }

        public virtual void Interact()
        {
            if (!IsInteracting)
            {
                IsInteracting = true;
                npcWindow.Show(this);
            }
        }

        public virtual void StopInteract()
        {
            if (IsInteracting)
            {
                IsInteracting = false;
                npcWindow.Hide();
            }
        }
    }

}
