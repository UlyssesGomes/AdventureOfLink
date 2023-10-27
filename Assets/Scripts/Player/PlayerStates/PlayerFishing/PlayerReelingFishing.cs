public class PlayerReelingFishing : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.REELING_FISHING;
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
