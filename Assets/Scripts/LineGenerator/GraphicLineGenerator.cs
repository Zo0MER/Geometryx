using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GraphicLineGenerator : MonoBehaviour
{
    string expression = "x";
    LineRenderer lineRenderer;
    [Range(0.0f, 1.0f)]
    public float bold = 0.2f;
    List<Vector2> points;

    public float lerpTime = 0.5f;

    List<Vector2> oldPoints;
    List<Vector2> newPoints;
    private EdgeCollider2D edgeCollider;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    IEnumerator LerpGraph()
    {
        points = oldPoints;
        for (float i = 0; i < lerpTime; i += Time.deltaTime)
        {
            for (int j = 0; j < oldPoints.Count; j++)
            {
                points[j] = Vector3.Lerp(oldPoints[j], newPoints[j], i / lerpTime);
            }
            SetGraphicLine(new List<Vector2>(points));
            yield return new WaitForEndOfFrame();
        }
    }

    void SetGraphicLine(List<Vector2> points)
    {
        lineRenderer.numPositions = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        edgeCollider.points = points.ToArray();
    }

}
