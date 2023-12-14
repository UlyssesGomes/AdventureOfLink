using UnityEngine;

public class ChestPositioning: ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.CHEST_POSITIONING;
    }

    public override void startState()
    {
        InputManager<InputAgentsEnum>.isLocked = true;
        Debug.Log("oi");
    }

    protected override void UpdateUnitState()
    {
        Debug.Log("oi3");
        stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));
    }
}
