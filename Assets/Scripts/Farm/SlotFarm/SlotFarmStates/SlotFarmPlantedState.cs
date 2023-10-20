using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmPlantedState : UnitState<SlotFarm>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.PLANTED;
    }

    public override void startState()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateUnitState()
    {
        throw new System.NotImplementedException();
    }
}
