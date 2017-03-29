using System;

public class StateChangeEvent
{
    public Func<bool> ifChangeEvent { get; set; }

    public GameStateType destStateType { get; set; }
}