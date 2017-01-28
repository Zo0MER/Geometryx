using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CollapsiblePanel : MonoBehaviour {

    public void Hide()
    {
        transform.DOMoveY(-60, 0.2f).SetEase(Ease.InQuad);
    }

    public void Unhide()
    {
        transform.DOMoveY(0, 0.2f).SetEase(Ease.InQuad);
    }
}
