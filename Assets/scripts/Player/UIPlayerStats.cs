using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStats : MonoBehaviour
{
    [Header("Health & Mana UI")]
    public Slider healthSlider;
    public Slider manaSlider;

    [Header("EXP UI")]
    public Slider expSlider;
    public TMP_Text levelText;

    void Start()
    {
        // HEALTH
        healthSlider.maxValue = PlayerStats.instance.maxHealth;
        healthSlider.value = PlayerStats.instance.currentHealth;

        // MANA
        manaSlider.maxValue = PlayerStats.instance.maxMana;
        manaSlider.value = PlayerStats.instance.currentMana;

        // EXP
        expSlider.maxValue = ExperienceManager.instance.requiredExp;
        expSlider.value = ExperienceManager.instance.currentExp;

        // LEVEL TEXT
        levelText.text = "Lv " + ExperienceManager.instance.level;
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // HEALTH
        healthSlider.value = PlayerStats.instance.currentHealth;

        // MANA
        manaSlider.value = PlayerStats.instance.currentMana;

        // EXP
        expSlider.maxValue = ExperienceManager.instance.requiredExp;
        expSlider.value = ExperienceManager.instance.currentExp;

        // LEVEL
        levelText.text = "Lv " + ExperienceManager.instance.level;
    }
}
