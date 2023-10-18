using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmPlantedState : UnitState<SlotFarm>
{
    public override int getUnitCurrentState()
    {
        return (int)SlotFarmEnum.SLOT_FARM_PLANTED_STATE;
    }

    public override void startState()
    {
        throw new System.NotImplementedException();
    }

    protected override UnitState<SlotFarm> Next()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateUnitState()
    {
        throw new System.NotImplementedException();
    }
}
