using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarmStartState : UnitState<SlotFarm>
{
    public override int getUnitCurrentState()
    {
        return (int) SlotFarmEnum.SLOT_FARM_START_STATE;
    }

    public override void startState()
    {
        
    }

    protected override UnitState<SlotFarm> Next()
    {
        return new SlotFarmHoleState();
    }

    protected override void UpdateUnitState()
    {
        
    }
}
