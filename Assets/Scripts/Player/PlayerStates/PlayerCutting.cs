using UnityEngine;

public class PlayerCutting : MasterState
{
    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
    }

    public override int getUnitCurrentState()
    {
        return (int)PlayerStatesEnum.CUTTING;
    }

    protected override void UpdateUnitState()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRunning = false;
            nextState = (int)PlayerStatesEnum.IDDLE;
        }
    }
}
