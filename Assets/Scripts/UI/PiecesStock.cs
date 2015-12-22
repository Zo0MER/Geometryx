using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Obsolete("This is an obsolete method")]
public class PiecesStock : MonoBehaviour, IDropHandler
{

    public GameObject piecePrefab;
    public GameObject parametricPrefab;
	// Use this for initialization
	void Start ()
	{
	    foreach(var symbol in FindObjectOfType<LevelState>().mixedFormula)
	    {
	        AddPiece(symbol);
	    }
	}

    public void AddPiece(string text)
    {
        GameObject newPiece;
        if (text == "t")
        {
            newPiece = Instantiate(parametricPrefab);
        }
        else
        {
            newPiece = Instantiate(piecePrefab);
        }
         
        newPiece.transform.SetParent(transform);
        newPiece.GetComponentInChildren<Text>().text = text;
    }

    public void OnDrop(PointerEventData pointerData)
    {
        
        EquationPiece piece = ExpressionToken.itemBeginDraged.GetComponent<EquationPiece>();
        if (piece)
        {
            piece.PutOnSlot(transform);
        }
    }
}
