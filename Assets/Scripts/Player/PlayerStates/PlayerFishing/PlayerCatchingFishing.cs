using UnityEngine;

public class PlayerCatchingFishing : UnitState<Player>
{
    private float time;
    public override int getUnitCurrentStateKey()
    {
        return (int)PlayerStatesEnum.CATCHING_FISHING;
    }

    public override void startState()
    {
        Debug.Log("Entrou em catching.");
        time = 0.0f;
    }

    protected override void UpdateUnitState()
    {
        time += Time.deltaTime;
        if (time >= 1.0f)
        {
            callNextState((int)PlayerStatesEnum.IDDLE);
        }
    }
}
