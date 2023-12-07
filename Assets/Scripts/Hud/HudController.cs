using System.Collections.Generic;
using UnityEngine;

enum ActiveUiEnum: int { NONE, BUILDING_MENU, BACKPACK };

public class HudController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private InventorySystem inventorySystem;

    [SerializeField]
    private BuildingMenuController buildingMenuController;

    private int slotSelectedIndex;

    private List<Observer<int>> slotListObservers;

    private bool isInSelection;
    private ActiveUiEnum currentActiveUi;

    private InputManager<InputAgentsEnum> input;

    // Start is called before the first frame update
    void Start()
    {
        input = new InputManager<InputAgentsEnum>(InputAgentsEnum.UI_CONTROL);

        slotListObservers = new List<Observer<int>>();
        slotSelectedIndex = 0;
        isInSelection = inventorySystem.backpackVisibility();

        if (inventorySystem.backpackVisibility())
            currentActiveUi = ActiveUiEnum.BACKPACK;
        else if (buildingMenuController.buildingMenuVisibility())
            currentActiveUi = ActiveUiEnum.BUILDING_MENU;
        else
            currentActiveUi = ActiveUiEnum.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetKeyUp(KeyCode.B) && (currentActiveUi == ActiveUiEnum.BACKPACK || currentActiveUi == ActiveUiEnum.NONE))
        {
            inventorySystem.backpackChangeVisibility();
            if (isInSelection)
            {
                InputManager<InputAgentsEnum>.isLocked = false;
                isInSelection = !isInSelection;
                currentActiveUi = ActiveUiEnum.NONE;
            }
            else
            {
                InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.UI_CONTROL;
                InputManager<InputAgentsEnum>.isLocked = true;
                isInSelection = !isInSelection;
                currentActiveUi = ActiveUiEnum.BACKPACK;
            }
        }
        else if (input.GetKeyUp(KeyCode.M) && (currentActiveUi == ActiveUiEnum.BUILDING_MENU || currentActiveUi == ActiveUiEnum.NONE))
        {
            buildingMenuController.invertOpenState();
            if (buildingMenuController.buildingMenuVisibility())
            {
                InputManager<InputAgentsEnum>.allowedControllingAgent = InputAgentsEnum.UI_CONTROL;
                InputManager<InputAgentsEnum>.isLocked = true;
                currentActiveUi = ActiveUiEnum.BUILDING_MENU;
            }
            else
            {
                InputManager<InputAgentsEnum>.isLocked = false;
                currentActiveUi = ActiveUiEnum.NONE;
            }
        }

        if(isInSelection)
        {
            if(input.GetKeyUp(KeyCode.RightArrow))
            {
                //if(slotSelectedIndex < inventorySlots.Length - 1)
                //{
                //    slotSelectedIndex++;
                //    notifyIndex(slotSelectedIndex);
                //}
            }
            else if(input.GetKeyUp(KeyCode.LeftArrow))
            {
                if(slotSelectedIndex > 0)
                {
                    slotSelectedIndex--;
                }
            }

        }
        else if (currentActiveUi == ActiveUiEnum.NONE)
        {
            if (input.GetKeyUp(KeyCode.Alpha1))
            {
                inventorySystem.setSwitablePlayerItem(0);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (input.GetKeyUp(KeyCode.Alpha2))
            {
                inventorySystem.setSwitablePlayerItem(1);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (input.GetKeyUp(KeyCode.Alpha3))
            {
                inventorySystem.setSwitablePlayerItem(2);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (input.GetKeyUp(KeyCode.Alpha4))
            {
                inventorySystem.setSwitablePlayerItem(3);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (input.GetKeyUp(KeyCode.Alpha5))
            {
                inventorySystem.setSwitablePlayerItem(4);
                buildingMenuController.updateBuildingMenuAccess();
            }
        }
    }

}
