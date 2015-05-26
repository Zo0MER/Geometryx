using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    static private int currentLvl = 1;
    private float angleSpeed = 2.0f;
    private float pause = 2.0f;
    private bool teleport;
    
	// Use this for initialization
	void Start () {
        ((GameObject)GameObject.FindGameObjectWithTag("WinText")).GetComponent<TextMesh>().GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3(0, 0, angleSpeed));
        
        if(teleport)
            pause -= Time.deltaTime;
        if (pause < 0)
        {
            Application.LoadLevel(++currentLvl % Application.levelCount);

        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        teleport = true;
        ((GameObject)GameObject.FindGameObjectWithTag("WinText")).GetComponent<TextMesh>().GetComponent<Renderer>().enabled = true;
          
    }
}
