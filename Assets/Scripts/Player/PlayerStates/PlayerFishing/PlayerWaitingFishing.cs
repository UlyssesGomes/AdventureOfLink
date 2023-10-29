﻿using UnityEngine;

public class PlayerWaitingFishing : UnitState<Player>
{
    private float countDownTime;        // time left before change state
    private bool isFishCatch;           // if true, player catch a fish, but not added in inventory yet

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.WAITING_FISHING;
    }

    public override void startState()
    {
        FishingRodItem item = stateMachineObject.playerInventory.getCurrentSwitableItem() as FishingRodItem;
        float drawnNumber = Random.Range(0.0f, 1.0f);
        if(item.odds >= drawnNumber)
        {
            isFishCatch = true;
            countDownTime = Random.Range(3, 10);
            Debug.Log("Fish catched.");
        }
        else
        {
            isFishCatch = false;
            countDownTime = Random.Range(7, 10);
            Debug.Log("Fish dont catched. Sorteado: " + drawnNumber);
        }
        
    }

    protected override void UpdateUnitState()
    {
        countDownTime -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            callNextState((int)PlayerStatesEnum.CATCHING_FISHING);
        }

        if(countDownTime <= 0.0f && isFishCatch)
        {
            callNextState((int)PlayerStatesEnum.REELING_FISHING);
        }
        else if(countDownTime <= 0.0f && !isFishCatch)
        {
            callNextState((int)PlayerStatesEnum.CATCHING_FISHING);
        }
    }
}
