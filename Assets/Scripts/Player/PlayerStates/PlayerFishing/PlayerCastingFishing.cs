using UnityEngine;

public class PlayerCastingFishing : UnitState<Player>
{
    private float time;
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CASTING_FISHING;
    }

    public override void startState()
    {
        stateMachineObject.movingObject.currentSpeed = 0;
        time = 0.0f;
        UnityEngine.Debug.Log("Entrou em Casting.");
    }

    protected override void UpdateUnitState()
    {
        time += Time.deltaTime;
        if (stateMachineObject.isFishing)
        {
            //callNextState((int)PlayerStatesEnum.CASTING_FISHING_ON_WATER);
        }
        else if(time >= 1.0f)
        {
            callNextState((int)PlayerStatesEnum.CATCHING_FISHING);
        }
    }
}
