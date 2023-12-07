public class ChestPositioning: ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.CHEST_POSITIONING;
    }

    public override void startState()
    { }

    protected override void UpdateUnitState()
    {
        
    }
}
