using UnityEngine;

public enum ItemType
{
    Helmet,
    Chest,
    Legs,
    Weapon,
    Shield,
    Ring,
    Necklace,
    NormalItem
}


[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

    public bool isStackable;
    public int value; // bình máu, mana

    [Header("Equipment Stats Bonus")]
    public int bonusHealth;
    public int bonusMana;
    public int bonusDamage;
    public float bonusSpeed;

    [TextArea]
    public string description;
}
