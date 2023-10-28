using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineController<Player>
{
    public MovingObject movingObject;               // moving objects commom attributes

    public Rigidbody2D rigid;                       // collision component

    public PlayerInventory playerInventory;         // inventory script

    public bool isFishing;                          // if true, player can fishing

    public bool reachFinalSpriteFrame;              // used to control by animator when the last frame of animation is displayed

    protected override void stateMachineAwake()
    { }

    protected override void stateMachineStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent("PlayerInventory") as PlayerInventory;
        movingObject = new MovingObject();
        movingObject.baseSpeed = 4;
        isFishing = false;
    }

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    {
        OnMove();
    }

    protected override UnitState<Player> getFirstState()
    {
        return getNextState((int)PlayerStatesEnum.IDDLE);
    }


    protected override Player getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        addUnitStateInstance(new PlayerIdle());
        addUnitStateInstance(new PlayerCutting());
        addUnitStateInstance(new PlayerDigging());
        addUnitStateInstance(new PlayerRolling());
        addUnitStateInstance(new PlayerRunning());
        addUnitStateInstance(new PlayerWalking());
        addUnitStateInstance(new PlayerWatering());
        addUnitStateInstance(new PlayerCastingFishing());
        addUnitStateInstance(new PlayerCastingFishingOnWater());
        addUnitStateInstance(new PlayerWaitingFishing());
        addUnitStateInstance(new PlayerReelingFishing());
        addUnitStateInstance(new PlayerCatchingFishing());
        addUnitStateInstance(new PlayerBackFishing());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlotFarm"))
        {
            collision.transform.GetComponent<SlotFarm>().doHarvest();
        }
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
