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
        WateringCan wateringCan = (WateringCan)inventory.getListItem(0);
        if (Input.GetKeyUp(KeyCode.LeftControl) || inventory.getListItem(0).id != (int)ItemsEnum.WATERING_CAN)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }
        else if (wateringCan.waterCapacity <= 0.0000f)
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }
        else
        {
                wateringCan.waterCapacity -= 1 * Time.deltaTime; 
        }
        TODO - Parei no vídeo 7.4 no minuto 15.3
    }
}
