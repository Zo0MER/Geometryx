using UnityEngine;
using System.Collections;

public class PlotPresenter : MonoBehaviour {

    ExpressionPanel exprPanel;
    PlotView plotView;

    void Awake()
    {
        exprPanel = FindObjectOfType<ExpressionPanel>();
        plotView = FindObjectOfType<PlotView>();

        if (exprPanel && plotView)
        {
            exprPanel.OnExpressionChanged += UpdatePlot;
        }
    }

    void UpdatePlot(string expr)
    {
        plotView.SetGraphicLine(PlotPointsGenerator.SimpleGeneratePoints(expr));
    }
}
