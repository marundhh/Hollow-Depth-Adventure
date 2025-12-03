using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class SkillSlot
{
    public SkillData skillData;  // ScriptableObject skill
    public Image cooldownIcon;   // Icon riêng cho skill
    [HideInInspector] public bool isCooldown = false; // trạng thái cooldown
}

public class AttackTrigger : MonoBehaviour
{
    [Header("Skills List")]
    public List<SkillSlot> skills = new List<SkillSlot>();

    private List<Transform> enemiesInRange = new List<Transform>();

    private void Update()
    {
        foreach (SkillSlot slot in skills)
        {
            if (slot.skillData != null && Input.GetKeyDown(slot.skillData.key))
            {
                TryUseSkill(slot);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ nhận khi va chạm với Enemy và va vào đúng BoxCollider có tag "AttackTrigger"
        if (collision.CompareTag("Enemy") && collision.gameObject.CompareTag("Enemy")
            && this.CompareTag("AttackTrigger")
            && !enemiesInRange.Contains(collision.transform))
        {
            enemiesInRange.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && this.CompareTag("AttackTrigger"))
        {
            enemiesInRange.Remove(collision.transform);
        }
    }


    private void TryUseSkill(SkillSlot slot)
    {
        if (slot.isCooldown || enemiesInRange.Count == 0 || slot.skillData == null) return;

        Transform target = GetClosestEnemy();
        if (target == null) return;

        StartCoroutine(UseSkill(slot, target.position));
    }

    private Transform GetClosestEnemy()
    {
        if (enemiesInRange.Count == 0) return null;

        Transform closest = enemiesInRange[0];
        float minDist = Vector2.Distance(transform.position, closest.position);

        foreach (Transform enemy in enemiesInRange)
        {
            float dist = Vector2.Distance(transform.position, enemy.position);
            if (dist < minDist)
            {
                closest = enemy;
                minDist = dist;
            }
        }
        return closest;
    }

    private IEnumerator UseSkill(SkillSlot slot, Vector3 pos)
    {
        slot.isCooldown = true;

        // Spawn chiêu
        GameObject fx = Instantiate(slot.skillData.attackEffectPrefab, pos, Quaternion.identity);
        Destroy(fx, slot.skillData.effectDuration);

        // UI cooldown: từ full → empty
        if (slot.cooldownIcon != null)
            slot.cooldownIcon.fillAmount = 1f;

        float timer = 0f;
        while (timer < slot.skillData.cooldownTime)
        {
            timer += Time.deltaTime;
            if (slot.cooldownIcon != null)
                slot.cooldownIcon.fillAmount = 1f - (timer / slot.skillData.cooldownTime);
            yield return null;
        }

        if (slot.cooldownIcon != null)
            slot.cooldownIcon.fillAmount = 1f;

        slot.isCooldown = false;
    }

}
