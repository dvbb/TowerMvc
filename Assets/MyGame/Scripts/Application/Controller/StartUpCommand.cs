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
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));

        // Enter game title
        Game.Instance.LoadScene(1);
    }
}
