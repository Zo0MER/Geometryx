using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelState : MonoBehaviour
{
    public string[] formula;
    public string[] mixedFormula;
    private MainSphere player;

    public delegate void Pause(bool value);

    public Pause PauseUpdated;

    public bool paused
    {
        get
        {
            return !player.IsCanMove;
        }
    }


    void Awake()
    {
        mixedFormula = ShuffleString(formula);
        while (formula.Equals(mixedFormula))
        {
            mixedFormula = ShuffleString(formula);
        }
    }

    void Start()
    {
        player = FindObjectOfType<MainSphere>();
    }

    string[] ShuffleString(string[] str)
    {
        System.Random random = new System.Random();
        str = str.OrderBy(x => random.Next()).ToArray();
        return str;
    }
}
