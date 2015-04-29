using UnityEngine;
using System.Collections;

public class MovebaleUI : MonoBehaviour {

    Vector3 screenMiddlePoint;
    bool isDragged;
	// Use this for initialization
	void Start () {
        screenMiddlePoint = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
	}

    void Update()
    {
    }
	
	public void MoveButton()
    {
        isDragged = true;
        transform.localPosition = Input.mousePosition - screenMiddlePoint;
    }

    public void Dropped()
    {
        isDragged = false;
    }

    public bool IsDragged
    {
        get
        {
            return isDragged;
        }
    }
}
