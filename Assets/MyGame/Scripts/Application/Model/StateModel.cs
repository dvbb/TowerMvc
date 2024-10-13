using System;
using System.Collections.Generic;

public enum StateEnum
{
    Normal,
    Drag,
}

public class StateModel :Singleton<StateModel>
{
    public StateEnum state;
}