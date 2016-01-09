
using System;

namespace Defines
{
    public enum OperandType
    {
        OpenBracket,
        CloseBracket,
        Operator,
        Function,
        Variable,
        Empty,
        None
    };

    public enum SlotState
    {
        Closing,
        Opening,
        ScalingUp,
        ScalingBack,
        None
    }
}