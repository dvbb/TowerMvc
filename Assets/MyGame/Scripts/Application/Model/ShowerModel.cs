using System;
using System.Collections.Generic;

public class ShowerModel : Singleton<ShowerModel>
{
    Turret currentTurret;

    public void ChangeSelectTurret(Turret turret)
    {
        currentTurret?.DisableSelect();
        turret.Select();
        currentTurret = turret;
    }

    public void ChangeSelectTurret()
    {
        currentTurret?.DisableSelect();
        currentTurret = null;
    }
}