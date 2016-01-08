using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LayoutElement))]
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    LayoutElement layout;
    float preferredWidth;
    public float scaleTime = 0.2f;
    public float closeScaleTime = 0.2f;

    public bool isCanBeEmpty = false;

    ExpressionPanel parentPanel;

    private bool isClosing = false;
    public float openScaleTime;


    public ExpressionPanel ParentPanel
    {
        get { return parentPanel; }
    }

    // Use this for initialization
    void Awake()
    {
        layout = GetComponent<LayoutElement>();
        preferredWidth = layout.preferredWidth;
    }

    void Start()
    {
        parentPanel = transform.parent.GetComponent<ExpressionPanel>();
    }

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

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    void OnTokenDrop(GameObject token)
    {
        token.transform.SetParent(transform);
        token.transform.localPosition = Vector3.zero;
        LeanTween.moveLocal(token, Vector3.zero, 0.1f);

        var expressionToken = token.GetComponent<ExpressionToken>();
        expressionToken.OnDroppedInSlot(this);

        parentPanel.UpdateExpression();
    }

    public void OnTokenRemove(GameObject token)
    {
        parentPanel.UpdateExpression();
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (ExpressionToken.itemBeginDraged != null && ExpressionToken.itemBeginDraged.GetComponent<LayoutElement>())
        {
            if (IsEmpty())
            {
                LayoutElement layoutDropped = ExpressionToken.itemBeginDraged.GetComponent<LayoutElement>();
                ScaleToWidth(layoutDropped.preferredWidth);
            }
            else
            {
                if (parentPanel)
                {
                    LayoutElement layoutDropped = ExpressionToken.itemBeginDraged.GetComponent<LayoutElement>();
                    parentPanel.AddSlot(layoutDropped.preferredWidth, transform.GetSiblingIndex(), true);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (ExpressionToken.itemBeginDraged != null && IsEmpty())
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
        if (ExpressionToken.itemBeginDraged != null && IsEmpty())
        {
            OnTokenDrop(ExpressionToken.itemBeginDraged);
        }
    }

    public void Close()
    {
        if (isClosing)
            return;

        isClosing = true;
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, 0, closeScaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.destroyOnComplete = true;
    }

    public void Open(float width)
    {
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, 0, width, openScaleTime)
            .setEase(LeanTweenType.easeOutCubic);
    }

}
