using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineController<Player>
{
    public MovingObject movingObject;               // moving objects commom attributes

    public Rigidbody2D rigid;

    public PlayerInventory playerInventory;

    protected override void stateMachineAwake()
    { }

    protected override void stateMachineStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent("PlayerInventory") as PlayerInventory;
        movingObject = new MovingObject();
        movingObject.baseSpeed = 4;
    }

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    {
        OnMove();
    }

    protected override UnitState<Player> getFirstState()
    {
        return new PlayerIdle();
    }


    protected override Player getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        throw new System.NotImplementedException();
    }

    #region Moviment
    /// <summary>
    /// Calculate player moviment.
    /// </summary>
    void OnMove()
    {
        if (rigid != null)
        {
            rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Debug.Log("Plyaer -> rigid null");
        }
    }
    #endregion
}
