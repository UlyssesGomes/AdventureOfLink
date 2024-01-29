using UnityEngine;

public class PlayerDoing : PlayerUnitState
{
    private float maxDoingTimer;            // max doing timer to be set at the start method

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.DOING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;

        maxDoingTimer = stateMachineObject.maxDoingTimer;

        stateMachineObject.doingBar.SetActive(true);
    }

    protected override void UpdateUnitState()
    {
        if(maxDoingTimer != stateMachineObject.maxDoingTimer)
        {
            maxDoingTimer = stateMachineObject.maxDoingTimer;
            updateFilledBar();
        }

        stateMachineObject.doingTimer -= Time.deltaTime;
        updateFilledBar();

        if(stateMachineObject.doingTimer <= 0.0000f)
        {
            stateMachineObject.doingTimer = 0.0f;
            stateMachineObject.maxDoingTimer = 0.0f;
            stateMachineObject.doingBar.SetActive(false);
            callNextState((int)PlayerStatesEnum.IDDLE);
        }

    }

    private void updateFilledBar()
    {
        float total = stateMachineObject.doingTimer / maxDoingTimer;
        stateMachineObject.doingFilledBar.fillAmount = total;
    }
}
