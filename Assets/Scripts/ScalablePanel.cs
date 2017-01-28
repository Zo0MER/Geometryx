using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class ScalablePanel : MonoBehaviour
{
    public float closeTime = 1f;
    RectTransform rectTransform;

    Vector2 initalSize;
    public Vector2 closedSize = Vector2.zero;
    public Ease openEasing;
    public Ease closeEasing;

    private bool isOpen = true;

    public bool IsOpen
    {
        get { return isOpen; }
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initalSize = rectTransform.sizeDelta;
    }

	public void Close()
	{
	    isOpen = false;
	    transform.DOScale(Vector2.zero, closeTime).SetEase(closeEasing);
	}

    public void Open()
    {
        isOpen = true;
        transform.DOScale(Vector2.one, closeTime).SetEase(openEasing);
    }
}
