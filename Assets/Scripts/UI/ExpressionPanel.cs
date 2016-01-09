using System;
using System.Collections.Generic;
using System.Linq;
using Defines;
using UnityEngine;

public class ExpressionPanel : MonoBehaviour
{
    public GameObject slot;

    public Action<string> OnExpressionChanged;

    readonly Dictionary<OperandType, List<OperandType>> syntaxMatch = new Dictionary<OperandType, List<OperandType>>
    {
        { OperandType.Variable, new List<OperandType> {OperandType.OpenBracket, OperandType.Operator,
            OperandType.None} },
        {OperandType.Operator, new List<OperandType> {OperandType.Variable} },
        {OperandType.OpenBracket, new List<OperandType> { OperandType.Function, OperandType.OpenBracket,
            OperandType.Operator, OperandType.None} },
        {OperandType.CloseBracket, new List<OperandType> {OperandType.Variable, OperandType.CloseBracket} },
        {OperandType.None, new List<OperandType> {OperandType.CloseBracket, OperandType.Variable, OperandType.Empty } },
        {OperandType.Function, new List<OperandType> {OperandType.OpenBracket, OperandType.Operator,
            OperandType.None } },
        {OperandType.Empty, new List<OperandType> { OperandType.OpenBracket, OperandType.Operator, OperandType.None,
            OperandType.Function, OperandType.CloseBracket, OperandType.Variable} }

    };

    void Start()
    {
        UpdateExpression();
    }

    public GameObject AddSlot(float width = 15.0f, int siblingIndex = 0, bool scale = false, bool isCanBeEmpty = false)
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
                if (token.operandType == OperandType.OpenBracket)
                {
                    opBraketStack.Push((ExpressionParenthesis)token);
                }
                if (token.operandType == OperandType.CloseBracket && opBraketStack.Count > 0)
                {
                    ((ExpressionParenthesis)token).PairParenthesis = opBraketStack.Pop();
                }

                OperandType tokenToTheLeftType = OperandType.None;
                if (slot.transform.GetSiblingIndex() != 0)
                {
                    Slot slotToTheLeft = transform.GetChild(slot.transform.GetSiblingIndex() - 1).GetComponent<Slot>();
                    if (slotToTheLeft.IsEmpty())
                    {
                        tokenToTheLeftType = OperandType.Empty;
                    }
                    else
                    {
                        var tokenToTheLeft = slotToTheLeft.GetComponentInChildren<ExpressionToken>();
                        tokenToTheLeftType = tokenToTheLeft.operandType;
                    }
                }

                if (!syntaxMatch[token.operandType].Contains(tokenToTheLeftType))
                {
                    AddSlot(siblingIndex: slot.transform.GetSiblingIndex(), isCanBeEmpty: true);
                }
            }
            else
            {
                OperandType tokenToTheLeftType = OperandType.None;
                OperandType tokenToTheRightType = OperandType.None;
                if (slot.transform.GetSiblingIndex() > 0)
                {
                    Slot leftSlot = transform.GetChild(slot.transform.GetSiblingIndex() - 1).GetComponent<Slot>();
                    if (leftSlot.IsEmpty())
                    {
                        tokenToTheLeftType = OperandType.Empty;
                    }
                    else
                    {
                        ExpressionToken token = leftSlot.GetComponentInChildren<ExpressionToken>();
                        tokenToTheLeftType = token.operandType;
                    }
                }
                if (slot.transform.GetSiblingIndex() + 1 < transform.childCount)
                {
                    Slot leftSlot = transform.GetChild(slot.transform.GetSiblingIndex() + 1).GetComponent<Slot>();
                    if (leftSlot.IsEmpty())
                    {
                        tokenToTheRightType = OperandType.Empty;
                    }
                    else
                    {
                        ExpressionToken token = leftSlot.GetComponentInChildren<ExpressionToken>();
                        tokenToTheRightType = token.operandType;
                    }
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
                var token = lastSlot.GetComponentInChildren<ExpressionToken>();
                if (!syntaxMatch[OperandType.None].Contains(token.operandType))
                {
                    AddSlot(siblingIndex: transform.childCount, isCanBeEmpty: true);
                }
            }
        }


        if (IsValidExpression() && OnExpressionChanged != null)
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
        return (from Transform child in transform select child.GetComponent<Slot>()).All(slot => !slot.IsEmpty());
    }
}
