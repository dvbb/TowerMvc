using System;
using System.Collections.Generic;

public class UIMap : View
{
    public override string Name => throw new NotImplementedException();

    public void GotoMap()
    {
        Game.Instance.LoadScene(2);
    }



    #region Method
    public void GotoStart() =>Game.Instance.LoadScene(1);
    public void ChooseLevel()
    {
        StartLevelArgs e = new StartLevelArgs()
        {
            LevelID = 0,
        };
        SendEvent(Consts.E_StartLevel, e);
    }
    #endregion

    #region Event Callback
    public override void HandleEvent(string eventName, object obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Unity Callback
    #endregion

    #region MyRegion
    #endregion
}