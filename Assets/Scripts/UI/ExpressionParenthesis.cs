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
            SwapBrackets();
        }
        base.OnDroppedInSlot(slot);
    }

    void SwapBrackets()
    {
        pairParenthesis.ChangeValue(Value() == ")" ? ")": "(", false);
        pairParenthesis.operandType = operandType == Defines.OperandType.CloseBracket ? 
            Defines.OperandType.CloseBracket : Defines.OperandType.OpenBracket;
        ChangeValue(Value() == ")" ? "(" : ")", false);
        operandType = operandType == Defines.OperandType.CloseBracket ?
             Defines.OperandType.OpenBracket :  Defines.OperandType.CloseBracket;
    }

    public override void MoveInStock()
    {
        base.MoveInStock();
    }
}
