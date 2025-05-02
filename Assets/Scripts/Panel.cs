using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    public GameObject panelToOpen;
    public float animationDuration = 0.3f;
    private Vector3 originalScale;

    private void Start()
    {
        // Panelin baþlangýçta sahnede olduðu scale'i kaydet
        originalScale = panelToOpen.transform.localScale;

        // Açýlýþta sýfýrlayýp görünmez yap
        panelToOpen.transform.localScale = Vector3.zero;
        panelToOpen.SetActive(false);
    }

    private void OnMouseDown()
    {
        OpenPanel();
    }

    public void OpenPanel()
    {
        panelToOpen.SetActive(true);
        panelToOpen.transform.localScale = Vector3.zero;
        panelToOpen.transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBack);
    }

    public void ClosePanel()
    {
        panelToOpen.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            panelToOpen.SetActive(false);
        });
    }
}
