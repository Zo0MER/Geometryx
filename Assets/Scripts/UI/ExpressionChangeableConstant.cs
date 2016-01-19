using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ExpressionChangeableConstant : ExpressionToken, IPointerClickHandler
{

    float leftBound = -1.0f;
    float rightBound = 1.0f;

    Slider slider;
    private ScalablePanel sliderPanel;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        sliderPanel = GetComponentInChildren<ScalablePanel>();


        ChangeValue((leftBound + (rightBound - leftBound) * slider.value).ToString("#0.00"));

        slider.onValueChanged.AddListener(x => ChangeValue((leftBound + (rightBound - leftBound) * x)
            .ToString("#0.00")));

        sliderPanel.Close();
    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (sliderPanel.IsOpen)
        {
            sliderPanel.Close();
        }
        else
        {
            sliderPanel.Open();
        }
    }

    public override void OnDroppedInSlot(Slot slot)
    {
        base.OnDroppedInSlot(slot);
    }
}
