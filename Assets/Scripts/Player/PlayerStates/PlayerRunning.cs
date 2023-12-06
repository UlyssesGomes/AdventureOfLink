using UnityEngine;

public class PlayerRunning : UnitState<Player>
{
    private const int runningSpeed = 3;

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = stateMachineObject.movingObject.baseSpeed + runningSpeed;
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.RUNNING;
    }

    protected override void UpdateUnitState()
    {
        stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));

        if (input.GetKeyUp(KeyCode.LeftShift) || stateMachineObject.movingObject.direction.sqrMagnitude == 0)
        {
            callNextState((int)PlayerStatesEnum.WALKING);
        }

        if (input.GetKeyDown(KeyCode.Backslash))
        {
            callNextState((int)PlayerStatesEnum.ROLLING);
        }
    }
}
