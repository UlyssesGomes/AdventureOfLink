﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : StateMachineController<Player>
{
    public MovingObject movingObject;                           // moving objects commom attributes

    public Rigidbody2D rigid;                                   // collision component

    public PlayerInventory playerInventory;                     // inventory script

    public PlayerBuildingSkills playerBuildingSkills;           // building skills script

    public FurniturePlacement furniturePlacement;               // component with placemente furniture implementation to guide player to place furniture
    public PlayerGizmosGuide gizmosGuide;                       // gizmos to help player to place furniture on the map

    public bool isFishing;                                      // if true, player can fishing

    public bool reachFinalSpriteFrame;                          // used to control by animator when the last frame of animation is displayed

    private int isInDoorIntractArea;                            // control in how many door interact area player is
    private Door door;                                          // the door under interaction

    public AssetFactory assetfactory;                           // Manager of assets available in memory.

    [SerializeField]
    private PlayerIconsEnum [] playerIconsEnum;                 // array of int respresent icons
    [SerializeField]
    private Sprite [] playerIconsSprite;                        // array of sprites represent icons in same order that playerIconsEnum
    
    private Dictionary<PlayerIconsEnum, Sprite> icons;          // hash table to get icons quickly
    [SerializeField]
    private SpriteRenderer iconSpriteRenderer;                  // sprite renderer to show icons

    public GameObject doingBar;                                 // game obeject of the bar to show doing progress
    public Image doingFilledBar;                                // bar to control doing filled amount
    public float doingTimer;                                    // amount of time to run player doing state. If > 0.0f doing state will run or is runing, otherwise doing state its not running.
    public float maxDoingTimer;                                 // initial amount of time

    protected static InputManager<InputAgentsEnum> input = new InputManager<InputAgentsEnum>(InputAgentsEnum.PLAYER);   // input manager instance

    protected override void stateMachineAwake()
    {
        icons = new Dictionary<PlayerIconsEnum, Sprite>();
        for (int u = 0; u < playerIconsEnum.Length; u++)
        {
            icons.Add(playerIconsEnum[u], playerIconsSprite[u]);
        }
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void stateMachineStart()
    {
        playerInventory = GetComponent("PlayerInventory") as PlayerInventory;
        playerBuildingSkills = GetComponent("PlayerBuildingSkills") as PlayerBuildingSkills;
        movingObject = new MovingObject();
        movingObject.baseSpeed = 4;
        isFishing = false;

        furniturePlacement.enablePlacement(false);

        doingBar.SetActive(false);

        isInDoorIntractArea = 0;
    }

    protected override void stateMachineUpdate()
    {
        if(isInDoorIntractArea > 0)
        {
            if(input.GetKeyDown(KeyCode.F))
            {
                door.openDoor(this);
            }
        }
    }

    protected override void stateMachineFixedUpdate()
    {
        if(rigid.bodyType != RigidbodyType2D.Static)
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
        addUnitStateInstance(new PlayerDontCatchFishing());
        addUnitStateInstance(new PlayerBuilding());
        addUnitStateInstance(new PlayerDoing());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlotFarm"))
        {
            collision.transform.GetComponent<SlotFarm>().doHarvest();
        }
        else if (collision.CompareTag("Door"))
        {
            door = collision.GetComponentInParent<Door>();
            isInDoorIntractArea++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            isInDoorIntractArea--;
            if(isInDoorIntractArea <= 0)
            {
                isInDoorIntractArea = 0;
                door = null;
            }
        }
    }

    /// <summary>
    /// Select icon to show by its enum.
    /// </summary>
    /// <param name="icon">Id that represent its icon desired</param>
    public void showIconById(PlayerIconsEnum icon)
    {
        iconSpriteRenderer.sprite = icons[icon];
        iconSpriteRenderer.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide player icon.
    /// </summary>
    public void hideIcon()
    {
        iconSpriteRenderer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Lock player to be moved or not by collision. If isLocked true then
    /// the player will be set to rigid body type to static, dynamic will
    /// be set otherwise.
    /// </summary>
    /// <param name="isLocked"></param>
    public void setLockPlayer(bool isLocked)
    {
        if (isLocked)
        {
            rigid.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    /// <summary>
    /// Enable placement ui and show placement limits and item.
    /// </summary>
    /// <param name="itemId">id of the item that will be positioned</param>
    public void enablePlacementUi(int itemId)
    {
        furniturePlacement.setDataPlacement(this, assetfactory.checkItemInfo((int)ItemIdEnum.CHEST) as DrawableItem);
        furniturePlacement.enablePlacement(true);
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
