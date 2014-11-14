using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour {

    public GameObject xLine;
    public GameObject yLine;

	// Use this for initialization
    void Start()
    {
        for(int i = -10; i < 20; i++)
        {
            Instantiate(xLine, xLine.transform.position + new Vector3(i, 0), xLine.transform.rotation);
            
            Instantiate(yLine, yLine.transform.position + new Vector3(0, i), yLine.transform.rotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
