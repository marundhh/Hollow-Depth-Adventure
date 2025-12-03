using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class AttackTrigger : MonoBehaviour
{
    [Header("Link to SkillBar")]
    public SkillBarManager skillBar; // link in inspector (player's SkillBarManager)

    private List<Transform> enemiesInRange = new List<Transform>();
    private Animator animator;

    private void Start()
    {
        if (skillBar == null)
            Debug.LogWarning("SkillBarManager chưa được gán cho AttackTrigger!");

        animator = GetComponentInParent<Animator>();
        if (animator == null)
            Debug.LogWarning("Không tìm thấy Animator trên Player hoặc parent!");
    }

    // Called by SkillBarManager when player presses 1..5
    public void ActivateSkill(int index)
    {
        if (skillBar == null) return;
        if (index < 0 || index >= skillBar.slots.Length) return;

        var slot = skillBar.slots[index];
        if (slot == null || slot.skillData == null) return;
        if (slot.isCooldown) return;
        if (enemiesInRange.Count == 0) return;

        TryUseSkill(slot, index);
    }

    private void TryUseSkill(SkillSlot slot, int slotIndex)
    {
        if (slot.isCooldown || slot.skillData == null || enemiesInRange.Count == 0) return;

        Transform target = GetClosestEnemy();
        if (target == null) return;

        animator?.SetTrigger("Attack");

        StartCoroutine(UseSkillCoroutine(slot, slotIndex, target.position));
    }

    private Transform GetClosestEnemy()
    {
        if (enemiesInRange.Count == 0) return null;
        Transform closest = enemiesInRange[0];
        float minDist = Vector2.Distance(transform.position, closest.position);

        foreach (Transform e in enemiesInRange)
        {
            float d = Vector2.Distance(transform.position, e.position);
            if (d < minDist)
            {
                minDist = d;
                closest = e;
            }
        }
        return closest;
    }

    private IEnumerator UseSkillCoroutine(SkillSlot slot, int slotIndex, Vector3 pos)
    {
        slot.isCooldown = true;

        // spawn effect
        if (slot.skillData.attackEffectPrefab != null)
        {
            GameObject fx = Instantiate(slot.skillData.attackEffectPrefab, pos, Quaternion.identity);
            Destroy(fx, slot.skillData.effectDuration);
        }

        // update UI: set cooldown full initially (1)
        SkillSlotUI ui = null;
        if (skillBar != null && skillBar.slotUIs != null && slotIndex >= 0 && slotIndex < skillBar.slotUIs.Length)
            ui = skillBar.slotUIs[slotIndex];

        float timer = 0f;
        float cd = Mathf.Max(0.0001f, slot.skillData.cooldownTime);

        // Immediately show full cooldown (1 -> 0 over time) or the opposite depending on your UI convention.
        // Here: cooldownFill.fillAmount = normalized (1 = full cooldown, 0 = ready)
        if (ui != null) ui.UpdateCooldown(1f); // fill = 1 khi bắt đầu cooldow

        while (timer < cd)
        {
            timer += Time.deltaTime;
            float normalized = 1f - (timer / cd); // giảm dần từ 1 → 0
            if (ui != null) ui.UpdateCooldown(normalized);
            yield return null;
        }

        if (ui != null) ui.UpdateCooldown(1f); // cooldown xong, fill = 0

        slot.isCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
                enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}
