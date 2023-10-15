using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<Player>
{
    /// <summary>
    /// Get initial player state. By default player init idle.
    /// </summary>
    /// <returns></returns>
    protected override UnitState<Player> getFirstState()
    {
        return new PlayerIdle();
    }

    protected override Player getStateMachineObject()
    {
        return stateMachineObject;
    }

    protected override void stateMachineFixedUpdate()
    { }

    protected override void stateMachineStart()
    {
        stateMachineObject = new Player();
        stateMachineObject.rigid = GetComponent<Rigidbody2D>();
        stateMachineObject.playerInventory = GetComponent("PlayerInventory") as PlayerInventory;
    }

    protected override void stateMachineUpdate()
    { }
}
