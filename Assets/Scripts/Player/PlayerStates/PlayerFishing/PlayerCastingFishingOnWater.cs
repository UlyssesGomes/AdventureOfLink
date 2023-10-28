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
    }

    protected override void UpdateUnitState()
    {
    }
}
