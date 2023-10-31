using UnityEngine;

public class SlotFarmPlantedState : UnitState<SlotFarm>
{
    private readonly float growingTime = 25.0f;
    private readonly float readyToHarvestTime = 15f;

    private bool harvestControl;

    public override int getUnitCurrentStateKey()
    {
        return (int)SlotFarmEnum.PLANTED;
    }

    public override void startState()
    {
        stateMachineObject.currentRespownTime = 0;
        harvestControl = false;
    }

    protected override void UpdateUnitState()
    {
        // if its was ready to harvest before and now is not, then its already harvest
        if (harvestControl && !stateMachineObject.isReadyToHarvest)
        {
            callNextState((int)SlotFarmEnum.START);
        }
        else if (!stateMachineObject.isReadyToHarvest && stateMachineObject.currentRespownTime >= readyToHarvestTime)
        {
            stateMachineObject.isReadyToHarvest = true;
            harvestControl = true;
        }

        stateMachineObject.currentRespownTime += Time.deltaTime;
        if(stateMachineObject.currentRespownTime > growingTime)
        {
            callNextState((int)SlotFarmEnum.START);
        }
    }
}
