using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlotPointsGenerator : MonoBehaviour
{

    static public List<Vector3> SimpleGeneratePoints(string expr, double left = -20.0f, double right = 20.0f, double delta = 0.1f)
    {
        List<Vector3> list = new List<Vector3>();
        try
        {


            var parser = YAMP.Parser.Parse(expr);
            Vector3 newPoint = Vector3.zero;
            for (double x = left; x < right; x += delta)
            {
                var value = parser.Execute(new { x });

                newPoint.x = (float)x;
                newPoint.y = (float)YAMP.Value.CastStringToDouble(value.ToString());

                if (list.Count == 0 ||
                    Vector2.Distance(list[list.Count - 1], newPoint) > 0.1f)
                {
                    list.Add(newPoint);
                }

            }
        }
        catch
        { }
        return list;
    }
}
