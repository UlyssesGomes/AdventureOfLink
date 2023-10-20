using UnityEngine;

public class PlayerDigging : UnitState<Player>
{
    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.DIGGING;
    }

    protected override void UpdateUnitState()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }
    }
}
