using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Defines;

[RequireComponent(typeof(LayoutElement))]
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    LayoutElement layout;
    float preferredWidth;
    public float scaleTime = 0.2f;
    public float closeScaleTime = 0.2f;

    public bool isPermament = false;

    ExpressionPanel parentPanel;

    public float openScaleTime;

    private SlotState currentState = SlotState.None;

    public ExpressionPanel ParentPanel
    {
        get { return parentPanel; }
    }

    public bool IsClosing
    {
        get { return currentState == SlotState.Closing; }
    }

    public SlotState CurrentState
    {
        get { return currentState; }
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
        if (!IsEmpty())
        {
            OnTokenDrop(transform.GetChild(0).gameObject);
        }
    }

    public void ScaleToWidth(float width)
    {
        if (currentState != SlotState.None)
            return;
        currentState = SlotState.ScalingUp;
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, preferredWidth, width, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.onComplete += () => currentState = SlotState.None;
    }
    public void ScaleBack()
    {
        if (currentState != SlotState.None)
            return;
        currentState = SlotState.ScalingBack;
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, preferredWidth, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.onComplete += () => currentState = SlotState.None;
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    void OnTokenDrop(GameObject token)
    {
        token.transform.SetParent(transform);
        LeanTween.moveLocal(token, Vector3.zero, 0.1f);

        var expressionToken = token.GetComponent<ExpressionToken>();
        expressionToken.OnDroppedInSlot(this);

        isPermament = true;

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
            if (!isPermament)
            {
                parentPanel.UpdateExpression();
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
        if (currentState != SlotState.None)
            return;

        currentState = SlotState.Closing;
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, 0, closeScaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.destroyOnComplete = true;
    }

    public void Open(float width)
    {
        if (currentState != SlotState.None)
            return;

        currentState = SlotState.Opening;
        preferredWidth = width;
        var leen = LeanTween.value(gameObject, (x) => layout.preferredWidth = x, 0, width, openScaleTime)
            .setEase(LeanTweenType.easeOutCubic);
        leen.onComplete += () => currentState = SlotState.None;

        leen.onComplete += () =>
        {
            if (!IsEmpty())
            {
                var token = GetComponentInChildren<ExpressionToken>();
                LeanTween.moveLocal(token.gameObject, Vector3.zero, 0.1f);
            }
        };
    }


    public Defines.OperandType GetOperandType()
    {
        if (IsEmpty())
        {

            return Defines.OperandType.Empty;
        }

        var token = GetComponentInChildren<ExpressionToken>();
        return token.operandType;
    }

    void Update()
    {
        if (!isPermament && IsEmpty())
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Close();
            }
        }
    }
}
