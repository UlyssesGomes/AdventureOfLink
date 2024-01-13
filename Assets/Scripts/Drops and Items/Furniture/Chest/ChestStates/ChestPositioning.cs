using UnityEngine;

public class ChestPositioning: ChestUnitState
{
    public override int getUnitCurrentStateKey()
    {
        return (int) ChestStateEnum.CHEST_POSITIONING;
    }

    public override void startState()
    {
        InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.CHEST;
        InputManager<InputAgentsEnum>.isLocked = true;

        stateMachineObject.gizmosGuide.gameObject.SetActive(true);
    }

    protected override void UpdateUnitState()
    {
        getInputMovementNormalized();
    }
}
