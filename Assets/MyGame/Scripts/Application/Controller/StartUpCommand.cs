using System;
using System.Collections.Generic;

public class StartUpCommand : Controller
{
    public override void Execute(object obj)
    {
        // Register Model

        // Register Controller: Establish mapping relationship
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Consts.E_EnterLevel, typeof(EnterLevelCommand));
        RegisterController(Consts.E_ExitLevel, typeof(ExitLevelCommand));

        RegisterController(Consts.E_CardSelect, typeof(CardCommand));
        RegisterController(Consts.E_CardUnSelect, typeof(CardCommand));

        RegisterController(Consts.E_UIWindow, typeof(UIWindowCommand));

        // Enter game title
        Game.Instance.LoadScene(1);
    }
}
