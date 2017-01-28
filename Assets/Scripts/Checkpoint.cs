using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Checkpoint : MonoBehaviour
{
    public Color startColor;
    public Color checkedColor;

    public Image _image;


    void OnTriggerEnter2D(Collider2D collider)
    {
        _image.DOColor(checkedColor, 0.4f);
    }

    public void Revert()
    {
        _image.DOColor(startColor, 0.4f);
    }
}
