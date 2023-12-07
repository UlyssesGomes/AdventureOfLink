using UnityEngine;

public abstract class PlayerUnitState : UnitState<Player>
{
    protected static InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.PLAYER);   // input manager instance
}
