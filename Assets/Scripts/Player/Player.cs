using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineController<Player>
{
    public MovingObject movingObject;                           // moving objects commom attributes

    public Rigidbody2D rigid;                                   // collision component

    public PlayerInventory playerInventory;                     // inventory script

    public PlayerBuildingSkills playerBuildingSkills;           // building skills script

    public bool isFishing;                                      // if true, player can fishing

    public bool reachFinalSpriteFrame;                          // used to control by animator when the last frame of animation is displayed

    public DropAssetManager assetManager;                       // Manager of assets available in memory.

    [SerializeField]
    private PlayerIconsEnum [] playerIconsEnum;                 // array of int respresent icons
    [SerializeField]
    private Sprite [] playerIconsSprite;                        // array of sprites represent icons in same order that playerIconsEnum
    
    private Dictionary<PlayerIconsEnum, Sprite> icons;          // hash table to get icons quickly
    [SerializeField]
    private SpriteRenderer iconSpriteRenderer;                  // sprite renderer to show icons

    protected override void stateMachineAwake()
    {
        icons = new Dictionary<PlayerIconsEnum, Sprite>();
        for (int u = 0; u < playerIconsEnum.Length; u++)
        {
            icons.Add(playerIconsEnum[u], playerIconsSprite[u]);
        }
    }

    protected override void stateMachineStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent("PlayerInventory") as PlayerInventory;
        playerBuildingSkills = GetComponent("PlayerBuildingSkills") as PlayerBuildingSkills;
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
        addUnitStateInstance(new PlayerDontCatchFishing());
        addUnitStateInstance(new PlayerBuilding());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlotFarm"))
        {
            collision.transform.GetComponent<SlotFarm>().doHarvest();
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
