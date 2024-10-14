using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameModel : Singleton<GameModel>
{
    public int Health = 10;

    public void SubtractHealth(int health)
    {
        Health -= health;
        MVC.SendEvent(Consts.E_SubtractHealth);
    }
}