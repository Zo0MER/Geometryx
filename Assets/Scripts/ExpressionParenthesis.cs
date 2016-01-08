using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpressionParenthesis : ExpressionToken
{

    ExpressionParenthesis pairParenthesis;

    public ExpressionParenthesis PairParenthesis
    {
        set { pairParenthesis = value; }
    }

    public override void OnDroppedInSlot(Slot slot)
    {
        var opBracketIndex = Value() == "(" ? slot.transform.GetSiblingIndex()  
            : pairParenthesis.transform.parent.transform.GetSiblingIndex();
        var clBracketIndex = Value() == ")" ? slot.transform.GetSiblingIndex()
            : pairParenthesis.transform.parent.transform.GetSiblingIndex();
        if (opBracketIndex > clBracketIndex)
        {
            SwapSide();
        }
        base.OnDroppedInSlot(slot);
    }

    void SwapSide()
    {
        pairParenthesis.ChangeToken(Value() == ")" ? ")": "(");
        pairParenthesis.operandType = operandType == Defines.OperandType.CloseBracket ? 
            Defines.OperandType.CloseBracket : Defines.OperandType.OpenBracket;
        ChangeToken(Value() == ")" ? "(" : ")");
        operandType = operandType == Defines.OperandType.CloseBracket ?
             Defines.OperandType.OpenBracket :  Defines.OperandType.CloseBracket;
    }
}
