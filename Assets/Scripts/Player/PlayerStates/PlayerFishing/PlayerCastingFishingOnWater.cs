using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingFishingOnWater : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CASTING_FISHING_ON_WATER;
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
