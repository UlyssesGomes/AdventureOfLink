using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerObject : MovingObject
{
    protected UnitState currentUnitState;           // current unit state in execution

    protected int _playerState;                     // States that player can assume like idle, walking, attack ...

    public int playerState
    {
        get { return _playerState; }
        set { _playerState = value; }
    }

    void Start()
    {
        PlayerStart();
        currentUnitState = getFirstState();
        currentUnitState.player = this;
        currentUnitState.Start();
    }


    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
        currentUnitState.Update();

        if (!currentUnitState.isRunning)
        {
            UnitState nextState = currentUnitState.NextUnitState();
            currentUnitState = nextState;
        }
    }

    /*
     * Player must implement PlayerStart() instead Start()
     */
    protected abstract void PlayerStart();

    /*
     * Player must implement PlayerUpdate() instead Update()
     */
    protected abstract void PlayerUpdate();

    protected abstract UnitState getFirstState();
}
