﻿using System.Collections;
using UnityEngine;

public class Chest : StateMachineController<Chest> //Furniture<ChestItem>
{
    [SerializeField]
    protected ChestItem furnitureAsset;         // item that this class intance will generate after collistion
    [SerializeField]
    protected SpriteRenderer sprite;            // sprite to render on screen

    public Rigidbody2D rigid;
    public MovingObject movingObject;

    protected override void stateMachineAwake()
    {
        movingObject = new MovingObject();
    }

    protected override void stateMachineStart()
    {
        furnitureAsset = Instantiate(furnitureAsset);
        sprite.sprite = furnitureAsset.sprite;
        furnitureAsset.itemName = "Baú alterado.";

        movingObject.baseSpeed = movingObject.currentSpeed = 3;
    }

    protected override void stateMachineUpdate()
    { }

    protected override void stateMachineFixedUpdate()
    {
        onMove();
    }

    protected override UnitState<Chest> getFirstState()
    {
        return getNextState((int)ChestStateEnum.CHEST_POSITIONING);
    }

    protected override Chest getStateMachineObject()
    {
        return this;
    }

    protected override void instantiateAllUnitStates()
    {
        addUnitStateInstance(new ChestPositioning());
    }

    /// <summary>
    /// Method to call interact() when player in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interact();
        }
    }

    /// <summary>
    /// Method to call getAway() when player is no more in collision with this object's interaction area.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            getAway();
        }
    }
    
    protected void getAway()
    {
        sprite.sprite = furnitureAsset.sprite;
    }

    protected void interact()
    {
        sprite.sprite = furnitureAsset.openedChesterSprite;
    }

    private void onMove()
    {
        rigid.MovePosition(rigid.position + movingObject.direction * movingObject.currentSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("@Collision: " + collision.gameObject.name);
    }
}
