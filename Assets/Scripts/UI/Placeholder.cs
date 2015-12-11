using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LayoutElement))]
public class Placeholder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    LayoutElement layout;
    float preferredWidth;
    public float scaleTime = 0.2f;
    public float closeScaleTime = 0.2f;

    public void ScaleToWidth(float width)
    {
        LeanTween.value(gameObject, (x) => layout.preferredWidth = x, preferredWidth, width, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
    }
    public void ScaleBack()
    {
        LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, preferredWidth, scaleTime)
            .setEase(LeanTweenType.easeOutCubic);
    }

    bool isHasPiece()
    {
        return transform.childCount > 0;
    }

    // Use this for initialization
    void Awake () {
        layout = GetComponent<LayoutElement>();
        preferredWidth = layout.preferredWidth;
        DragHandler piece = transform.GetComponentInChildren<DragHandler>();
        if (piece)
        {
            piece.OnBeginDragEvent += OnPieceRemoved;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    void SetPiece(GameObject piece)
    {
        piece.transform.SetParent(transform);
        //piece.transform.localPosition = Vector3.zero;
        LeanTween.moveLocal(piece, Vector3.zero, 0.1f);
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && DragHandler.itemBeginDraged.GetComponent<LayoutElement>() && !isHasPiece())
        {
            LayoutElement layoutDropped = DragHandler.itemBeginDraged.GetComponent<LayoutElement>();
            ScaleToWidth(layoutDropped.preferredWidth);
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null)
        {
            ScaleBack();
        }
    }

    public void OnDrop(PointerEventData pointerData)
    {
        if (DragHandler.itemBeginDraged != null && !isHasPiece())
        {
            SetPiece(DragHandler.itemBeginDraged);
        }
    }

    void OnPieceRemoved(DragHandler piece)
    {

    }

    public void Close()
    {
        LeanTween.value(gameObject, (x) => layout.preferredWidth = x, layout.preferredWidth, 0, closeScaleTime).setEase(LeanTweenType.easeOutCubic);
    }
}
