using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExpressionPanel : MonoBehaviour
{
    public GameObject slot;
    
    public void Start()
    {
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

    List<string> GetMathExpression()
    {
        List<string> tokens = new List<string>(transform.childCount);
        foreach (Transform child in transform)
        {

        }

        return tokens;
    }

}
