using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OpenLevel(int n)
    {
        Application.LoadLevel(n);
    }
}
