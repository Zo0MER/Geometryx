using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class ExpressionToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeginDraged;
    private CanvasGroup canvasGroup;

    Text label;

    public Action<string> OnTokenChanged;

    public Defines.OperandType operandType;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        label = GetComponentInChildren<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeginDraged = gameObject;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);

        OnTokenChanged = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeginDraged = null;
        canvasGroup.blocksRaycasts = true;
    }

    void Update()
    {
        if (itemBeginDraged && this != itemBeginDraged)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void ChangeToken(string token)
    {
        if(transform.parent)
        {
            Slot slot = transform.parent.GetComponent<Slot>();
            if (slot)
            {
                OnTokenChanged(token);
            }
        }

        label.text = token;
    }
}
