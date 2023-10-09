using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatering : MasterState
{
    private PlayerInventory inventory;

    public override void startState()
    {
        player.currentSpeed = 0;
        inventory = player.GetComponent<PlayerInventory>();
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.WATERING;
    }

    protected override void UpdateUnitState()
    {
        GameItem gameItem = inventory.getCurrentSwitableItem();

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            exitState();
        }
        else if (gameItem != null && gameItem.type == ItemTypeEnum.WATERING)
        {
            WateringCanItem wateringCan = (WateringCanItem)gameItem;
            if (wateringCan.waterCapacity <= 0.0000f)
            {
                isRunning = false;
                nextState = (int)PlayerStatesEnum.IDDLE;
            }
            else
            {
                wateringCan.toWater();
            }
        }
        else
        {
            exitState();
        }
    }

    private void exitState()
    {
        isRunning = false;
        nextState = (int)PlayerStatesEnum.IDDLE;
    }
}
