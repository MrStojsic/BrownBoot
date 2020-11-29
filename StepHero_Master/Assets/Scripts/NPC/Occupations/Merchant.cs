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
        [Header("Common")]
        [SerializeField] private List<PossibleStock> commonPossibleStockItems = default;
        [SerializeField] private int minCommonItemsExpected = default;
        [Header("Uncommon")]
        [SerializeField] private List<PossibleStock> uncommonPossibleStockItems = default;
        [SerializeField] private int minUncommonItemsExpected = default;
        [Header("Rare")]
        [SerializeField] private List<PossibleStock> rarePossibleStockItems = default;
        [SerializeField] private int minRareItemsExpected = 0;

        public override void Interact()
        {
      
        }

        public override void StopIneract()
        {

        }
    }

    [System.Serializable]
    public struct PossibleStock
    {
        public Item _item;
    
        public int _maxNumberOfItem;
    }
}
