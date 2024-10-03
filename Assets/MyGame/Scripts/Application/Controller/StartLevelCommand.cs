using System;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
    public override void Execute(object obj)
    {
        StartLevelArgs args =  obj as StartLevelArgs;

        LevelModel.Instance.Init(args.LevelID);

        // Enter Level
        Game.Instance.LoadScene(3);
    }
}