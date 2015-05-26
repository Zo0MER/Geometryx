using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PieceParametric : MonoBehaviour {

    private float value = 0;
    private Text textLabel;
    private EquationPanel panel;

    void Start()
    {
        panel = FindObjectOfType<EquationPanel>();
        textLabel = gameObject.GetComponentInChildren<Text>();
	}
    	
	// Update is called once per frame
	void Update () {
	    value += Time.deltaTime;
        textLabel.text = value.ToString();
        panel.UpdateFormula();
	}
}
