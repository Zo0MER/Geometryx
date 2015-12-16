﻿using UnityEngine;
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
            //dragHandler.OnBeginDragEvent += ReplaceWithPlaceHolder;
        }
    }

    void ReplaceWithPlaceHolder(DragHandler obj)
    {
        AddSlot(obj.GetComponent<LayoutElement>().minWidth, obj.transform.GetSiblingIndex());
    }

    public GameObject AddSlot(float width, int siblingIndex = 0, bool scale = false)
    {
        GameObject newPlaceholder = Instantiate(placeHolder);
        newPlaceholder.transform.SetParent(transform);
        newPlaceholder.transform.SetSiblingIndex(siblingIndex);
        if(scale)
        {
            newPlaceholder.GetComponent<Placeholder>().ScaleToWidth(width);
        }
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
