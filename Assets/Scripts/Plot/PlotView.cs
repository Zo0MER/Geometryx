using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class PlotView : MonoBehaviour
{

    LineRenderer lineRenderer;
    private EdgeCollider2D collider2D;

    // Use this for initialization
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(0);

        collider2D = GetComponent<EdgeCollider2D>();
    }

    public void SetGraphicLine(List<Vector3> points)
    {

        lineRenderer.SetVertexCount(points.Count);
        lineRenderer.SetPositions(points.ToArray());

        try
        {
            List<Vector2> pointsVector2 = new List<Vector2>(points.Count);
            pointsVector2.AddRange(points.Select(point => (Vector2)point));
            collider2D.points = pointsVector2.ToArray();
        }
        catch (Exception)
        {
            collider2D.points = new Vector2[0];
        }
        
    }
}
