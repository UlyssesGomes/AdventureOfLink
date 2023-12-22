using UnityEngine;

public class ChestPositioning: ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.CHEST_POSITIONING;
    }

    public override void startState()
    {
        InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.CHEST;
        //InputManager<InputAgentsEnum>.isLocked = true;
    }

    protected override void UpdateUnitState()
    {
        //stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));
    }
}
