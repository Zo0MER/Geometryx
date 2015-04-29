using UnityEngine;
using System.Collections;

public class Fromula : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Piece")
        {
            collider.gameObject.GetComponent<Piece>().MoveTo(transform.localPosition);
        }
    }
}
