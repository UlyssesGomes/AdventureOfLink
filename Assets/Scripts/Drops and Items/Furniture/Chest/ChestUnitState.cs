using UnityEngine;

public abstract class ChestUnitState : UnitState<FurniturePlacement>
{
    protected InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.CHEST);

    protected void getInputMovementNormalized()
    {
        stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));
        if (stateMachineObject.movingObject.direction.x != 0.0f && stateMachineObject.movingObject.direction.y != 0.0f)
            stateMachineObject.movingObject.direction.Normalize();
    }
}
