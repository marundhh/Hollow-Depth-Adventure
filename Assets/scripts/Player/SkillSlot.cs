using UnityEngine;

[System.Serializable]
public class SkillSlot
{
    public SkillData skillData;     // dữ liệu skill
    [HideInInspector] public bool isCooldown = false;  // trạng thái cooldown
}
