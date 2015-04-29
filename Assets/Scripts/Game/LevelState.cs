using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelState : MonoBehaviour {

    public GameObject button;
	public string formula = "";
	static char[] mixedFormula;
    List<GameObject> list = new List<GameObject>(0);
    List<GameObject> activated = new List<GameObject>(0);

	// Use this for initialization
	void Start () {
        mixedFormula = ShuffleString(formula);
		while (formula.ToCharArray() == mixedFormula) {
            mixedFormula = ShuffleString(formula);
		}
        CreateButtons(mixedFormula);
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

    void CreateButtons(char[] a)
    {
        list.Capacity = a.Length;
        for (int i = 0; i < list.Capacity; i++)
        {
            GameObject button1 = (GameObject)Instantiate(button, 
                new Vector3(Camera.main.transform.position.x + 8, Camera.main.transform.position.y + 4 - i), Quaternion.identity);
            TextMesh tm = button1.GetComponentInChildren<TextMesh>();
            tm.text = "";
            tm.text += a[i];
            list.Add(button1);
        }
        //button.tag = "text";
        
    }

	
	// Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            TextMesh eval = GameObject.FindGameObjectWithTag("Evaluation").GetComponent<TextMesh>();
            GraphicLineGenerator graphic = GameObject.FindGameObjectWithTag("GraphicLine").GetComponentInChildren<GraphicLineGenerator>();
            Vector3 vector = Input.mousePosition;
            vector = Camera.main.ScreenToWorldPoint(vector);
            vector.z = 0;
            for (int i = 0; i < list.Count; i++)
            {
                Vector3 buttonPos = list[i].transform.position;

                if ((Mathf.Abs(vector.x - buttonPos.x) <= list[i].GetComponent<Collider2D>().bounds.size.x/2) &&
                    (Mathf.Abs(vector.y - buttonPos.y) <= list[i].GetComponent<Collider2D>().bounds.size.y/2))
                {
                    if (!activated.Contains(list[i]))
                    {
                        activated.Add(list[i]);
                        list[i].GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
                    }
                    else
                    {
                        activated.Remove(list[i]);
                        list[i].GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = true;
                    }
                    eval.text = "y=";
                    foreach(GameObject obj in activated)
                    {
                        TextMesh text = obj.GetComponentInChildren<TextMesh>();
                        eval.text += text.text;
                    }
                    graphic.formula = eval.text.Remove(0, 2);
                }
            }
        }
    }

    

	static public char[] Formula {
		get {
			return mixedFormula;
		}
	}
}
