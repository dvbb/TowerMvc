using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LevelModel : Singleton<LevelModel>
{
    public int LevelIndex { get; private set; }
    public int Cost { get; private set; }
    public int CurrentRound { get; private set; }
    public int TotalRound { get; private set; }

    public LevelInfo levelInfo;

    public Vector3[] waypoints;

    public void Init(int levelIndex)
    {
        LevelIndex = levelIndex;
        CurrentRound = 1;

        TextAsset XMLAsset = Resources.Load<TextAsset>($"UI/Levels/Level{LevelModel.Instance.LevelIndex}");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLAsset.text);

        Tools.ParseXml(doc, ref levelInfo);
    }

    public void Init(int levelIndex, int cost, int totalRound)
    {
        LevelIndex = levelIndex;
        Cost = cost;
        CurrentRound = 1;
        TotalRound = totalRound;
    }
}