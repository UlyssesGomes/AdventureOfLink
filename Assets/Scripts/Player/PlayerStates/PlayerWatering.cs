﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatering : MasterState
{
    public override void startState()
    {
        player.currentSpeed = 0;
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.WATERING;
    }

    protected override void UpdateUnitState()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }
    }
}
