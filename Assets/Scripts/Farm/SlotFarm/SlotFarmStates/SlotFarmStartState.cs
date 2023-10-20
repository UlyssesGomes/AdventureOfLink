using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmStartState : UnitState<SlotFarm>
{
    public override void startState()
    {
        
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.digAmount <= 0)
        {
            stateMachineObject.currentRespownTime += Time.deltaTime;
            if (stateMachineObject.currentRespownTime >= stateMachineObject.RESPOWN_TIME)
            {
                stateMachineObject.digAmount = stateMachineObject.maxDigAmount;
                
            }
        }
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.START;
    }
}
