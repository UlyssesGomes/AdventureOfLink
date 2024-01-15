using UnityEngine;

public class ChestPlaced : ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.CHEST_PLACED;
    }

    public override void startState()
    {
        stateMachineObject.setPlacedChest(true);
    }

    protected override void UpdateUnitState()
    {

    }
}
