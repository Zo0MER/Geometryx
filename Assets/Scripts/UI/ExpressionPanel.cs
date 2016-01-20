using System;
using System.Collections.Generic;
using System.Linq;
using Defines;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExpressionPanel : MonoBehaviour
{
    public GameObject slot;

    public Action<string> OnExpressionChanged;

    readonly Dictionary<OperandType, List<OperandType>> syntaxMatch = new Dictionary<OperandType, List<OperandType>>
    {
        { OperandType.Variable, new List<OperandType> {OperandType.OpenBracket, OperandType.Operator,
            OperandType.None, OperandType.Empty} },
        {OperandType.Operator, new List<OperandType> {OperandType.Variable, OperandType.Empty, OperandType.CloseBracket } },
        {OperandType.OpenBracket, new List<OperandType> { OperandType.Function, OperandType.OpenBracket,
            OperandType.Operator, OperandType.None, OperandType.Empty} },
        {OperandType.CloseBracket, new List<OperandType> {OperandType.Variable, OperandType.CloseBracket,
            OperandType.Empty} },
        {OperandType.None, new List<OperandType> { } },
        {OperandType.Function, new List<OperandType> {OperandType.OpenBracket, OperandType.Operator,
            OperandType.None, OperandType.Empty } },
        {OperandType.Empty, new List<OperandType> { OperandType.OpenBracket, OperandType.Operator, OperandType.None,
            OperandType.Function, OperandType.CloseBracket, OperandType.Variable, OperandType.Empty} }

    };

    void Start()
    {
        UpdateExpression();
    }

    public GameObject AddSlot(float width = 53.0f, int siblingIndex = 0, bool scale = false, bool isCanBeEmpty = false)
    {
        if (transform.GetChild(Mathf.Max(siblingIndex - 1, 0)).GetComponent<Slot>().IsEmpty())
        {
            return null;
        }

        GameObject newSlot = Instantiate(slot);
        newSlot.transform.SetParent(transform);
        newSlot.transform.SetSiblingIndex(siblingIndex);
        newSlot.GetComponent<Slot>().Open(width);
        newSlot.GetComponent<Slot>().isPermament = isCanBeEmpty;
        return newSlot;
    }

    public void UpdateExpression()
    {
        Stack<ExpressionParenthesis> opBraketStack = new Stack<ExpressionParenthesis>();
        List<Slot> slots = (from Transform slotTransform in transform select slotTransform.GetComponent<Slot>()).ToList();
        slots.RemoveAll(slot => slot.IsEmpty() && slot.IsClosing);
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty())
            {
                var token = slot.GetComponentInChildren<ExpressionToken>();
                if (slot.GetOperandType() == OperandType.OpenBracket)
                {
                    opBraketStack.Push((ExpressionParenthesis)token);
                }
                if (slot.GetOperandType() == OperandType.CloseBracket && opBraketStack.Count > 0)
                {
                    var bracket = opBraketStack.Pop();
                    ((ExpressionParenthesis)token).PairParenthesis = bracket;
                    bracket.PairParenthesis = ((ExpressionParenthesis) token);
                }

                OperandType tokenToTheLeftType = OperandType.None;
                if (slot.transform.GetSiblingIndex() != 0)
                {
                    Slot leftSlot = transform.GetChild(slot.transform.GetSiblingIndex() - 1).GetComponent<Slot>();
                    tokenToTheLeftType = leftSlot.GetOperandType();
                }

                if (!syntaxMatch[slot.GetOperandType()].Contains(tokenToTheLeftType))
                {
                    AddSlot(siblingIndex: slot.transform.GetSiblingIndex(), isCanBeEmpty: true);
                }
            }
            else
            {
                OperandType tokenToTheLeftType = OperandType.None;
                OperandType tokenToTheRightType = OperandType.None;
                for (int i = slot.transform.GetSiblingIndex() - 1; i >= 0; i--)
                {
                    Slot leftSlot = transform.GetChild(i).GetComponent<Slot>();
                    tokenToTheLeftType = leftSlot.GetOperandType();
                    if (tokenToTheLeftType != OperandType.Empty)
                    {
                        break;
                    }
                }
                if (slot.transform.GetSiblingIndex() + 1 < transform.childCount)
                {
                    Slot rightSlot = transform.GetChild(slot.transform.GetSiblingIndex() + 1).GetComponent<Slot>();
                    tokenToTheRightType = rightSlot.GetOperandType();
                }

                if (syntaxMatch[tokenToTheRightType].Contains(tokenToTheLeftType))
                {
                    slot.Close();
                }
                
            }
        }

        if (transform.childCount > 0)
        {
            Slot lastSlot = transform.GetChild(transform.childCount - 1).GetComponent<Slot>();
            if (!lastSlot.IsEmpty())
            {
                if (!syntaxMatch[OperandType.None].Contains(lastSlot.GetOperandType()))
                {
                    AddSlot(siblingIndex: transform.childCount, isCanBeEmpty: true);
                }
            }
        }


        if (OnExpressionChanged != null)
        {
            OnExpressionChanged(GetMathExpression());
        }
    }

    public string GetMathExpression()
    {
        return transform.Cast<Transform>().Select(child => child.GetComponentInChildren<ExpressionToken>()).
            Where(text => text).Aggregate("", (current, text) => current + text.Value());
    }

    public bool IsValidExpression()
    {
        return (from Transform child in transform select child.GetComponent<Slot>()).All(slot => !slot.IsEmpty() 
            || (slot.IsEmpty() && slot.IsClosing));
    }
}
