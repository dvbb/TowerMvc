using System;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : View
{
    public override string Name => throw new NotImplementedException();

    public void GotoMap()
    {
        Game.Instance.LoadScene(2);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        throw new NotImplementedException();
    }

    #region Unity Button Click

    public void OnStartClicked() => Game.Instance.LoadScene(2);
    
    public void OnContinueClicked()
    {

    }

    public void OnSettingClicked()
    {

    }

    public void OnExitClicked() => Application.Quit();

    #endregion

}