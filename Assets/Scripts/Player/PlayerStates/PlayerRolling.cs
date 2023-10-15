using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolling : MasterState
{
    private const float ROLLING_TIME = 2.0f;

    private bool isRolling;
    private float rollingTime;

    private const float rollSpeed = 8;

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.ROLLING;
    }

    protected override void UpdateUnitState()
    {
        if (isRolling)
        {
            rollingTime += Time.fixedDeltaTime;

            if (rollingTime >= ROLLING_TIME)
            {
                isRolling = false;
                isRunning = false;
                nextState = (int)PlayerStatesEnum.WALKING;
            }
        }

        if (Input.GetKeyDown(KeyCode.Backslash) && !isRolling)
        {
            isRolling = true;
            rollingTime = 0.0f;
        }
    }

    public override void startState()
    {
        isRolling = true;
        rollingTime = 0.0f;

        stateMachineObject.movingObject.currentSpeed = stateMachineObject.movingObject.baseSpeed + rollSpeed;
    }
}
