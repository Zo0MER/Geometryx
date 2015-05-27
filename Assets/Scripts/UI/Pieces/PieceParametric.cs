using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PieceParametric : EquationPiece
{

    private float value = 0;
    private Text textLabel;
    private EquationPanel panel;
    private LevelState levelState;

    public string Value
    {
        get { return value.ToString(); }
    }

    IEnumerator UpdateValue()
    {
        while (!levelState.paused)
        {
            value += Time.deltaTime;
            textLabel.text = value.ToString();
            panel.UpdateFormula();
            yield return new WaitForEndOfFrame();
        }

        value = 0;
        textLabel.text = "t";
    }

    void Start()
    {
        levelState = FindObjectOfType<LevelState>();
        panel = FindObjectOfType<EquationPanel>();
        textLabel = gameObject.GetComponentInChildren<Text>();
        levelState.PauseUpdated += PauseUpdated;
    }

    public void PauseUpdated(bool paused)
    {
        if (!paused)
        {
            StartCoroutine("UpdateValue");
        }
    }
}
