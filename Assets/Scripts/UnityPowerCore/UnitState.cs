using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState
{
    public bool isRunning;                              // state of UnitState, when false, call NextUnitState()

    public PlayerObject player;                         // instance of the player to be controlled

    protected static IDictionary<int, UnitState> instanceStates = new Dictionary<int, UnitState>();   

    // Start is called before the first frame update
    public void Start()
    {
        isRunning = true;
        player.playerState = getUnitCurrentState();

        if(!instanceStates.ContainsKey(getUnitCurrentState()))
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

    /*
     * Change to next state
     */
    public UnitState NextUnitState()
    {
        UnitState next = Next();
        next.player = player;
        next.Start();

        return next;
    }

    public static UnitState getInstance(int key)
    {
        if (instanceStates.ContainsKey(key))
        {
            return instanceStates[key];
        }

        return null;
    }

    public void addInstance(int key, UnitState state)
    {
        if (!instanceStates.ContainsKey(key))
        {
            instanceStates.Add(key, state);
        }
    }

    /*
     * Implements logic to change the current state returning to new state.
     */
    protected abstract UnitState Next();

    /*
     * Implement state update and veiry
     */
    protected abstract void UpdateUnitState();

    /*
     * Return player current state
     */
    public abstract int getUnitCurrentState();

    /*
     * Called in Start() method. Derivated classes must implement this method instead Start()
     */
    public abstract void startState();
}
