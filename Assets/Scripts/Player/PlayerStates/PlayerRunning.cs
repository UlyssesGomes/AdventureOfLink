using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MasterState
{
    private const int runningSpeed = 3;

    public override void startState()
    {
        player.currentSpeed = player.baseSpeed + runningSpeed;
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.RUNNING;
    }

    protected override void UpdateUnitState()
    {
        player.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyUp(KeyCode.LeftShift) || player.direction.sqrMagnitude == 0)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.WALKING;
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.ROLLING;
        }
    }
}
