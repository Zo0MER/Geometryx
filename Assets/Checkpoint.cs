using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public Color startColor;
    public Color checkedColor;

    void OnTriggerEnter2D(Collider2D collider)
    {
        LeanTween.color(gameObject, checkedColor, 0.4f);
    }

    public void Revert()
    {
        LeanTween.color(gameObject, startColor, 0.1f);
    }
}
