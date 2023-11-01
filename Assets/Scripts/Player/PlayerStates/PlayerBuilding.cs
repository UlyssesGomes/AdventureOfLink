using UnityEngine;

public class PlayerBuilding : UnitState<Player>
{
    public override int getUnitCurrentStateKey()
    {
        return (int) PlayerStatesEnum.BUILDING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
    }

    protected override void UpdateUnitState()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }
    }
}
