using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LayoutElement))]
public class Placeholder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    LayoutElement layout;
    float preferredWidth;
    public float scaleTime = 0.2f;
    public float closeScaleTime = 0.2f;

    public bool isCanBeEmpty = false;

    public void ScaleToWidth(float width)
    {
        LeanTween.value(gameObject, (x) => layout.preferredWidth = x, preferredWidth, width, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
    }
    public void ScaleBack()
    {
        LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, preferredWidth, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
    }

    bool isEmpty()
    {
        return transform.childCount == 0;
    }

    // Use this for initialization
    void Awake()
    {
        layout = GetComponent<LayoutElement>();
        preferredWidth = layout.preferredWidth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetPiece(GameObject piece)
    {
        piece.transform.SetParent(transform);
        //piece.transform.localPosition = Vector3.zero;
        LeanTween.moveLocal(piece, Vector3.zero, 0.1f);
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && DragHandler.itemBeginDraged.GetComponent<LayoutElement>())
        {
            if (isEmpty())
            {
                LayoutElement layoutDropped = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
                ScaleToWidth(layoutDropped.preferredWidth);
            }
            else
            {
                ExpressionPanel panel = transform.parent.GetComponent<ExpressionPanel>();
                if (panel)
                {
                    LayoutElement layoutDropped = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
                    panel.AddSlot(layoutDropped.preferredWidth, transform.GetSiblingIndex(), true);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && isEmpty())
        {
            if (!isCanBeEmpty)
            {
                Close();
            }
            else
            {
                ScaleBack();
            }
        }
    }

    public void OnDrop(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && isEmpty())
        {
            SetPiece(DragHandler.itemBeginDraged);
        }
    }

    public void Close()
    {
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, 0, closeScaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.destroyOnComplete = true;
    }
}
