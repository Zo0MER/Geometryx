using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    foreach (Transform child in transform)
	    {
	        child.transform.localScale = Vector3.zero;
	        child.DOScale(Vector3.one, Random.Range(0.4f, 0.8f)).SetEase(Ease.OutCubic);
	    }
	}
	
	
}
