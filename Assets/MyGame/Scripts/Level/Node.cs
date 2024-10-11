using System;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isDeploy;
    public GameObject prefab;
    public int X;
    public int Y;


    public Node(int x, int y,GameObject prefab)
    {
        this.X = x;
        this.Y = y;
        this.prefab = prefab;
    }
}