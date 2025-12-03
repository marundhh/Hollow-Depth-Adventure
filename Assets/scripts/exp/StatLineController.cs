using UnityEngine;
using TMPro;

public class StatLineController : MonoBehaviour
{
    public enum StatType { Health, Mana, Damage, Defense }
    public StatType statType;

    public TMP_Text valueText;

    private void Update()
    {
        int value = GetTempValue();
        valueText.text = value.ToString();
    }

    int GetTempValue()
    {
        switch (statType)
        {
            case StatType.Health: return StatUpgradeManager.instance.tempHealth;
            case StatType.Mana: return StatUpgradeManager.instance.tempMana;
            case StatType.Damage: return StatUpgradeManager.instance.tempDamage;
            case StatType.Defense: return StatUpgradeManager.instance.tempDefense;
        }
        return 0;
    }

    // -----------------------------
    // BUTTONS
    // -----------------------------
    public void Increase()
    {
        ref int value = ref GetRefTempValue();

        StatUpgradeManager.instance.TryIncrease(ref value);
    }

    public void Decrease()
    {
        ref int value = ref GetRefTempValue();

        StatUpgradeManager.instance.TryDecrease(ref value);
    }

    public void Apply()
    {
        StatUpgradeManager.instance.ApplyUpgrades();
    }

    ref int GetRefTempValue()
    {
        switch (statType)
        {
            case StatType.Health: return ref StatUpgradeManager.instance.tempHealth;
            case StatType.Mana: return ref StatUpgradeManager.instance.tempMana;
            case StatType.Damage: return ref StatUpgradeManager.instance.tempDamage;
            default: return ref StatUpgradeManager.instance.tempDefense;
        }
    }
}
