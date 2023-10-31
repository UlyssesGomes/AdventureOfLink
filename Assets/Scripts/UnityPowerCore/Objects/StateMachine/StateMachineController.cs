using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineController<T> : MonoBehaviour
{
    protected UnitState<T> currentUnitState;                        // current unit state in execution

    protected int _objectUnitStateId;                               // States that object can assume like idle, walking, attack ...

    private IDictionary<int, UnitState<T>> instanceStates;          // UnitStates intances

    public int objectUnitStateId
    {
        get { return _objectUnitStateId; }
    }

    private void Awake()
    {
        instanceStates = new Dictionary<int, UnitState<T>>();
        stateMachineAwake();
    }

    // Start is called before the first frame update
    void Start()
    {
        instantiateAllUnitStates();
        stateMachineStart();
        currentUnitState = getFirstState();
        _objectUnitStateId = currentUnitState.getUnitCurrentStateKey();
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
            UnitState<T> nextState = getNextState(currentUnitState.nextStateKey);
            currentUnitState = nextState;
            if(currentUnitState.stateMachineObject == null)
            {
                currentUnitState.stateMachineObject = getStateMachineObject();
            }
            _objectUnitStateId = currentUnitState.getUnitCurrentStateKey();
            currentUnitState.Start();
        }
    }

    private void FixedUpdate()
    {
        stateMachineFixedUpdate();
    }

    /// <summary>
    /// Add a new UnitState instance if it isnt already added.
    /// </summary>
    /// <param name="unitState"></param>
    protected void addUnitStateInstance(UnitState<T> unitState)
    {
        if(!instanceStates.ContainsKey(unitState.getUnitCurrentStateKey()))
        {
            instanceStates.Add(unitState.getUnitCurrentStateKey(), unitState);
        }
    }

    /// <summary>
    /// Try get next instance UnitState.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Next instance UnitState</returns>
    protected UnitState<T> getNextState(int key) 
    {
        UnitState<T> next = getInstanceByKey(key);
        if (next == null)
        {
            throw new System.NotImplementedException("Unit State with key " + key + 
                " has not been instantiated yet. Add this UnitState in instantiateAllUnitStates() method.");
        }
        return next;
    }

    /// <summary>
    /// Get instance of a state by its key.
    /// </summary>
    /// <param name="key">key that identifies a instance state</param>
    /// <returns>UnitState instance</returns>
    private UnitState<T> getInstanceByKey(int key)
    {
        if (instanceStates.ContainsKey(key))
        {
            return instanceStates[key];
        }

        return null;
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

    /// <summary>
    /// On derivated StateMachineController Instantiate all UnitStates once to avoid 
    /// make new instances during game execution, because UnitStates are switched frequently. 
    /// </summary>
    protected abstract void instantiateAllUnitStates();
}
