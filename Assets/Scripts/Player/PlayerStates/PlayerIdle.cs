using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : MasterState
{
    protected override void UpdateUnitState()
    {
        player.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(player.direction.sqrMagnitude != 0)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.WALKING;
        }

        if (player.direction.sqrMagnitude == 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isRunning = false;
            PlayerInventory inventory = player.GetComponent("PlayerInventory") as PlayerInventory;
            if (inventory.getSetItem(0).id == (int)ItemsEnum.SIMPLE_AXE)
            {
                nextState = (int)PlayerStatesEnum.CUTTING;
            }
            else if (inventory.getSetItem(0).id == (int)ItemsEnum.SIMPLE_SHOVEL)
            {
                nextState = (int)PlayerStatesEnum.DIGGING;
            } else if (inventory.getSetItem(0).id == (int)ItemsEnum.WATERING_CAN)
            {
                nextState = (int)PlayerStatesEnum.WATERING;
            }
        }
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.IDDLE;
    }

    public override void startState()
    {
        player.currentSpeed = 0;
    }
}
