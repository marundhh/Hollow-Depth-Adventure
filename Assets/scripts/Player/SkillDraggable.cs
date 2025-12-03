using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SkillDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SkillData skillData;      // ScriptableObject chứa info skill

    private Canvas canvas;
    private CanvasGroup originalCanvasGroup;

    private GameObject dragIcon;
    private RectTransform dragRect;
    private CanvasGroup dragCanvasGroup;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        originalCanvasGroup = GetComponent<CanvasGroup>();
        if (originalCanvasGroup == null)
            originalCanvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (skillData == null || skillData.skillIcon == null) return;

        // tạo drag icon tạm
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(canvas.transform, false);
        dragIcon.transform.SetAsLastSibling();

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = skillData.skillIcon;
        img.SetNativeSize();
        img.raycastTarget = false;

        dragCanvasGroup = dragIcon.AddComponent<CanvasGroup>();
        dragCanvasGroup.blocksRaycasts = false;

        dragRect = dragIcon.GetComponent<RectTransform>();
        dragRect.sizeDelta = GetComponent<RectTransform>().sizeDelta;

      

        // đặt ngay tại pointer
        SetDragIconPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            SetDragIconPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);

        
    }

    // chuyển pointer sang local position canvas
    private void SetDragIconPosition(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out pos);
        dragRect.anchoredPosition = pos;
    }
}
