using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlotView : MonoBehaviour
{
    [SerializeField] private GameObject _lineGO;

    private List<GameObject> _linesGO = new List<GameObject>();
    private List<List<Vector3>> _lines = new List<List<Vector3>>();

    private bool _updateLineCollider = false;

    void Awake()
    {
        //_collider2D = GetComponents<EdgeCollider2D>().ToList();
    }

    public void SetGraphicLine(List<List<Vector3>> lines)
    {
        _lines = lines;
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
        }

        _updateLineCollider = true;
    }

    void FixedUpdate()
    {
        if (!_updateLineCollider) return;

        _updateLineCollider = false;

        for (int i = 0; i < _lines.Count; i++)
        {
            List<Vector3> line = _lines[i];
            var lineCollider = _linesGO[i].GetComponent<EdgeCollider2D>();
            try
            {
                List<Vector2> pointsVector2 = new List<Vector2>(_lines.Count);
                pointsVector2.AddRange(line.Select(point => (Vector2)point));
                lineCollider.points = pointsVector2.ToArray();
            }
            catch (Exception)
            {
                lineCollider.points = new Vector2[0];
            }
        }
    }
}
