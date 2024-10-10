using System;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevelCommand : Controller
{
    public override void Execute(object obj)
    {
        EnterLevelArgs args =  obj as EnterLevelArgs;

        LevelModel.Instance.Init(args.LevelID);

        // Enter Level
        Game.Instance.LoadScene(3);
    }
}