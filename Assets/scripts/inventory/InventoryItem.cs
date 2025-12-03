using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public ItemData itemData;
    public TextMeshProUGUI quantityText;

    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 originalPosition;

    public void SetQuantity(int q)
    {
        if (quantityText != null)
        {
            quantityText.gameObject.SetActive(q > 1);
            quantityText.text = q.ToString();
        }
    }

    public void ReturnToOldSlot()
    {
        transform.SetParent(parentAfterDrag);
        transform.localPosition = originalPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        // ⭐ Nếu item đang ở EquipmentSlot → gọi Unequip ⭐
        EquipmentSlot eq = parentAfterDrag.GetComponent<EquipmentSlot>();
        if (eq != null)
        {
            eq.Unequip();
        }
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // Nếu parent vẫn là root → không drop vào slot hợp lệ → quay lại slot cũ
        if (transform.parent == transform.root)
        {
            ReturnToOldSlot();
        }
        else
        {
            // Khi drop thành công vào slot, luôn set vị trí về center
            transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemData.itemType == ItemType.NormalItem)
            {
                PlayerStats.instance.currentHealth += itemData.value;
                PlayerStats.instance.currentHealth =
                    Mathf.Min(PlayerStats.instance.currentHealth, PlayerStats.instance.maxHealth);

               
            }
        }
    }
}
