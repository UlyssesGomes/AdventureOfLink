using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MasterState : UnitState<Player>
{
    protected int nextState;
    private static bool isFirtsCall = true;

    /// <summary>
    /// Start all player states intances.
    /// </summary>
    public MasterState()
    {
        if(isFirtsCall)
        {
            isFirtsCall = false;
            addInstance((int)PlayerStatesEnum.IDDLE, new PlayerIdle());
            addInstance((int)PlayerStatesEnum.WALKING, new PlayerWalking());
            addInstance((int)PlayerStatesEnum.RUNNING, new PlayerRunning());
            addInstance((int)PlayerStatesEnum.ROLLING, new PlayerRolling());
            addInstance((int)PlayerStatesEnum.CUTTING, new PlayerCutting());
            addInstance((int)PlayerStatesEnum.DIGGING, new PlayerDigging());
            addInstance((int)PlayerStatesEnum.WATERING, new PlayerWatering());
        }
    }

    /// <summary>
    /// Try call next state. If current state doesnt implement next state, then a error will be throw.
    /// </summary>
    /// <returns>Return next state.</returns>
    protected override UnitState<Player> Next()
    {
        UnitState<Player> next = getInstance(nextState);
        if(next == null)
        {
            throw new System.NotImplementedException("Unit State with id " + nextState +" not added yet");
        }
        return getInstance(nextState);
    }
}
