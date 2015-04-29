using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    MovebaleUI move;
    void Start()
    {
        move = GetComponent<MovebaleUI>();
    }

    public void MoveTo(Vector3 pos)
    {
        if(!move.IsDragged)
        {
            transform.localPosition = pos;
        }
    }
}
