using System;
using System.Collections.Generic;

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
}