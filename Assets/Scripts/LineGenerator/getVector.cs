using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class getVector : MonoBehaviour
{

    public float delta = 1.0f;
    // Use this for initialization
    Vector2 newPoint = new Vector2(0, 0);

    void Start()
    {
    }

    public List<Vector2> getVect(string s)
    {
        List<Vector2> list = new List<Vector2>();
        var parser = YAMP.Parser.Parse(s);
        for (double x = -20; x < 20; x += 0.1f)
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
        return list;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
