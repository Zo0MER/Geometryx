using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ExpressionPanel : MonoBehaviour
{
    public GameObject slot;

    public Action<string> OnExpressionChanged;


    void Start()
    {
        UpdateExpression();
    }

    public GameObject AddSlot(float width, int siblingIndex = 0, bool scale = false)
    {
        if (transform.GetChild(Mathf.Max(siblingIndex - 1, 0)).GetComponent<Slot>().isEmpty())
        {
            return null;
        }

        GameObject newSlot = Instantiate(slot);
        newSlot.transform.SetParent(transform);
        newSlot.transform.SetSiblingIndex(siblingIndex);
        if(scale)
        {
            newSlot.GetComponent <Slot>().ScaleToWidth(width);
        }
        return newSlot;
    }

    public void UpdateExpression()
    {
        if (IsValidExpression() && OnExpressionChanged != null)
        {
            OnExpressionChanged(GetMathExpression());
        }
    }

    List<string> GetTokens()
    {
        List<string> tokens = new List<string>(transform.childCount);
        foreach (Transform child in transform)
        {
            var text = child.GetComponentInChildren<Text>();
            tokens.Add(text.text);
        }

        return tokens;
    }

    public string GetMathExpression()
    {
        string expression = "";
        foreach (Transform child in transform)
        {
            var text = child.GetComponentInChildren<Text>();
            if (text)
            {
                expression += text.text.Replace("\n", "");
            }
        }

        return expression;
    }

    public bool IsValidExpression()
    {
        foreach (Transform child in transform)
        {
            var text = child.GetComponentInChildren<Text>();
            if (!text)
            {
                return false;
            }
        }

        return true;
    }
}
