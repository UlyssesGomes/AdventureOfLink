using UnityEngine;

public class PlayerBackFishing : UnitState<Player>
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
