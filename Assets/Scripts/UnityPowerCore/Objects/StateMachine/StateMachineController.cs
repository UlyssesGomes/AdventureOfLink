using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineController<T> : MonoBehaviour
{
    protected UnitState<T> currentUnitState;           // current unit state in execution

    protected int _objectStateId;                   // States that object can assume like idle, walking, attack ...

    public int objectStateId
    {
        get { return _objectStateId; }
        set { _objectStateId = value; }
    }

    private void Awake()
    {
        stateMachineAwake();
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachineStart();
        currentUnitState = getFirstState();
        objectStateId = currentUnitState.getUnitCurrentState();
        currentUnitState.stateMachineObject = getStateMachineObject();
        currentUnitState.Start();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachineUpdate();
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
    }

    /// <summary>
    /// Object must implement stateMachineAwake() instead Awake()
    /// </summary>
    protected abstract void stateMachineAwake();

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
    /// Implement method that return StateMachineController derivated object instance.
    /// </summary>
    /// <returns>StateMachineObject intance of type T</returns>
    protected abstract T getStateMachineObject();
}
