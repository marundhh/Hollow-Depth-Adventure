using UnityEngine;



public class SkillBarManager : MonoBehaviour
{
    [Header("Slots")]
    public SkillSlot[] slots = new SkillSlot[5];

    [Header("UI")]
    public SkillSlotUI[] slotUIs = new SkillSlotUI[5];

    [Header("Gameplay")]
    public AttackTrigger attackTrigger;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
                slots[i] = new SkillSlot();
        }

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slotUIs[i] != null)
            {
                slotUIs[i].manager = this;
                slotUIs[i].slotIndex = i;
                slotUIs[i].RefreshUI();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            KeyCode key = (KeyCode)((int)KeyCode.Alpha1 + i);
            if (Input.GetKeyDown(key))
            {
                ActivateSkill(i);
            }
        }
    }

    public void ActivateSkill(int index)
    {
        if (attackTrigger != null && index >= 0 && index < slots.Length)
        {
            attackTrigger.ActivateSkill(index);
        }
    }

    /// <summary>
    /// Thả skill vào slot
    /// </summary>
    public void AssignSkillToSlot(int targetIndex, SkillData newSkill)
    {
        if (targetIndex < 0 || targetIndex >= slots.Length || newSkill == null) return;

        // tìm nếu skill đang ở slot khác
        int oldIndex = -1;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].skillData == newSkill)
            {
                oldIndex = i;
                break;
            }
        }

        if (oldIndex == targetIndex)
        {
            // kéo vào đúng ô hiện tại → không làm gì
            return;
        }

        // swap nếu targetIndex đã có skill
        SkillData temp = slots[targetIndex].skillData;
        slots[targetIndex].skillData = newSkill;

        if (oldIndex >= 0)
            slots[oldIndex].skillData = temp; // swap

        // nếu skill mới từ bên ngoài list và target slot trống, oldIndex <0 → chỉ gán
        else if (temp != null)
        {
            // nếu target slot có skill → trả lại cho list (hoặc xóa tạm)
            // tuỳ game bạn muốn
        }

        // refresh UI
        RefreshAllSlotUI();
    }

    private void RefreshAllSlotUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slotUIs[i] != null)
                slotUIs[i].RefreshUI();
        }
    }

    public SkillSlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return null;
        return slots[index];
    }
}
