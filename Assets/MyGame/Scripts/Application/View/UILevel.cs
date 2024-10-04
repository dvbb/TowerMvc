using System;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : View
{
    public override string Name => Consts.V_Level;

    private void Awake()
    {
    }


    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_LoadLevel:
                break;
            default:
                break;
        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_LoadLevel);
    }

    public void Init()
    {


        Debug.Log(LevelModel.Instance.LevelIndex);
        Debug.Log(Tools.GetLevelFile(LevelModel.Instance.LevelIndex));
        Debug.Log(Tools.GetLevelFile(LevelModel.Instance.LevelIndex).FullName);

        //string fileName = Tools.GetLevelFile(LevelModel.Instance.LevelIndex).FullName;
        //Tools.ParseXml(fileName, ref m_level);
        //LoadLevel(m_level);
    }
}