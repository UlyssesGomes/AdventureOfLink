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
            if(inventory.getCurrentSwitableItem() != null)
            {
                if (inventory.getCurrentSwitableItem().type == (int)ItemTypeEnum.WOOD_CUTTER)
                {
                    nextState = (int)PlayerStatesEnum.CUTTING;
                }
                else if (inventory.getCurrentSwitableItem().type == (int)ItemTypeEnum.DIGGING)
                {
                    nextState = (int)PlayerStatesEnum.DIGGING;
                } 
                else if (inventory.getCurrentSwitableItem().type == (int)ItemTypeEnum.WATERING)
                {
                    nextState = (int)PlayerStatesEnum.WATERING;
                }
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
