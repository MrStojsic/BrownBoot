using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Npcs.Occupation
{
    [CreateAssetMenu(fileName = "Merchant", menuName = "Npc/Merchant", order = 1)]
    public class Merchant : Npc, IOccupation
    {
        [SerializeField] private string _businessTitle;
        public string BusinessTitle
        {
            get { return _businessTitle; }
        }
    }
}
