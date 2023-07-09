using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment", order = 1)]
public class Equipment : Item, IEquippable
{
    private int _maxDurability = default;

   public override int StackSize
   { get => 1; }


    public int MaxDurability {  get => _maxDurability;  }

    public bool Equip()
    {
        throw new System.NotImplementedException();
    }
}
