using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MasterState
{
    public override void startState()
    {
        player.currentSpeed = player.baseSpeed;
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.WALKING;
    }

    protected override void UpdateUnitState()
    {
        player.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(player.direction.sqrMagnitude == 0)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }

        if (Input.GetKey(KeyCode.LeftShift) && player.direction.sqrMagnitude > 0)
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
