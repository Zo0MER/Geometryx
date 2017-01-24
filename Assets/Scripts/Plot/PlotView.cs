using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlotView : MonoBehaviour
{
    [SerializeField] private GameObject _lineGO;

    private List<GameObject> _linesGO = new List<GameObject>();
    private List<EdgeCollider2D> _collider2D;

    void Awake()
    {
        //_collider2D = GetComponents<EdgeCollider2D>().ToList();
    }

    public void SetGraphicLine(List<List<Vector3>> lines)
    {
        if (lines.Count > _linesGO.Count)
        {
            var _linesGOCoount = _linesGO.Count;
            for (int i = 0; i < lines.Count - _linesGOCoount; i++)
            {
                GameObject lineGO = Instantiate(_lineGO);
                _linesGO.Add(lineGO);
                lineGO.transform.parent = transform;
            }
        }
        else if (lines.Count < _linesGO.Count)
        {
            for (int i = 0; i < lines.Count - _linesGO.Count; i++)
            {
                _linesGO[lines.Count + i].GetComponent<LineRenderer>().numPositions = 0;
            }
        }

        for (int i = 0; i < lines.Count; i++)
        {
            List<Vector3> line = lines[i];
            var lineRenderer = _linesGO[i].GetComponent<LineRenderer>();
            lineRenderer.numPositions = line.Count;
            lineRenderer.SetPositions(line.ToArray());

            //try
            //{
            //    List<Vector2> pointsVector2 = new List<Vector2>(lines.Count);
            //    pointsVector2.AddRange(line.Select(point => (Vector2)point));
            //    _collider2D.points = pointsVector2.ToArray();
            //}
            //catch (Exception)
            //{
            //    _collider2D.points = new Vector2[0];
            //}
        }

        
    }
}
