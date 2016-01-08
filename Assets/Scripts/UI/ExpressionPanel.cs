using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class ExpressionPanel : MonoBehaviour
{
    public GameObject slot;

    public Action<string> OnExpressionChanged;

    Dictionary<Defines.OperandType, List<Defines.OperandType>> syntaxMatch = new Dictionary<Defines.OperandType, List<Defines.OperandType>>()
    {
        {Defines.OperandType.Variable,
            new List<Defines.OperandType>() {Defines.OperandType.OpenBracket, Defines.OperandType.Operator, Defines.OperandType.None} },
        {Defines.OperandType.Operator,
            new List<Defines.OperandType>() {Defines.OperandType.Variable} },
        {Defines.OperandType.OpenBracket,
            new List<Defines.OperandType>() {Defines.OperandType.Variable, Defines.OperandType.Function,
                Defines.OperandType.OpenBracket, Defines.OperandType.Operator} },
        {Defines.OperandType.CloseBracket,
            new List<Defines.OperandType>() {Defines.OperandType.Variable, Defines.OperandType.CloseBracket} },
        {Defines.OperandType.None,
            new List<Defines.OperandType>() {Defines.OperandType.CloseBracket, Defines.OperandType.Variable} },
        {Defines.OperandType.Function,
            new List<Defines.OperandType>() {Defines.OperandType.OpenBracket, Defines.OperandType.Operator, Defines.OperandType.None } },

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
        newSlot.GetComponent <Slot>().Open(width);
        newSlot.GetComponent<Slot>().isCanBeEmpty = isCanBeEmpty;
        return newSlot;
    }

    public void UpdateExpression()
    {
        Stack<ExpressionParenthesis> opBraketStack = new Stack<ExpressionParenthesis>();
        List<Slot> slotsToClose = new List<Slot>();
        foreach (Transform slot in transform)
        {
            if (!slot.GetComponent<Slot>().IsEmpty())
            {
                var token = slot.GetComponentInChildren<ExpressionToken>();
                if (token.operandType == Defines.OperandType.OpenBracket)
                {
                    opBraketStack.Push((ExpressionParenthesis)token);
                }
                if (token.operandType == Defines.OperandType.CloseBracket && opBraketStack.Count > 0)
                {
                    var opBracket = opBraketStack.Pop();
                    var clBracket = (ExpressionParenthesis) token;
                    opBracket.PairParenthesis = clBracket;
                    clBracket.PairParenthesis = opBracket;
                }
                if (token.operandType == Defines.OperandType.Operator)
                {
                    if (slot.GetSiblingIndex() == 0)
                    {
                        AddSlot(siblingIndex: slot.GetSiblingIndex(), isCanBeEmpty: true);
                    }
                    else
                    {
                        Slot leftSlot = transform.GetChild(slot.GetSiblingIndex() - 1).GetComponent<Slot>();
                        if (!leftSlot.IsEmpty())
                        {
                            var leftToken = leftSlot.GetComponentInChildren<ExpressionToken>();
                            if (leftToken.operandType != Defines.OperandType.Variable &&
                                leftToken.operandType != Defines.OperandType.CloseBracket)
                            {
                                AddSlot(siblingIndex: slot.GetSiblingIndex(), isCanBeEmpty: true);
                            }
                        }

                    }
                }
                if (token.operandType == Defines.OperandType.Variable ||
                    token.operandType == Defines.OperandType.Function)
                {
                    if (slot.GetSiblingIndex() > 0)
                    {
                        Slot leftSlot = transform.GetChild(slot.GetSiblingIndex() - 1).GetComponent<Slot>();
                        if (!leftSlot.IsEmpty())
                        {
                            var leftToken = leftSlot.GetComponentInChildren<ExpressionToken>();
                            if (leftToken.operandType != Defines.OperandType.OpenBracket &&
                                leftToken.operandType != Defines.OperandType.Operator)
                            {
                                AddSlot(siblingIndex: slot.GetSiblingIndex(), isCanBeEmpty: true);
                            }
                        }
                    }
                }
                if (token.operandType == Defines.OperandType.OpenBracket)
                {
                    if (slot.GetSiblingIndex() > 0)
                    {
                        Slot leftSlot = transform.GetChild(slot.GetSiblingIndex() - 1).GetComponent<Slot>();
                        if (!leftSlot.IsEmpty())
                        {
                            var leftToken = leftSlot.GetComponentInChildren<ExpressionToken>();
                            if (leftToken.operandType != Defines.OperandType.Function &&
                                leftToken.operandType != Defines.OperandType.Operator)
                            {
                                AddSlot(siblingIndex: slot.GetSiblingIndex(), isCanBeEmpty: true);
                            }
                        }
                    }
                }
                if (token.operandType == Defines.OperandType.CloseBracket)
                {
                    if (slot.GetSiblingIndex() > 0)
                    {
                        Slot leftSlot = transform.GetChild(slot.GetSiblingIndex() - 1).GetComponent<Slot>();
                        if (!leftSlot.IsEmpty())
                        {
                            var leftToken = leftSlot.GetComponentInChildren<ExpressionToken>();
                            if (leftToken.operandType != Defines.OperandType.Variable &&
                                leftToken.operandType != Defines.OperandType.CloseBracket)
                            {
                                AddSlot(siblingIndex: slot.GetSiblingIndex(), isCanBeEmpty: true);
                            }
                        }
                    }
                }
            }
            else
            {
                Slot leftSlot = transform.GetChild(slot.GetSiblingIndex() - 1).GetComponent<Slot>();
                if (leftSlot.IsEmpty())
                {
                    slotsToClose.Add(leftSlot);
                }
            }
        }

        slotsToClose.ForEach(slot => slot.Close());
        slotsToClose.Clear();

        if (transform.childCount > 0)
        {
            Slot lastSlot = transform.GetChild(transform.childCount - 1).GetComponent<Slot>();
            if (!lastSlot.IsEmpty())
            {
                var lastToken = lastSlot.GetComponentInChildren<ExpressionToken>();
                if (lastToken.operandType != Defines.OperandType.CloseBracket &&
                    lastToken.operandType != Defines.OperandType.Variable)
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
