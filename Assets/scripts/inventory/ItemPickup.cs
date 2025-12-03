using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    [Header("Pickup Content")]
    public ItemData itemData;           // Item bình thường
    [Tooltip("Số lượng nếu stackable")]
    public int itemAmount = 1;

    [Header("Input")]
    public KeyCode pickupKey = KeyCode.F;

    private bool isPlayerNearby = false;

    void Update()
    {
        // Nhấn phím để nhặt
        if (isPlayerNearby && Input.GetKeyDown(pickupKey))
        {
            PickupItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = false;
    }

    private void PickupItem()
    {
        if (itemData == null || InventoryManager.instance == null) return;

        // Nếu item stackable, thêm số lượng; nếu không, thêm từng item
        for (int i = 0; i < itemAmount; i++)
        {
            InventoryManager.instance.AddItem(itemData);
        }

        // Xóa object trên map
        Destroy(gameObject);
    }
}
