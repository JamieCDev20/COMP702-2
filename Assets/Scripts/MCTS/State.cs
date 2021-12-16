using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{

    public virtual State GetStateFromWorld()
    {
        State currentWorldState = null;
        return currentWorldState;
    }

    public virtual void SetState(object curState)
    {

    }

    protected virtual bool CheckStateIsTerminal()
    {
        return false;
    }

    public virtual float GetStateValue(int player)
    {
        
        return 0;
    }

    public virtual object GetActionFromStateChange(State uState, State dState)
    {
        return null;
    }

    public virtual object GetAvailableActions()
    {

        return new object[0];
    }

    public virtual State Simulate(int playerCount, object action)
    {
        return null;
    }

    public virtual void ActOnWorld(int player, object action)
    {
    }

}
