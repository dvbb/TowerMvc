using System;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowCommand : Controller
{
    public override void Execute(object obj)
    {
        var arg =  obj as UIWindowArgs;

        switch (arg.Index)
        {
            case WindowCode.ESC:
                break;
            default:
                break;
        }
    }
}