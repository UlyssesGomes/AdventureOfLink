using UnityEngine;
using UnityEngine.UI;

using System;

public class BuildingMenuController : MonoBehaviour
{
    [SerializeField]
    private Player player;                                  // player reference
    [SerializeField]
    private Animator animator;                              // animator to control open and close animations

    [SerializeField]
    private Color enabledColor;                             // color to show enable icon build menu
    [SerializeField]
    private Color disabledColor;                            // color to show disabled icon build menu

    [SerializeField]
    private Image backgroundIcon;                           // building manu icon background image
    [SerializeField]
    private Image icon;                                     // building menu icon image

    [SerializeField]
    private bool isOpened;                                  // flag to control open and close state
    private bool isEnable;                                  // flag to control permission to open build menu

    [SerializeField]
    private BuildingSkillUiSystem buildingSkillUiSystem;    // building skill ui presentention controller
    private BuildingSkill currentBuildingSkill;             // current building skill to be displayed

    [SerializeField]
    private Tabs tabs;                                      // tabs contoller

    [SerializeField]
    private SkillDescriptionPanel descriptionPanel;         // object description panel controller

    private BuildingSkill[][] playerBuildingSkills;         // matriz with all arrays building skills

    private void Start()
    {
        animator.SetBool("isOpened", isOpened);
        updateBuildingMenuAccess();

        playerBuildingSkills = new BuildingSkill[][]
        {
            player.playerBuildingSkills.toolsSkills,
            player.playerBuildingSkills.refinedSkills,
            player.playerBuildingSkills.housesSkills
        };

        buildingSkillUiSystem.setSkills(playerBuildingSkills[0]);
        currentBuildingSkill = playerBuildingSkills[0][0];

        string[] labels = { "Ferramentas", "Refinados", "Casa" };
        tabs.setLabelsTabs(labels);
    }

    private void Update()
    {
        if (isOpened)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (tabs.changeTab(ChangeTabsOptionsEnum.PREVIOUS))
                {
                    buildingSkillUiSystem.setSkills(playerBuildingSkills[tabs.selectedTabIndex]);
                    currentBuildingSkill = playerBuildingSkills[tabs.selectedTabIndex][0];
                    setDescription(currentBuildingSkill);
                }            
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                if (tabs.changeTab(ChangeTabsOptionsEnum.NEXT))
                {
                    buildingSkillUiSystem.setSkills(playerBuildingSkills[tabs.selectedTabIndex]);
                    currentBuildingSkill = playerBuildingSkills[tabs.selectedTabIndex][0];
                    setDescription(currentBuildingSkill);
                }
            }

            if(Input.GetKeyUp(KeyCode.RightArrow))
            {
                currentBuildingSkill = buildingSkillUiSystem.setSkillSelected(MoveOptionsEnum.RIGHT);
                if (currentBuildingSkill != null)
                    setDescription(currentBuildingSkill);
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow))
            {
                currentBuildingSkill = buildingSkillUiSystem.setSkillSelected(MoveOptionsEnum.LEFT);
                if (currentBuildingSkill != null)
                    setDescription(currentBuildingSkill);
            }
        }
    }

    /// <summary>
    /// Change between opened and closed state of BuildingMenu.
    /// If its opened this method will close and open if its closed.
    /// </summary>
    public void invertOpenState()
    {
        if(isOpened || isEnable)
        {
            isOpened = !isOpened;
            animator.SetBool("isOpened", isOpened);
        }

        if(isOpened)
            setDescription(currentBuildingSkill);
    }

    /// <summary>
    /// Tells if BuildingMenu is active or not.
    /// </summary>
    /// <returns>bool represents BuildingMenu state</returns>
    public bool buildingMenuVisibility()
    {
        return isOpened;
    }

    /// <summary>
    /// Enable building menu when player is using a HAMMER_BUILD item and disable
    /// otherwise.
    /// </summary>
    public void updateBuildingMenuAccess()
    {
        GameItem gameItem = player.playerInventory.getCurrentSwitableItem();
        if (gameItem != null && gameItem.type == ItemTypeEnum.HAMMER_BUILD)
        {
            isEnable = true;
            backgroundIcon.color = enabledColor;
            icon.color = enabledColor;
        }
        else
        {
            isEnable = false;
            backgroundIcon.color = disabledColor;
            icon.color = disabledColor;
        }
    }

    /// <summary>
    /// Get all material amount in player inventory and set descriptionPanel with
    /// buildingSkill and all available material in inventory.
    /// </summary>
    /// <param name="buildingSkill">BuildingSkill to be displayed</param>
    private void setDescription(BuildingSkill buildingSkill)
    {
        int [] playerMaterialAmountArray = { 0, 0, 0 };

        for(int u = 0; u < buildingSkill.material.Length; u++)
        {
            playerMaterialAmountArray[u] = player.playerInventory.countItemAmountByItemId(buildingSkill.material[u].itemId);
        }

        descriptionPanel.setDescriptionPanel(buildingSkill, playerMaterialAmountArray);
    }

    /// <summary>
    /// Generate item from building skill selected.
    /// </summary>
    public void generateButtonEvent()
    {
        if (descriptionPanel.currentBuildingSkill.type == BuildingSkillGroupType.TOOLS)
        {
            bool isRemovedAll = removeMaterialItems(descriptionPanel.currentBuildingSkill.material);

            if (isRemovedAll)
            {
                GameItem g = player.assetManager.intanceGameItemByItemId((int)descriptionPanel.currentBuildingSkill.itemId);
                g.amount = descriptionPanel.currentBuildingSkill.amountGenerated;
                int amountAdded = player.playerInventory.addStoreItem(g);

                if(amountAdded == g.amount)
                {
                    // TODO - Notify item added to inventory  when notify system were implemented.
                    setDescription(currentBuildingSkill);
                }
                else
                {
                    // TODO - Notify inventory full when notify system were implemented.
                    // TODO - Drop the item on the floor 
                }
            }
        } 
        else if(descriptionPanel.currentBuildingSkill.type == BuildingSkillGroupType.REFINED)
        {

        } 
        else if(descriptionPanel.currentBuildingSkill.type == BuildingSkillGroupType.HOUSE)
        {

        }
    }

    /// <summary>
    /// Remove all materials from inventory, and if was a success operation will return true,
    /// otherwise return false and return all items to player inventory.
    /// </summary>
    /// <param name="buildingSkill"></param>
    /// <returns></returns>
    private bool removeMaterialItems(Material [] materials)
    {
        int[] removedAmount = { 0, 0, 0 };
        for (int u = 0; u < materials.Length; u++)
        {
            removedAmount[u] = player.playerInventory.removeStoreItemAmount(materials[u].itemId, materials[u].amount);

            if(removedAmount[u] != materials[u].amount)
            {
                restoreRemovedItems(removedAmount, materials);
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Restore all items removed to PlayerInventory.
    /// </summary>
    /// <param name="amountRemoved"></param>
    /// <param name="materials"></param>
    private void restoreRemovedItems(int []amountRemoved, Material [] materials)
    {
        throw new Exception("[BuildingMenuController.restoreRemovedItems()] - method fail to return items to inventory.");
    }
}
