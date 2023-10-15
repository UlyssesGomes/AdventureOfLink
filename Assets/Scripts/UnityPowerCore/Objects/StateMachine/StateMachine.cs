using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : StateMachineObject
{
    protected UnitState<T> currentUnitState;            // current unit state in execution

    protected int _objectStateId;                         // States that object can assume like idle, walking, attack ...

    public T stateMachineObject;

    public int objectStateId
    {
        get { return _objectStateId; }
        set { _objectStateId = value; }
    }

    void Start()
    {
        stateMachineStart();
        currentUnitState = getFirstState();
        objectStateId = currentUnitState.getUnitCurrentState();
        currentUnitState.stateMachineObject = getStateMachineObject();
        currentUnitState.Start();
        stateMachineObject.objectStart();
    }


    // Update is called once per frame
    void Update()
    {
        stateMachineUpdate();
        stateMachineObject.objectUpdate();
        currentUnitState.Update();

        if (!currentUnitState.isRunning)
        {
            UnitState<T> nextState = currentUnitState.NextUnitState();
            currentUnitState = nextState;
            objectStateId = currentUnitState.getUnitCurrentState();
        }
    }

    private void FixedUpdate()
    {
        stateMachineFixedUpdate();
        stateMachineObject.objectFixedUpdate();
    }

    /// <summary>
    /// Object must implement stateMachineStart() instead Start()
    /// </summary>
    protected abstract void stateMachineStart();

    /// <summary>
    /// Derivated StateMachine must implement stateMachineUpdate() instead Update()
    /// </summary>
    protected abstract void stateMachineUpdate();

    /// <summary>
    /// Derivated StateMachine must implement stateMachineFixedUpdate instead FixedUpdate();
    /// </summary>
    protected abstract void stateMachineFixedUpdate();

    /// <summary>
    /// Implement method to instantiate and return the initial state.
    /// </summary>
    /// <returns>Initial UnitState of this machine.</returns>
    protected abstract UnitState<T> getFirstState();

    /// <summary>
    /// Implement method that return StateMachineObject instance.
    /// </summary>
    /// <returns>StateMachineObject intance of type T</returns>
    protected abstract T getStateMachineObject();
}
