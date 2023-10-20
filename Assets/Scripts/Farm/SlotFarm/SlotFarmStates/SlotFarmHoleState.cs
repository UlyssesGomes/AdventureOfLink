using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmHoleState : UnitState<SlotFarm>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.HOLE;
    }

    public override void startState()
    {
        stateMachineObject.currentRespownTime = 0;
        stateMachineObject.waterAmount = 0;
    }

    protected override void UpdateUnitState()
    {
        stateMachineObject.currentRespownTime += Time.deltaTime;
        if (stateMachineObject.currentRespownTime >= stateMachineObject.RESPOWN_TIME)
        {
            callNextState((int)SlotFarmEnum.START);
        }

        if(stateMachineObject.detectWater)
        {
            if(stateMachineObject.waterAmount <= stateMachineObject.maxWaterAmount)
            {
                stateMachineObject.waterAmount += Time.deltaTime;
            }
            else
            {
                callNextState((int)SlotFarmEnum.PLANTED);
            }
        }
    }
}
