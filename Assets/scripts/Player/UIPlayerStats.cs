using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;
    public Slider manaSlider;

    void Start()
    {
        // Cập nhật giá trị max
        healthSlider.maxValue = PlayerStats.instance.maxHealth;
        manaSlider.maxValue = PlayerStats.instance.maxMana;

        // Set giá trị ban đầu
        healthSlider.value = PlayerStats.instance.currentHealth;
        manaSlider.value = PlayerStats.instance.currentMana;
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        healthSlider.value = PlayerStats.instance.currentHealth;
        manaSlider.value = PlayerStats.instance.currentMana;
    }
}
