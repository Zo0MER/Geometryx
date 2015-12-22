using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class ExpressionToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    public static GameObject itemBeginDraged;
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;

    public Defines.OperandType operandType;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeginDraged = gameObject;
        startPosition = transform.position;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //LeanTween.scale(GetComponent<RectTransform>(), new Vector2(1.2f, 1.2f), 0.3f).setEase(LeanTweenType.easeInBounce);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeginDraged = null;
        canvasGroup.blocksRaycasts = true;
        if (!transform.parent.gameObject.GetComponent<Slot>())
        {
            
        }
    }

    void Update()
    {
        if (itemBeginDraged && this != itemBeginDraged)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}
