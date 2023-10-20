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
        throw new System.NotImplementedException();
    }

    protected override void UpdateUnitState()
    {
        throw new System.NotImplementedException();
    }
}
