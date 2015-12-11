using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpressionPanel : MonoBehaviour , IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject placeHolder;
    
    public void Start()
    {
        foreach(DragHandler dragHandler in GetComponentsInChildren<DragHandler>())
        {
            dragHandler.OnBeginDragEvent += ReplaceWithPlaceHolder;
        }
    }

    void ReplaceWithPlaceHolder(DragHandler obj)
    {
        AddPlaceholder(obj.GetComponent<LayoutElement>().minWidth, obj.transform.GetSiblingIndex());
    }

    GameObject AddPlaceholder(float width, int siblingIndex = 0)
    {
        GameObject newPlaceholder = Instantiate(placeHolder);
        newPlaceholder.transform.SetParent(transform);
        newPlaceholder.transform.SetSiblingIndex(siblingIndex);
        newPlaceholder.GetComponent<LayoutElement>().minWidth = width;
        newPlaceholder.GetComponent<Placeholder>().Close();
        return newPlaceholder;
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        //if (DragHandler.itemBeginDraged != null)
        //{
        //    GameObject placeholderObj = AddPlaceholder();
        //    Placeholder placeholder = placeholderObj.GetComponent<Placeholder>();
        //    LayoutElement layout = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
        //    placeholder.StartScaleToWidth(layout.minWidth + 1.0f);
        //}
    }


    public void OnPointerExit(PointerEventData pointerData)
    {
    }

    public void OnDrop(PointerEventData pointerData)
    {
        
    }

}
