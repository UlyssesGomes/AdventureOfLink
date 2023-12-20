using UnityEngine;

public class PlayerWalking : PlayerUnitState
{
    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = stateMachineObject.movingObject.baseSpeed;
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.WALKING;
    }

    protected override void UpdateUnitState()
    {
        getInputMovementNormalized();

        if (stateMachineObject.movingObject.direction.sqrMagnitude == 0)
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }

        if (input.GetKey(KeyCode.LeftShift) && stateMachineObject.movingObject.direction.sqrMagnitude > 0)
        {
            callNextState((int)PlayerStatesEnum.RUNNING);
        }

        if (input.GetKeyDown(KeyCode.Backslash))
        {
            callNextState((int)PlayerStatesEnum.ROLLING);
        }
    }
}
