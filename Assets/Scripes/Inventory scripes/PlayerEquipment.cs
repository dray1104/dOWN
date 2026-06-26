using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform headPoint;
    public Transform weaponPoint;
    public Transform armorPoint;
    public Transform bootsPoint;

    private GameObject currentHelmet;
    private GameObject currentWeapon;
    private GameObject currentArmor;
    private GameObject currentBoots;

    public void EquipItem(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.Hat:
                Equip(ref currentHelmet, item.handitemPrefab, headPoint);
                break;

            case ItemType.Weapon:
                Equip(ref currentWeapon, item.handitemPrefab, weaponPoint);
                break;

            case ItemType.Armor:
                Equip(ref currentArmor, item.handitemPrefab, armorPoint);
                break;

            case ItemType.Boots:
                Equip(ref currentBoots, item.handitemPrefab, bootsPoint);
                break;
        }
    }

    private void Equip(ref GameObject currentItem, GameObject prefab, Transform parent)
    {
        if (currentItem != null)
            Destroy(currentItem);

        if (prefab != null)
        {
            currentItem = Instantiate(prefab, parent, false);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;
        }
    }
}
