using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpressionPanel : MonoBehaviour , IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject placeHolder;

    GameObject AddPlaceholder()
    {
        GameObject newPlaceholder = Instantiate(placeHolder);
        newPlaceholder.transform.SetParent(transform);
        return newPlaceholder;
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null)
        {
            GameObject placeholderObj = AddPlaceholder();
            Placeholder placeholder = placeholderObj.GetComponent<Placeholder>();
            LayoutElement layout = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
            placeholder.StartScaleToWidth(layout.minWidth + 1.0f);
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
    }

    public void OnDrop(PointerEventData pointerData)
    {
        
    }

}
