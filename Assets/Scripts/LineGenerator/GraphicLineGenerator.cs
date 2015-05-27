using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GraphicLineGenerator : MonoBehaviour {
    public string formula;
    LineRenderer lineRenderer;
    [Range(0.0f , 1.0f)] 
    public float bold = 0.2f;
    getVector vector = new getVector();
    string prevf;
    List<Vector2> points;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
        try
        {
            if (prevf != formula)
            {
                points = vector.getVect(formula);
                lineRenderer = GetComponent<LineRenderer>();
                GenerateMesh(new List<Vector2>(points));
            }
            prevf = formula;
        }
        catch { }
	}


    void GenerateMesh(List<Vector2> points)
    {
        lineRenderer.SetVertexCount(points.Count);

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        GetComponent<EdgeCollider2D>().points = points.ToArray();
    }
}
