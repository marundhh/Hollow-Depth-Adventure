using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlotUI : MonoBehaviour, IDropHandler
{
    [Header("UI Elements")]
    public Image skillIcon;        // hình chính
    public Image cooldownFill;     // image fill: 0 = ready, 1 = full cooldown

    [Header("References")]
    public SkillBarManager manager; // gán trong inspector
    public int slotIndex = 0;       // index ô (0..4)

    private void Start()
    {
        if (cooldownFill != null)
            cooldownFill.fillAmount = 1f; // lúc đầu full cooldown
        RefreshUI();
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillDraggable dragged = eventData.pointerDrag?.GetComponent<SkillDraggable>();
        if (dragged == null || dragged.skillData == null) return;

        manager.AssignSkillToSlot(slotIndex, dragged.skillData);
    }

    // Cập nhật UI icon
    public void RefreshUI()
    {
        var slot = manager.GetSlot(slotIndex);
        if (slot != null && slot.skillData != null && skillIcon != null)
        {
            skillIcon.sprite = slot.skillData.skillIcon;
            skillIcon.enabled = true;
        }
        else if (skillIcon != null)
        {
            skillIcon.sprite = null;
            skillIcon.enabled = false;
        }
    }

    // normalized: 0 = ready, 1 = full cooldown
    public void UpdateCooldown(float normalized)
    {
        if (cooldownFill != null)
            cooldownFill.fillAmount = normalized;
    }
}
