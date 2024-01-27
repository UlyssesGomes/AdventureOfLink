using UnityEngine;

public class PlayerDoing : PlayerUnitState
{
    private float doingTimer;               // max doing timer to be set at the start method
    private float currentDoingTimer;        // current doing timer that will be decreased at update mathod

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.DOING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;

        currentDoingTimer = doingTimer = stateMachineObject.doingTimer;
        stateMachineObject.doingTimer = 0.0f;

        stateMachineObject.doingBar.SetActive(true);
    }

    protected override void UpdateUnitState()
    {
        currentDoingTimer -= Time.deltaTime;
        updateFilledBar();

        if(currentDoingTimer <= 0.0000f)
        {
            stateMachineObject.doingBar.SetActive(false);
            callNextState((int)PlayerStatesEnum.IDDLE);
        }

    }

    private void updateFilledBar()
    {
        float total = currentDoingTimer / doingTimer;
        stateMachineObject.doingFilledBar.fillAmount = total;
    }
}
