using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmHoleState : UnitState<SlotFarm>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.SLOT_FARM_HOLE_STATE;
    }

    public override void startState()
    {
        throw new System.NotImplementedException();
    }

    protected override UnitState<SlotFarm> newInstance()
    {
        return new SlotFarmHoleState();
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
