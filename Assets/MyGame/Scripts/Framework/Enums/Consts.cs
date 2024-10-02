using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    // direction
    public static string MapDir = Application.dataPath + "/Resources/UI/Maps/";
    public static string LevelDir = Application.dataPath + "/Resources/UI/Levels/";

    // save

    // model

    // view
    public const string V_Start = "V_Start";
    public const string V_Map = "V_Map";
    public const string V_Board = "V_Board";
    public const string V_CountDown = "V_CountDown";
    public const string V_Win = "V_Win";
    public const string V_Lost = "V_Lost";
    public const string V_System = "V_System";
    public const string V_Complete = "V_Complete";

    //controller
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene"; //SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs

    public const string E_StartLevel = "E_StartLevel";//StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";//EndLevelArgs

    public const string E_CountDownComplete = "E_CountDownComplete";
}
