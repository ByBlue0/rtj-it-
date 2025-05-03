using UnityEngine;
using DG.Tweening;

public class DraggablePanelOpener : MonoBehaviour
{
    public GameObject panelToOpen;
    public float animationDuration = 0.3f;

    private Vector3 originalScale;
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    // Çift týklama için zamanlama
    private float lastClickTime = 0f;
    public float doubleClickThreshold = 0.3f;

    private void Start()
    {
        mainCamera = Camera.main;

        originalScale = panelToOpen.transform.localScale;
        panelToOpen.transform.localScale = Vector3.zero;
        panelToOpen.SetActive(false);
    }

    private void OnMouseDown()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        // Çift týklama kontrolü
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            TogglePanel();
        }
        else
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
            isDragging = true;
        }

        lastClickTime = Time.time;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;

            transform.DOMove(targetPos, 0.05f).SetEase(Ease.OutQuad);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void ClosePanel()
    {
        if (!panelToOpen.activeSelf) return;

        panelToOpen.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            panelToOpen.SetActive(false);
        });
    }


    private void TogglePanel()
    {
        if (panelToOpen.activeSelf)
        {
            ClosePanel();
        }
        else
        {
            panelToOpen.SetActive(true);
            panelToOpen.transform.localScale = Vector3.zero;
            panelToOpen.transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBack);
        }
    }
}
