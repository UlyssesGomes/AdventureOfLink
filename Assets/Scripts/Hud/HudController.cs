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

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyUp(KeyCode.B) && (currentActiveUi == ActiveUiEnum.BACKPACK || currentActiveUi == ActiveUiEnum.NONE))
        {
            inventorySystem.backpackChangeVisibility();
            if (isInSelection)
            {
                isInSelection = !isInSelection;
                currentActiveUi = ActiveUiEnum.NONE;
            }
            else
            {
                isInSelection = !isInSelection;
                currentActiveUi = ActiveUiEnum.BACKPACK;
            }
        }
        else if (Input.GetKeyUp(KeyCode.M) && (currentActiveUi == ActiveUiEnum.BUILDING_MENU || currentActiveUi == ActiveUiEnum.NONE))
        {
            buildingMenuController.invertOpenState();
            if (buildingMenuController.buildingMenuVisibility())
                currentActiveUi = ActiveUiEnum.BUILDING_MENU;
            else
                currentActiveUi = ActiveUiEnum.NONE;
        }

        if(isInSelection)
        {
            if(Input.GetKeyUp(KeyCode.RightArrow))
            {
                //if(slotSelectedIndex < inventorySlots.Length - 1)
                //{
                //    slotSelectedIndex++;
                //    notifyIndex(slotSelectedIndex);
                //}
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if(slotSelectedIndex > 0)
                {
                    slotSelectedIndex--;
                }
            }

        }
        else if (currentActiveUi == ActiveUiEnum.NONE)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                inventorySystem.setSwitablePlayerItem(0);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                inventorySystem.setSwitablePlayerItem(1);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                inventorySystem.setSwitablePlayerItem(2);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                inventorySystem.setSwitablePlayerItem(3);
                buildingMenuController.updateBuildingMenuAccess();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                inventorySystem.setSwitablePlayerItem(4);
                buildingMenuController.updateBuildingMenuAccess();
            }
        }
    }

}
