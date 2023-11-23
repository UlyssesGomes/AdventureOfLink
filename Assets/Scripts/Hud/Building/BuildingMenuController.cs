using UnityEngine;
using UnityEngine.UI;

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
            player.playerBuildingSkills.housesSkills,
            player.playerBuildingSkills.refinedSkills,
            player.playerBuildingSkills.toolsSkills
        };

        buildingSkillUiSystem.setSkills(playerBuildingSkills[0]);
        currentBuildingSkill = playerBuildingSkills[0][0];
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
    public void openClose()
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
}
