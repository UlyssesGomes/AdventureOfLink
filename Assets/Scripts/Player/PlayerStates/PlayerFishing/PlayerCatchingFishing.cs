using UnityEngine;

public class PlayerCatchingFishing : PlayerUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CATCHING_FISHING;
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
