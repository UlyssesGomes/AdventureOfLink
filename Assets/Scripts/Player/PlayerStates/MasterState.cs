using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MasterState : UnitState
{
    protected int nextState;
    private static bool isFirtsCall = true;

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
    protected override UnitState Next()
    {
        UnitState next = getInstance(nextState);
        if(next == null)
        {
            throw new System.NotImplementedException("Unit State with id " + nextState +" not added yet");
        }
        return getInstance(nextState);
    }
}
