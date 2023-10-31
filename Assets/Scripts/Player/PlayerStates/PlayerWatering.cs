using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatering : UnitState<Player>
{
    private PlayerInventory inventory;

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
        inventory = stateMachineObject.playerInventory;
    }

    public override int getUnitCurrentStateKey()
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
                nextStateKey = (int)PlayerStatesEnum.IDDLE;
            }
            else
            {
                wateringCan.toWater();
                inventory.notifyStoredItemsObserversById(wateringCan.id);
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
        nextStateKey = (int)PlayerStatesEnum.IDDLE;
    }
}
