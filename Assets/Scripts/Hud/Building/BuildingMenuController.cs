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

    private void Start()
    {
        animator.SetBool("isOpened", isOpened);
        updateBuildingMenuAccess();

        buildingSkillUiSystem.setSkills(player.playerBuildingSkills.housesSkills);
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
}
