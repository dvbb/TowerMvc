using System;
using System.Collections.Generic;

public class ShowerModel : Singleton<ShowerModel>
{
    TurretBase currentTurret;

    public void ChangeSelectTurret(TurretBase turret)
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