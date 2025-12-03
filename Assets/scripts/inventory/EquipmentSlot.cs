using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public ItemType allowedType;
    public ItemData equippedItem;

    private InventoryItem currentItem; // Item đang ở slot này

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (newItem == null) return;

        // ❌ Kiểm tra loại item
        if (newItem.itemData.itemType != allowedType)
        {
            Debug.Log("Sai loại slot!");
            newItem.ReturnToOldSlot();
            return;
        }

        // Nếu slot đã có item → chỉ cho hoán đổi
        if (currentItem != null)
        {
            // Nếu cùng item → trả về slot cũ
            if (currentItem.itemData.itemName == newItem.itemData.itemName)
            {
                newItem.ReturnToOldSlot();
                return;
            }

            // Hoán đổi item
            Transform oldSlot = newItem.parentAfterDrag;
            currentItem.parentAfterDrag = oldSlot;
            currentItem.transform.SetParent(oldSlot);
            currentItem.transform.localPosition = Vector3.zero;
        }

        // Cập nhật item mới vào slot
        equippedItem = newItem.itemData;
        currentItem = newItem;

        newItem.parentAfterDrag = transform;
        newItem.transform.SetParent(transform);
        newItem.transform.localPosition = Vector3.zero;

        // Cập nhật stats
        PlayerStats.instance.AddEquipmentStats(equippedItem);

        // Weapon show
        if (allowedType == ItemType.Weapon)
            FindObjectOfType<WeaponRenderer>().EquipWeapon(equippedItem);
    }

    public void Unequip()
    {
        if (equippedItem == null) return;

        PlayerStats.instance.RemoveEquipmentStats(equippedItem);

        if (allowedType == ItemType.Weapon)
            FindObjectOfType<WeaponRenderer>().UnequipWeapon();

        currentItem = null;
        equippedItem = null;
    }
}
