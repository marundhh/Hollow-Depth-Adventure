using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Mana")]
    public int maxMana = 50;
    public int currentMana;

    [Header("Combat")]
    public int damage = 10;
    public int defense = 2;

    [Header("Movement")]
    public float moveSpeed = 5f;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    // Cộng stats khi trang bị

    public void AddEquipmentStats(ItemData item)
    {
        maxHealth += item.bonusHealth;
        maxMana += item.bonusMana;
        damage += item.bonusDamage;
        moveSpeed += item.bonusSpeed;

        currentHealth = Mathf.Min(currentHealth, maxHealth);
        currentMana = Mathf.Min(currentMana, maxMana);

       
    }

    public void RemoveEquipmentStats(ItemData item)
    {
        maxHealth -= item.bonusHealth;
        maxMana -= item.bonusMana;
        damage -= item.bonusDamage;
        moveSpeed -= item.bonusSpeed;

        currentHealth = Mathf.Min(currentHealth, maxHealth);
        currentMana = Mathf.Min(currentMana, maxMana);

        
    }
}
