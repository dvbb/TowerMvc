using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Grid
{
    public int X;
    public int Y;
    public bool IsHolder;
    public object Data;

    public Grid(int x, int y)
    {
        X = x;
        Y = y;
    }
}