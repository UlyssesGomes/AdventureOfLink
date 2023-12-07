using UnityEngine;

public class PlayerCutting : PlayerUnitState
{
    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
    }

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CUTTING;
    }

    protected override void UpdateUnitState()
    {
        if (input.GetKeyUp(KeyCode.LeftControl))
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }
    }
}
