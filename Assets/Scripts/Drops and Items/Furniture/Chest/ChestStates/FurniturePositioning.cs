using UnityEngine;

public class FurniturePositioning: ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.FURNITURE_POSITIONING;
    }

    public override void startState()
    {
        InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.CHEST;
        InputManager<InputAgentsEnum>.isLocked = true;

        stateMachineObject.gizmosGuide.gameObject.SetActive(true);
        stateMachineObject.setPlacedChest(false);
    }

    protected override void UpdateUnitState()
    {
        getInputMovementNormalized();

        if(input.GetKey(KeyCode.F) && stateMachineObject.canPlace)
        {
            callNextState((int) ChestStateEnum.FURNITURE_PLACED);
            InputManager<InputAgentsEnum>.isLocked = false;
            stateMachineObject.gizmosGuide.setEnable(false);
        }
        else if(input.GetKey(KeyCode.F) && !stateMachineObject.canPlace)
        {
            // TODO - emit "tandan" sound, because chest cant be placed.
            Debug.LogWarning("TANDAN - the chest cant be placed here.");
        }
    }
}
