using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private string title = "New NPC Name";
        [SerializeField] private string greetingText = "Hello Adventurer!";


        public void Interact()
        {
            Debug.Log($"{greetingText}, my name is {title}, nice to meet you.");
        }

        public void StopIneract()
        {
         
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
