using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeginDraged;
    private Vector3 startPosition;
    private GameObject startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeginDraged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeginDraged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (!transform.parent.gameObject.GetComponent<GridLayoutGroup>())
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform);
        }
    }


}
