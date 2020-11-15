using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npcs;
using Npcs.Occupation;

[CreateAssetMenu(fileName = "LocationPoi", order = 1)]
public class LocationPoi : ScriptableObject
{
    [SerializeField] Sprite _townMap;
    public Sprite TownMap
    {
        get { return _townMap; }
    }

    //[SerializeField] private Npc npc;
    [SerializeField] private List<Merchant> _merchants;
    public List<Merchant> Merchants
    {
        get { return _merchants; }
    }



    public void TEST_PrintMerchantName()
    {
        foreach (Merchant merchant in _merchants)
        {
            Debug.Log(merchant.BusinessTitle);
        }
    }
}
