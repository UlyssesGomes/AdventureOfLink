using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmPlantedState : UnitState<SlotFarm>
{
    private readonly float growingTime = 25.0f;

    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.PLANTED;
    }

    public override void startState()
    {
        stateMachineObject.currentRespownTime = 0;
    }

    protected override void UpdateUnitState()
    {
        stateMachineObject.currentRespownTime += Time.deltaTime;
        if(stateMachineObject.currentRespownTime > growingTime)
        {
            callNextState((int)SlotFarmEnum.START);
        }
    }
}
