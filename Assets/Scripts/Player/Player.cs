using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineObject
{
    public MovingObject movingObject;       // moving objects commom attributes

    public Rigidbody2D rigid;

    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    public void objectStart()
    {
        movingObject = new MovingObject();
        movingObject.baseSpeed = 4;
    }

    public void objectUpdate()
    { }

    /// <summary>
    /// Call OnMove to calculate player moviment.
    /// </summary>
    public void objectFixedUpdate()
    {
        OnMove();
    }

    #region Moviment
    /// <summary>
    /// Calculate player moviment.
    /// </summary>
    void OnMove()
    {
        rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
    }

    #endregion
}
