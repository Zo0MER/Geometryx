using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquationPiece : MonoBehaviour 
{
    public bool isInEquation = false;
    private Text text;
    virtual public string Value 
    {
        get { return text.text; }
    }

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void PutOnSlot(Transform panel)
    {
        transform.SetParent(panel);
        isInEquation = true;
    }
}
