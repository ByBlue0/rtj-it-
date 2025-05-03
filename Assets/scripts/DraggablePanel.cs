using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DraggablePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 pointerOffset;

    [Header("DOTween Ayarlarý")]
    public float tweenSpeed = 0.1f;
    public Ease tweenEase = Ease.OutQuad;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePos
        );

        pointerOffset = rectTransform.anchoredPosition - localMousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition))
        {
            Vector2 targetPos = localPointerPosition + pointerOffset;
            rectTransform.DOKill(); // önceki tween’i iptal et
            rectTransform.DOAnchorPos(targetPos, tweenSpeed).SetEase(tweenEase);
        }
    }
}
