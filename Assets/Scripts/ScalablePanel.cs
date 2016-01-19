using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ScalablePanel : MonoBehaviour
{
    public float closeTime = 1f;
    RectTransform rectTransform;

    Vector2 initalSize;
    public Vector2 closedSize = Vector2.zero;
    public LeanTweenType openEasing;
    public LeanTweenType closeEasing;

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
        //LeanTween.value(gameObject, (x) => rectTransform.sizeDelta = x, initalSize, closedSize, closeTime)
        //   .setEase(closeEasing);
        LeanTween.scale(gameObject, Vector2.zero, closeTime).setEase(closeEasing);
	}

    public void Open()
    {
        isOpen = true;
        //LeanTween.value(gameObject, (x) => rectTransform.sizeDelta = x, closedSize, initalSize, closeTime)
        //   .setEase(openEasing);

        LeanTween.scale(gameObject, Vector2.one, closeTime).setEase(openEasing);

    }
}
