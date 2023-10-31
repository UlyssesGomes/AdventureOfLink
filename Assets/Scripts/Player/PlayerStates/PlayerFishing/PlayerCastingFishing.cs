using UnityEngine;

public class PlayerCastingFishing : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CASTING_FISHING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
        stateMachineObject.reachFinalSpriteFrame = false;
        stateMachineObject.isFishing = false;
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.reachFinalSpriteFrame && stateMachineObject.isFishing)
        {
            callNextState((int)PlayerStatesEnum.CASTING_FISHING_ON_WATER);
        }
        else if (stateMachineObject.reachFinalSpriteFrame)
        {
            callNextState((int)PlayerStatesEnum.BACK_FISHING);
        }
    }
}
