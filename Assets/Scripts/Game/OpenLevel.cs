using UnityEngine;
using System.Collections;

public class OpenLevel : MonoBehaviour {

	public void LoadLevel(int n)
    {
        Application.LoadLevel(n);
    }
}
