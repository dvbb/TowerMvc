using System;
using System.Collections.Generic;
using UnityEngine;

public class RegisterLevelViewCommand : Controller
{
    public override void Execute(object obj)
    {
        var turret =  obj as TurretBase;
        RegisterView(turret);
    }
}