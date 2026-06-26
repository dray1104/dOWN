using UnityEngine;

public enum ItemType
{
    Hat,
    Armor,
    Boots,
    Weapon,
    Consumable,
    Ability
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStackSize = 1; 

    public ItemType itemType; // NEW
    public float cooldown = 2f;
    public AudioClip useSound; // NEW

    public GameObject itemPrefab;
    public GameObject handitemPrefab;

    public string description;
    public virtual void Use(GameObject user)
    {
        Debug.Log("Used item");
    }
}