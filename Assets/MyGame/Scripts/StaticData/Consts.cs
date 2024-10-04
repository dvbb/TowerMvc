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
    public const string V_Title = "V_Title";
    public const string V_Map = "V_Map";
    public const string V_Level = "V_Level";

    //controller
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene"; //SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs

    public const string E_StartLevel = "E_StartLevel";//StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";//EndLevelArgs

    public const string E_LoadLevel = "E_LoadLevel";
}
