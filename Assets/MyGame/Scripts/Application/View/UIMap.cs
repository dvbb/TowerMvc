using System;
using System.Collections.Generic;

public class UIMap : View
{
    public override string Name => Consts.V_Map;

    public void GotoMap()
    {
        Game.Instance.LoadScene(2);
    }



    #region Method
    public void GotoStart() =>Game.Instance.LoadScene(1);
    public void ChooseLevel()
    {
        EnterLevelArgs e = new EnterLevelArgs()
        {
            LevelID = 1,
        };
        SendEvent(Consts.E_EnterLevel, e);
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
}