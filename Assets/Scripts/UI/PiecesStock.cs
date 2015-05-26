using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PiecesStock : MonoBehaviour, IDropHandler
{

    public GameObject piecePrefab;

	// Use this for initialization
	void Start ()
	{
	    foreach(var symbol in FindObjectOfType<LevelState>().mixedFormula)
	    {
	        AddPiece(symbol.ToString());
	    }
	}

    public void AddPiece(string text)
    {
        GameObject newPiece = Instantiate(piecePrefab);
        newPiece.transform.SetParent(transform);
        newPiece.GetComponentInChildren<Text>().text = text;
    }

    public void OnDrop(PointerEventData pointerData)
    {
        
        EquationPiece piece = DragHandler.itemBeginDraged.GetComponent<EquationPiece>();
        if (piece)
        {
            piece.PutOnSlot(transform);
        }
    }
}
