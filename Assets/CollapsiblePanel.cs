using UnityEngine;
using System.Collections;

public class CollapsiblePanel : MonoBehaviour {

    public void Hide()
    {
        LeanTween.moveY(gameObject, -60, 0.2f).setEase(LeanTweenType.easeInQuad);
    }

    public void Unhide()
    {
        LeanTween.moveY(gameObject, 0, 0.2f).setEase(LeanTweenType.easeInQuad);
    }
}
