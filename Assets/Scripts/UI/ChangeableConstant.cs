using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ExpressionToken))]
public class ChangeableConstant : MonoBehaviour {

    float leftBound = -1.0f;
    float rightBound = 1.0f;

    Slider slider;
    ExpressionToken token;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        token = GetComponent<ExpressionToken>();

        token.ChangeValue((leftBound + (rightBound - leftBound) * slider.value).ToString("#0.00"));

        slider.onValueChanged.AddListener(x => token.ChangeValue((leftBound + (rightBound - leftBound) * x).ToString("#0.00")));
    }

}
