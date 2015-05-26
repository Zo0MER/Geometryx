using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EquationPiece : MonoBehaviour 
{
    public bool isInEquation = false;

    public void PutOnSlot(Transform panel)
    {
        transform.SetParent(panel);
        isInEquation = true;
    }
}
