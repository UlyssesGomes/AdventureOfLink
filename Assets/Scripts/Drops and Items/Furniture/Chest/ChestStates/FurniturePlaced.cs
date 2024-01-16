using UnityEngine;

public class FurniturePlaced : ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.FURNITURE_PLACED;
    }

    public override void startState()
    {
        stateMachineObject.setPlacedChest(true);
    }

    protected override void UpdateUnitState()
    {

    }
}
