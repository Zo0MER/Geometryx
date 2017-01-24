using UnityEngine;
using System.Collections;

public class PlotPresenter : MonoBehaviour {

    ExpressionPanel exprPanel;
    PlotView plotView;
    PlotPointsGenerator plotPointsGenerator = new PlotPointsGenerator();

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
        if (string.IsNullOrEmpty(expr)) return;

        plotView.SetGraphicLine(plotPointsGenerator.SimpleGeneratePoints(expr));
        //plotView.SetGraphicLine(plotPointsGenerator.GenerateLine(expr));
    }
}
