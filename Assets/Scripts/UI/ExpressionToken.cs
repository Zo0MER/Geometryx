using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DG.Tweening;
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

    public GameObject stockPanel;

    private LayoutElement layout;
    private RectTransform rectTransform;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        label = GetComponentInChildren<Text>();
        layout = GetComponent<LayoutElement>();
        stockPanel = GameObject.FindGameObjectWithTag("StockPanel");
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Slot slot = GetComponentInParent<Slot>();
        itemBeginDraged = gameObject;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);

        float currentSize = layout.preferredWidth;
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.2f).SetEase(Ease.OutElastic);

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
        transform.localScale = new Vector2(1.1f, 1.1f);
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutElastic);

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
        transform.localScale = Vector3.one;
        var tween = transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutElastic);
        tween.OnComplete(() => transform.SetParent(stockPanel.transform));
        tween = transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        tween.SetDelay(0.2f);
        layout.preferredWidth = 0.0f;
        layout.DOPreferredSize(new Vector2(currentSize, layout.preferredHeight), 0.5f);
        tween.SetDelay(0.2f);
        tween.SetEase(Ease.OutElastic);
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
