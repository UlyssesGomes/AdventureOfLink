using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState<T>
{
    public bool isRunning;                         // state of UnitState, when false, call NextUnitState()

    public T stateMachineObject;                   // instance of the object to be controlled

    protected static IDictionary<int, UnitState<T>> instanceStates = new Dictionary<int, UnitState<T>>();   

    // Start is called before the first frame update
    public void Start()
    {
        isRunning = true;

        if(!instanceStates.ContainsKey(getUnitCurrentState()) )
        {
            instanceStates.Add(getUnitCurrentState(), this);
        }
        startState();
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateUnitState();
    }

    /// <summary>
    /// Change to next state
    /// </summary>
    /// <returns>Next State</returns>
    public UnitState<T> NextUnitState()
    {
        UnitState<T> next = Next();
        next.stateMachineObject = stateMachineObject;
        next.Start();

        return next;
    }

    /// <summary>
    /// Get instance of a state by its key.
    /// </summary>
    /// <param name="key">key that identifies a instance state</param>
    /// <returns>UnitState instance</returns>
    public static UnitState<T> getInstance(int key)
    {
        if (instanceStates.ContainsKey(key))
        {
            return instanceStates[key];
        }

        return null;
    }

    /// <summary>
    /// Adds a new state instance if it has not already been added
    /// </summary>
    /// <param name="key"></param>
    /// <param name="state"></param>
    public void addInstance(int key, UnitState<T> state)
    {
        if (!instanceStates.ContainsKey(key))
        {
            instanceStates.Add(key, state);
        }
    }

    /// <summary>
    /// Implements logic to change the current state returning to new state.
    /// </summary>
    /// <returns></returns>
    protected abstract UnitState<T> Next();

    /// <summary>
    /// Implement state update
    /// </summary>
    protected abstract void UpdateUnitState();

    /// <summary>
    /// Return current state id
    /// </summary>
    /// <returns></returns>
    public abstract int getUnitCurrentState();

    /// <summary>
    /// Called in Start() method. Derivated (last child instance of UnitState)
    /// classes must implement this method instead Start()
    /// </summary>
    public abstract void startState();
}
