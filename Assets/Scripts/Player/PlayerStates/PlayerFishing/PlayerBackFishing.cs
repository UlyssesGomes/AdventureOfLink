using UnityEngine;

public class PlayerBackFishing : PlayerUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.BACK_FISHING;
    }

    public override void startState()
    {
        stateMachineObject.reachFinalSpriteFrame = false;
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.reachFinalSpriteFrame)
            callNextState((int)PlayerStatesEnum.IDDLE);
    }
}
