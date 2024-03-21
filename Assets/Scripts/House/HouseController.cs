using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HouseController : House<BuildingItemIdEnum>
{
    [Header("House Settings")]
    [SerializeField]
    private bool isBuilt;                               // flag that represents whether the house is already built or not
    [SerializeField]
    public bool isPositionedCorrecty;                   // flag to tell if this house is placed correctly
    [SerializeField]
    private bool isChimneyBuilt;                        // flag to tell if chimney is built in this house
    [SerializeField]
    private float houseMaxHitPoints;                    // max number of hit points this house can taken before turned into trash
    private float houseHitPoints;                       // current amount of hitpoints left this house has
    [SerializeField]
    private BuildingItemIdEnum houseType;               // type that represent this house.

    [Header("House Building Component")]
    [SerializeField]
    private BuildingBlockedArea buildingBlockedArea;    // instance to control when house is in building mode

    [Header("House Objects")]
    [SerializeField]
    private GameObject roof;                            // roof object of this house
    [SerializeField]
    private GameObject walls;                           // walls object of this house
    [SerializeField]
    private GameObject ground;                          // ground object of this house
    private HouseGround groundComponent;                // ground component that receives hit
    [SerializeField]
    private GameObject door;                            // door object of this house
    [SerializeField]
    private GameObject chimney;                         // chimeny object of this house
    [SerializeField]
    private GameObject constructionFance;               // construction object collection of this house

    [Header("Hitpoint Bar")]
    [SerializeField]
    private Image blueBar;                              // bar to represent current amount of hit points this house has left
    [SerializeField]
    private GameObject blueBarObject;                   // blue bar game object instance 

    private AgentExecutor executor;                     // agent executor to run agents

    // Start is called before the first frame update
    void Start()
    {
        construictionMode();
        if(!isBuilt)
            buildingBlockedArea.addListener(builtHouse);

        if (!isPositionedCorrecty)
            buildingBlockedArea.ativateInvalidPlace();

        if (isChimneyBuilt)
            chimney.SetActive(true);

        houseHitPoints = houseMaxHitPoints;

        groundComponent = ground.GetComponent<HouseGround>();
        groundComponent.addListeners(takeDamage);

        executor = new AgentExecutor();
    }

    private void Update()
    {
        executor.update();
    }

    /// <summary>
    /// When isBuilt is false, enable construction block area and disble houses object part, 
    /// and when isBuilt is true, the situation reverses.
    /// </summary>
    private void construictionMode()
    {
        constructionFance.SetActive(!isBuilt);

        roof.SetActive(isBuilt);
        walls.SetActive(isBuilt);
        ground.SetActive(isBuilt);
        door.SetActive(isBuilt);
    }

    /// <summary>
    /// Event to be called when house end construction mode.
    /// </summary>
    public void builtHouse()
    {
        isBuilt = true;
        blueBarObject.SetActive(false);
        construictionMode();
        doPuffFx();
    }

    /// <summary>
    /// Callback called when house taken a damage from an axe.
    /// </summary>
    /// <param name="amountHitPoints">Amount of damage taken.</param>
    public void takeDamage(int amountHitPoints, PlayerInventory playerInventory)
    {
        if (amountHitPoints >= 0 || (amountHitPoints < 0 && houseHitPoints < houseMaxHitPoints))
        {
            houseHitPoints -= amountHitPoints;
            if(amountHitPoints < 0)
                playerInventory.removeStoreItemAmount(ObjectIdEnum.PLANK, 1);

            updateFillBar();
            executor.addAgent(new HouseBuildBarAgent(blueBarObject));
            if (houseHitPoints <= 0)
            {
                doPuffFx();
                dropItems();
                Destroy(gameObject);
            }
        }
        //else if(amountHitPoints < 0)
        //{
        //    if(houseHitPoints < houseMaxHitPoints)
        //    {
        //        houseHitPoints -= amountHitPoints;
        //        playerInventory.removeStoreItemAmount(ObjectIdEnum.PLANK, 1);
        //    }
        //}

        //updateFillBar();
        //executor.addAgent(new HouseBuildBarAgent(blueBarObject));
        //if (houseHitPoints <= 0)
        //{
        //    doPuffFx();
        //    dropItems();
        //    Destroy(gameObject);
        //}
    }

    /// <summary>
    /// Generate items to be dropped.
    /// </summary>
    private void dropItems()
    {
        PlayerBuildingSkills buildingSkills = PlayerBuildingSkills.getInstance();

        List<DrawableItem> gameItems = buildingSkills.getItemAndGenerateMaterials((int)houseType, 2.0f);

        foreach(DrawableItem gameItem in gameItems)
        {
            GameObject droppedItem = AssetFactory.getInstance().instanceDroppedSceneryItem();

            droppedItem.GetComponent<DroppedSceneryItem>().setItem(gameItem);
            droppedItem.transform.position = transform.position;
            droppedItem.name = gameItem.itemName;
        }
    }

    /// <summary>
    /// Update house hitpoints and blueBar with new value.
    /// </summary>
    private void updateFillBar()
    {
        blueBar.fillAmount = houseHitPoints / houseMaxHitPoints;
    }

    /// <summary>
    /// Enable chimney in this house. It's start deactivated until player 
    /// build it inside a house.
    /// </summary>
    public void enableChimney()
    {
        isChimneyBuilt = true;
        chimney.SetActive(true);
    }

    /// <summary>
    /// Show puff effect.
    /// </summary>
    private void doPuffFx()
    {
        GameObject puff = AssetFactory.getInstance().instanceFxGameObjectByType((int)FxEnum.PUFF_SMOKE);
        puff.transform.position = transform.position;
        puff.transform.localScale = new Vector3(4f, 4f, 4f);
    }
}
