using System;
using System.Collections.Generic;

public class LevelModel : Singleton<LevelModel>
{
    public int LevelIndex { get; private set; }
    public int Cost { get; private set; }
    public int CurrentRound { get; private set; }
    public int TotalRound { get; private set; }
    
    public void Init(int levelIndex)
    {
        LevelIndex = levelIndex;
        CurrentRound = 1;
    }

    public void Init(int levelIndex, int cost, int totalRound)
    {
        LevelIndex = levelIndex;
        Cost = cost;
        CurrentRound = 1;
        TotalRound = totalRound;
    }
}