using UnityEngine;

public class PlayerReelingFishing : UnitState<Player>
{
    private float time;                              // count elapsed time
    private readonly float TOTAL_TIME = 5.0f;       // total time to wait before change state

    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.REELING_FISHING;
    }

    public override void startState()
    {
        time = 0f;
        stateMachineObject.showIconById(PlayerIconsEnum.EXCLAMATION);
    }

    protected override void UpdateUnitState()
    {
        time += Time.deltaTime;
        if (input.GetKeyDown(KeyCode.E))
        {
            tryAddFish();
            stateMachineObject.hideIcon();
            callNextState((int)PlayerStatesEnum.CATCHING_FISHING);
        }
        else if(time > TOTAL_TIME)
        {
            // TODO - notify lost fish when notify system were implemented.
            stateMachineObject.hideIcon();
            callNextState((int)PlayerStatesEnum.DONT_CATCH_FISHING);
        }
    }

    private void tryAddFish()
    {
        GameItem gameItem = stateMachineObject.assetManager.intanceGameItemByItemId((int)ItemIdEnum.FISH);
        int startAmount = gameItem.amount = 1;
        int addedAmount = stateMachineObject.playerInventory.addStoreItem(gameItem);

        if (startAmount == addedAmount)
        {
            // TODO - Notify inventory full when notify system were implemented.
        }
        else
        {
            // TODO - Notify fish added to inventory  when notify system were implemented.
        }
    }
}
