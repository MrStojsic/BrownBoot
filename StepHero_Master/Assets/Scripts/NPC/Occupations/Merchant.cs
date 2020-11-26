using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Npcs.Occupation
{
    [System.Serializable]

    public class Merchant: Npc, IOccupation
    {
        [SerializeField] private string _businessTitle;
        public string BusinessTitle
        {
            get { return _businessTitle; }
        }

        [SerializeField] private Sprite _icon;
        public Sprite Icon
        {
            get { return _icon; }
        }
    }
}
