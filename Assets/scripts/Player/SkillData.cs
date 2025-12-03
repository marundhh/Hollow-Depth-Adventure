using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Game/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Skill Settings")]
    public GameObject attackEffectPrefab;
    public float effectDuration = 1f;
    public float cooldownTime = 1f;

    [Header("Display")]
    public Sprite skillIcon;
    public string skillName;
}
