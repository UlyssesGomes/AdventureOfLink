using UnityEngine;

public class PlayerWalking : UnitState<Player>
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
        stateMachineObject.movingObject.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(stateMachineObject.movingObject.direction.sqrMagnitude == 0)
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }

        if (Input.GetKey(KeyCode.LeftShift) && stateMachineObject.movingObject.direction.sqrMagnitude > 0)
        {
            callNextState((int)PlayerStatesEnum.RUNNING);
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            callNextState((int)PlayerStatesEnum.ROLLING);
        }
    }

    protected override UnitState<Player> newInstance()
    {
        return this;
    }
}
