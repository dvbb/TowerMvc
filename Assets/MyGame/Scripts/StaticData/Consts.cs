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
    public const string V_CardTable = "V_CardTable";
    public const string V_CardShower = "V_CardShower";
    public const string V_CardItem = "V_CardItem";

    // controller
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene"; //SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs

    public const string E_StartLevel = "E_StartLevel";//StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";//EndLevelArgs

    public const string E_LoadLevel = "E_LoadLevel";

    public const string E_Card = "E_Card";
    public const string E_CardItemClick = "E_CardItemClick";
    public const string E_CardSelect = "E_CardSelect";
    public const string E_CardUnSelect = "E_CardUnSelect";

    public const string E_ShowNode = "E_ShowNode";
    public const string E_HideNode = "E_HideNode";

    public const string E_StartCardDrag = "E_StartCardDrag";
    public const string E_EndCardDrag = "E_EndCardDrag";

}
