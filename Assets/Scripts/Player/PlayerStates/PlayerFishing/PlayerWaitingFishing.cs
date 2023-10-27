using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitingFishing : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.WAITING_FISHING;
    }

    public override void startState()
    {
    }

    protected override void UpdateUnitState()
    {
    }
}
