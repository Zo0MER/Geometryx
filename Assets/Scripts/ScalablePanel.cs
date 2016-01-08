using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ScalablePanel : MonoBehaviour
{
    public float closeTime = 1f;
    RectTransform rectTransform;

    Vector2 initalSize;
    public Vector2 closedSize = Vector2.zero;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initalSize = rectTransform.sizeDelta;
    }

	public void Close()
    {
        LeanTween.value(gameObject, (x) => rectTransform.sizeDelta = x, initalSize, closedSize, closeTime)
           .setEase(LeanTweenType.easeOutCubic);
    }

    public void Open()
    {
        LeanTween.value(gameObject, (x) => rectTransform.sizeDelta = x, closedSize, initalSize, closeTime)
           .setEase(LeanTweenType.easeOutCubic);
    }
}
