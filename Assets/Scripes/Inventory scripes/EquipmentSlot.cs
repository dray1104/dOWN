using UnityEngine;
public class EquipmentSlot : Slot
{ 
    public ItemType allowedType;

    public bool CanEquip(ItemSO item)
    {
        if (item == null) return false;
        return item.itemType == allowedType;
    }
}