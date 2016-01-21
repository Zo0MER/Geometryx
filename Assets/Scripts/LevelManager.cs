using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private GameObject character;
    private Vector3 startPosition;

	void Start ()
    {
	    character = GameObject.FindGameObjectWithTag("Player");
	    startPosition = character.transform.position;
        Pause();
    }

    public void Retry()
    {
        character.transform.position = startPosition;
        Pause();
    }

    public void Pause()
    {
        character.GetComponent<Rigidbody2D>().isKinematic = true;
        foreach (var checkpoint in FindObjectsOfType<Checkpoint>())
        {
            checkpoint.Revert();
        }
    }

    public void Unpause()
    {
        character.GetComponent<Rigidbody2D>().isKinematic = false;
        
        
    }
}
