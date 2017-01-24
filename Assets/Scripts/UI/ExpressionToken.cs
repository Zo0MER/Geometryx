using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEditor;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LayoutElement))]
public class ExpressionToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeginDraged;
    private CanvasGroup canvasGroup;

    Text label;

    public string value;

    public Action<string> OnTokenChanged;

    public Defines.OperandType operandType;

    public StockPanel stockPanel;

    private LayoutElement layout;
    private RectTransform rectTransform;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        label = GetComponentInChildren<Text>();
        layout = GetComponent<LayoutElement>();
        stockPanel = FindObjectOfType<StockPanel>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Slot slot = GetComponentInParent<Slot>();
        itemBeginDraged = gameObject;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);

        float currentSize = layout.preferredWidth;
        var leen = LeanTween.value(gameObject, (x) =>
        {
            transform.localScale = new Vector2(x, x);
        },
            1, 1.1f, 0.2f);
        leen.setEase(LeanTweenType.easeOutElastic);

        OnTokenChanged = null;

        if (slot)
        {
            slot.OnTokenRemove(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var leen = LeanTween.value(gameObject, (x) =>
        {
            transform.localScale = new Vector2(x, x);
        },
            1.1f, 1, 0.2f);
        leen.setEase(LeanTweenType.easeOutElastic);

        itemBeginDraged = null;
        canvasGroup.blocksRaycasts = true;
        if (!IsInSlot())
        {
            MoveInStock();
        }
    }

    public virtual void MoveInStock()
    {
        float currentSize = layout.preferredWidth;
        var leen = LeanTween.value(gameObject, (x) =>
        {
            transform.localScale = new Vector2(x, x);
        },
            1, 0.0f, 0.2f);
        leen.onComplete += () => transform.SetParent(stockPanel.transform);
        leen = LeanTween.value(gameObject, (x) =>
        {
            transform.localScale = new Vector2(x, x);
            layout.preferredWidth = x * currentSize;
        },
            0.0f, 1, 0.5f);
        leen.delay = 0.2f;
        leen.setEase(LeanTweenType.easeOutElastic);
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

    public void ChangeValue(string token, string _value = "", bool callUpdate = true)
    {

        Slot slot = GetComponentInParent<Slot>();
        if (callUpdate && slot != null && OnTokenChanged != null)
        {
            value = _value;
            OnTokenChanged(token);
        }

        label.text = token;
    }

    public string Value()
    {
        if (string.IsNullOrEmpty(value))
        {
            return label.text.Replace("\n", "");
        }
        return value;
    }

    public virtual void OnDroppedInSlot(Slot slot)
    {
        OnTokenChanged += (x => slot.ParentPanel.UpdateExpression());
    }

    public bool IsInSlot()
    {
        return GetComponentInParent<Slot>() != null;
    }
}
