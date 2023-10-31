public class PlayerCastingFishingOnWater : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CASTING_FISHING_ON_WATER;
    }

    public override void startState()
    {
        stateMachineObject.reachFinalSpriteFrame = false;
    }

    protected override void UpdateUnitState()
    {
        if(stateMachineObject.reachFinalSpriteFrame)
        {
            callNextState((int)PlayerStatesEnum.WAITING_FISHING);
        }
    }
}
