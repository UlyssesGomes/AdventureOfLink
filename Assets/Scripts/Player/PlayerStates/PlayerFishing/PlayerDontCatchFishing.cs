using UnityEngine;

public class PlayerDontCatchFishing : PlayerUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.DONT_CATCH_FISHING;
    }

    public override void startState()
    {
        stateMachineObject.reachFinalSpriteFrame = false;
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.reachFinalSpriteFrame)
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }
    }
}
