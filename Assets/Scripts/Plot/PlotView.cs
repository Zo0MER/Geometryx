using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class PlotView : MonoBehaviour
{


    LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(0);
    }

    public void SetGraphicLine(List<Vector3> points)
    {
        lineRenderer.SetVertexCount(points.Count);
        lineRenderer.SetPositions(points.ToArray());
    }
}
