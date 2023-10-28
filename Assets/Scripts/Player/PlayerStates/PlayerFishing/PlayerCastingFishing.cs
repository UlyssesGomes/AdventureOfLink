using UnityEngine;

public class PlayerCastingFishing : UnitState<Player>
{
    private bool reachFinalFrame;
    private bool reachOnWater;
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CASTING_FISHING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
        stateMachineObject.reachFinalSpriteFrame = false;
        stateMachineObject.isFishing = false;
        reachFinalFrame = false;
        reachOnWater = false;
    }

    protected override void UpdateUnitState()
    {
        if (stateMachineObject.reachFinalSpriteFrame)
            reachFinalFrame = true;

        if (stateMachineObject.isFishing)
            reachOnWater = true;

        if (reachFinalFrame && reachOnWater)
        {
            Debug.Log("Mudando para PlayerStatesEnum.CASTING_FISHING_ON_WATER");
            //callNextState((int)PlayerStatesEnum.CASTING_FISHING_ON_WATER);
        }
        else if (reachFinalFrame)
        {
            Debug.Log("Mudando para PlayerStatesEnum.BACK_FISHING");
            //callNextState((int)PlayerStatesEnum.BACK_FISHING);
        }

    }
}
