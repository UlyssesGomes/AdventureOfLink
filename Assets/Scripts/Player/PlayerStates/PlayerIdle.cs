using UnityEngine;

public class PlayerIdle : UnitState<Player>
{
    protected override void UpdateUnitState()
    {
        stateMachineObject.movingObject.direction = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical"));

        if(stateMachineObject.movingObject.direction.sqrMagnitude != 0)
        {
            callNextState((int)PlayerStatesEnum.WALKING);
        }

        if (stateMachineObject.movingObject.direction.sqrMagnitude == 0 && input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerInventory inventory = stateMachineObject.playerInventory;
            if(inventory.getCurrentSwitableItem() != null)
            {
                if (inventory.getCurrentSwitableItem().type == ItemTypeEnum.WOOD_CUTTER)
                {
                    callNextState((int)PlayerStatesEnum.CUTTING);
                }
                else if (inventory.getCurrentSwitableItem().type == ItemTypeEnum.DIGGING)
                {
                    callNextState((int)PlayerStatesEnum.DIGGING);
                } 
                else if (inventory.getCurrentSwitableItem().type == ItemTypeEnum.WATERING)
                {
                    callNextState((int)PlayerStatesEnum.WATERING);
                }
                else if (inventory.getCurrentSwitableItem().type == ItemTypeEnum.FISHING)
                {
                    callNextState((int)PlayerStatesEnum.CASTING_FISHING);
                }
                else if(inventory.getCurrentSwitableItem().type == ItemTypeEnum.HAMMER_BUILD)
                {
                    callNextState((int)PlayerStatesEnum.BUILDING);
                }
            }
        }
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.IDDLE;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
    }
}
