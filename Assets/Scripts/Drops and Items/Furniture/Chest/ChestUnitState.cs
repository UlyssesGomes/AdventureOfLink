public abstract class ChestUnitState : UnitState<Chest>
{
    protected InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.CHEST);
}
