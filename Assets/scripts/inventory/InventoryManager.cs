using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("UI Slots")]
    public List<GameObject> inventorySlots;

    [Header("Equipment Slots")]
    public EquipmentSlot helmetSlot;
    public EquipmentSlot chestSlot;
    public EquipmentSlot legsSlot;
    public EquipmentSlot weaponSlot;
    public EquipmentSlot shieldSlot;
    public EquipmentSlot ringSlot;
    public EquipmentSlot necklaceSlot;

    [Header("Prefabs")]
    public GameObject inventoryItemPrefab;

    public RectTransform inventoryPanel;

    [System.Serializable]
    public class InventoryEntry
    {
        public GameObject slot;
        public ItemData data;
        public int quantity;
    }

    public List<InventoryEntry> items = new();

    public void AddItem(ItemData itemData)
    {
        if (itemData.isStackable)
        {
            foreach (var entry in items)
            {
                if (entry.data == itemData)
                {
                    entry.quantity++;
                    entry.slot.GetComponentInChildren<InventoryItem>().SetQuantity(entry.quantity);
                    return;
                }
            }
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                GameObject obj = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItem itemUI = obj.GetComponent<InventoryItem>();

                itemUI.itemData = itemData;
                itemUI.image.sprite = itemData.icon;
                itemUI.SetQuantity(1);

                items.Add(new InventoryEntry
                {
                    slot = slot,
                    data = itemData,
                    quantity = 1
                });

                return;
            }
        }
        Debug.LogWarning("Inventory full!");
    }

    public void UpdateSlot(InventoryItem item, GameObject newSlot)
    {
        foreach (var e in items)
        {
            if (e.data == item.itemData)
            {
                e.slot = newSlot;
                return;
            }
        }
    }

    public void DropItem(InventoryItem item)
    {
        foreach (var entry in items)
        {
            if (entry.data == item.itemData)
            {
                items.Remove(entry);
                break;
            }
        }

        Destroy(item.gameObject);
    }
}
