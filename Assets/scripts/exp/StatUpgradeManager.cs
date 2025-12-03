using UnityEngine;
using TMPro;

public class StatUpgradeManager : MonoBehaviour
{
    public static StatUpgradeManager instance;

    [Header("Points UI")]
    public TMP_Text pointText;

    [Header("Temporary Stat Values")]
    public int tempHealth = 0;
    public int tempMana = 0;
    public int tempDamage = 0;
    public int tempDefense = 0;

    private int points = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdatePointUI();
    }

    // Gọi từ ExperienceManager khi lên level
    public void AddPoint(int p)
    {
        points += p;
        UpdatePointUI();
    }

    public bool TryIncrease(ref int statVar)
    {
        if (points <= 0) return false;

        statVar++;
        points--;
        UpdatePointUI();
        return true;
    }

    public bool TryDecrease(ref int statVar)
    {
        if (statVar <= 0) return false;

        statVar--;
        points++;
        UpdatePointUI();
        return true;
    }

    // Khi nhấn nút Apply (mũi tên xanh)
    public void ApplyUpgrades()
    {
        PlayerStats.instance.maxHealth += tempHealth;
        PlayerStats.instance.maxMana += tempMana;
        PlayerStats.instance.damage += tempDamage;
        PlayerStats.instance.defense += tempDefense;

        PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
        PlayerStats.instance.currentMana = PlayerStats.instance.maxMana;

        ResetTemporaryStats();
    }

    void ResetTemporaryStats()
    {
        tempHealth = 0;
        tempMana = 0;
        tempDamage = 0;
        tempDefense = 0;
    }

    void UpdatePointUI()
    {
        pointText.text = points.ToString();
    }
}
