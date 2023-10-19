using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState<T>
{
    public bool isRunning;                          // state of UnitState, when false, call NextUnitState()
    public int nextStateKey;                        // key of the nextState

    public T stateMachineObject;                    // instance of the object to be controlled

    
    // Start is called before the first frame update
    public void Start()
    {
        isRunning = true;
        startState();
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateUnitState();
    }

    /// <summary>
    /// Stop running this state and call next state.
    /// </summary>
    protected void callNextState(int keyState)
    {
        isRunning = false;
        nextStateKey = keyState;
    }

    /// <summary>
    /// Called in Start() method. Derivated (last child instance of UnitState)
    /// classes must implement this method instead Start()
    /// </summary>
    public abstract void startState();

    /// <summary>
    /// Implement state update
    /// </summary>
    protected abstract void UpdateUnitState();

    /// <summary>
    /// Return current state id
    /// </summary>
    /// <returns></returns>
    public abstract int getUnitCurrentStateKey();
}
