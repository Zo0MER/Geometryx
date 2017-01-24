using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YAMP;

public class PlotPointsGenerator : MonoBehaviour
{

    public List<List<Vector3>> SimpleGeneratePoints(
        string expr,
        double left = -20.0f, double right = 20.0f,
        double top = 10.0f, double bottom = -10.0f,
        double delta = 0.1f)
    {
        List<List<Vector3>> lines = new List<List<Vector3>>();
        List<Vector3> line = new List<Vector3>();
        var parser = YAMP.Parser.Parse(expr);
        Vector3 newPoint = Vector3.zero;
        try
        {
            for (double x = left; x < right; x += delta)
            {
                var value = parser.Execute(new { x });

                newPoint.x = (float)x;
                newPoint.y = (float)YAMP.Value.CastStringToDouble(value.ToString());

                if (line.Count > 0 && (newPoint.y >= top || newPoint.y <= bottom))
                {
                    if (newPoint.y >= top && line.Last().y <= top)
                    {
                        newPoint.y = (float)top;
                        line.Add(newPoint);
                        lines.Add(line);
                        line = new List<Vector3>();
                    }
                    else if (newPoint.y <= bottom && line.Last().y >= bottom)
                    {
                        newPoint.y = (float)bottom;
                        line.Add(newPoint);
                        lines.Add(line);
                        line = new List<Vector3>();
                    }
                }
                else if (line.Count == 0 || Vector3.Distance(line.Last(), newPoint) > 0.1f)
                {
                    line.Add(newPoint);
                }
            }
        }
        catch (Exception)
        {
            
        }
        

        if (line.Count > 0)
        {
            lines.Add(line);
        }
        
        return lines;
    }

    public void Split(Parser parser, LinkedListNode<Vector3> left,
        LinkedListNode<Vector3> right, int depth)
    {
        if (depth <= 0) return;

        double x = left.Value.x + (right.Value.x - left.Value.x) / 2.0;
        Value value = parser.Execute(new { x });

        Vector3 newPoint = new Vector3();
        newPoint.x = (float)x;
        newPoint.y = (float)Value.CastStringToDouble(value.ToString());

        float angle = Vector3.Angle(newPoint - left.Value, right.Value - newPoint);
        if (angle > Mathf.Deg2Rad * 20.0f)
        {
            var newPointNode = left.List.AddAfter(left, newPoint);
            Split(parser, left, newPointNode, depth - 1);
            Split(parser, newPointNode, right, depth - 1);
        }
    }

    public List<Vector3> GenerateLine(string expr, double leftBound = -20.0f,
        double rightBound = 20.0f, int num = 20)
    {
        if (string.IsNullOrEmpty(expr)) return new List<Vector3>();

        LinkedList<Vector3> list = new LinkedList<Vector3>();
        Parser parser = Parser.Parse(expr);

        try
        {
            double delta = (rightBound - leftBound) / num;
            for (int i = 0; i < num; i += 2)
            {
                double x = leftBound + delta * i;
                Value leftValue = parser.Execute(new { x });
                Vector3 left = new Vector3();
                left.x = (float)x;
                left.y = (float)Value.CastStringToDouble(leftValue.ToString());

                x = leftBound + delta * (i + 1);
                Value rightValue = parser.Execute(new { x });
                Vector3 right = new Vector3();
                right.x = (float)x;
                right.y = (float)Value.CastStringToDouble(rightValue.ToString());

                LinkedListNode<Vector3> leftNode = list.AddLast(left);
                LinkedListNode<Vector3> rightNode = list.AddLast(right);

                Split(parser, leftNode, rightNode, 10);
            }
        }
        catch (Exception)
        { }
        return list.ToList();
    }
}
