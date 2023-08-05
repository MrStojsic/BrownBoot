using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Npcs.Occupation
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Merchant", menuName = "Npc/Merchant", order = 2)]
    public class Merchant: Npc, IOccupation
    {
        [SerializeField] private string _businessTitle = default;
        public string BusinessTitle
        {
            get { return _businessTitle; }
        }

        [SerializeField] private Sprite _icon = null;
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

        public override void StopInteract()
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
