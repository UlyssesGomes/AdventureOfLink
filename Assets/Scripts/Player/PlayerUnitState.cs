using UnityEngine;

public abstract class PlayerUnitState : UnitState<Player>
{
    protected static InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.PLAYER);   // input manager instance

    /// <summary>
    /// This method makes direction always with magnitudes 1.
    /// </summary>
    protected void getInputMovementNormalized()
    {
        stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));
        if (stateMachineObject.movingObject.direction.x != 0.0f && stateMachineObject.movingObject.direction.y != 0.0f)
            stateMachineObject.movingObject.direction.Normalize();
    }
}
