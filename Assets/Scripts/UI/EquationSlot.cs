using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquationSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    private EquationPanel panel;
    
    void Start()
    {
        panel = transform.parent.gameObject.GetComponent<EquationPanel>();
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && item)
        {
            panel.MoveItemsToRight(this);
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (item == null)
        {
            panel.MoveItemsToLeft(this);
        }

        if (item == null && DragHandler.itemBeginDraged != null)
        {
            panel.RemoveSlot();
        }

        panel.UpdateFormula();
    }


    public void OnDrop(PointerEventData pointerData)
    {
        if (item)
        {
            return;
        }

        EquationPiece piece = DragHandler.itemBeginDraged.GetComponent<EquationPiece>();
        if (piece)
        {
            piece.PutOnSlot(transform);
        }

        panel.ItemDroped();
    }

}
