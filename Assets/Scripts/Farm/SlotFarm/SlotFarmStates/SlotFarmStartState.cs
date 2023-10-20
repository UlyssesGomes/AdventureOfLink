using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmStartState : UnitState<SlotFarm>
{
    public override void startState()
    {
        stateMachineObject.digAmount = stateMachineObject.maxDigAmount;
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.digAmount <= 0)
        {
            callNextState((int)SlotFarmEnum.HOLE);
        }
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.START;
    }
}
