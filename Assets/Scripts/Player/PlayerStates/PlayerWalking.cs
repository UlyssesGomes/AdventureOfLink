using UnityEngine;

public class PlayerWalking : MasterState
{
    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = stateMachineObject.movingObject.baseSpeed;
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.WALKING;
    }

    protected override void UpdateUnitState()
    {
        stateMachineObject.movingObject.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(stateMachineObject.movingObject.direction.sqrMagnitude == 0)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }

        if (Input.GetKey(KeyCode.LeftShift) && stateMachineObject.movingObject.direction.sqrMagnitude > 0)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.RUNNING;
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.ROLLING;
        }
    }
}
