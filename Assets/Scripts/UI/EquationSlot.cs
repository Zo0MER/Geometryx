using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Obsolete("This is an obsolete method")]
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
        if (ExpressionToken.itemBeginDraged != null && item)
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

        if (item == null && ExpressionToken.itemBeginDraged != null)
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

        EquationPiece piece = ExpressionToken.itemBeginDraged.GetComponent<EquationPiece>();
        if (piece)
        {
            piece.PutOnSlot(transform);
        }

        panel.ItemDroped();
    }

}
