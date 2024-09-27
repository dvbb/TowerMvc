using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Level
{
    public string Name;
    public string CardImage;
    public string Background;
    public string Road;
    public int InitScore;

    public List<Point> Holder = new List<Point>();
    public List<Point> Path = new List<Point>();
    public List<Round> Rounds = new List<Round>();
}
