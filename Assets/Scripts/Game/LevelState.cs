using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelState : MonoBehaviour {
    public string formula = "";
	public char[] mixedFormula;

	void Awake () {
        mixedFormula = ShuffleString(formula);
		while (formula.ToCharArray() == mixedFormula) {
            mixedFormula = ShuffleString(formula);
		}
	}

	char[] ShuffleString(string str)
	{
		char[] mixedArray = str.ToCharArray();
		int n = mixedArray.Length;  
		while (n > 1) {  
			n--;  
			int k = Random.Range(0, n + 1); 
			Random.Range(0, n + 1);
			char value = mixedArray[k];  
			mixedArray[k] = mixedArray[n];  
			mixedArray[n] = value;  
		}  
		return mixedArray;
	}
}
