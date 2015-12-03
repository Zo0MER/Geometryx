using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LayoutElement))]
public class Placeholder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    LayoutElement layout;
    float minWidth;
    public float scaleTime;

    IEnumerator ScaleToWidth(float newWidth)
    {
        for (float i = 0; i < scaleTime; i += Time.deltaTime )
        {
            layout.minWidth = Mathf.SmoothStep(minWidth, newWidth, i / scaleTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ScaleBack()
    {
        float currentWidth = layout.minWidth;
        for (float i = 0; i < scaleTime; i += Time.deltaTime)
        {
            layout.minWidth = Mathf.SmoothStep(currentWidth, minWidth, i / scaleTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartScaleToWidth(float newWidth)
    {
        StartCoroutine(ScaleToWidth(newWidth));
    }
    public void StartScaleBack()
    {
        StartCoroutine(ScaleBack());
    }

	// Use this for initialization
	void Awake () {
        layout = GetComponent<LayoutElement>();
        minWidth = layout.minWidth;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void ReplaceYourselfWidth(GameObject piece)
    {
        piece.transform.SetParent(transform.parent);
        piece.transform.SetSiblingIndex(transform.GetSiblingIndex());
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && DragHandler.itemBeginDraged.GetComponent<LayoutElement>())
        {
            LayoutElement layoutDropped = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
            StartScaleToWidth(layoutDropped.minWidth);
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null)
        {
            StartScaleBack();
        }
    }

    public void OnDrop(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null)
        {
            ReplaceYourselfWidth(DragHandler.itemBeginDraged);
        }
    }
}
