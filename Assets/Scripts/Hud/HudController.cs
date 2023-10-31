using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private InventorySystem inventorySystem;

    private int slotSelectedIndex;

    private List<Observer<int>> slotListObservers;

    private bool isInSelection;

    // Start is called before the first frame update
    void Start()
    {
        slotListObservers = new List<Observer<int>>();
        slotSelectedIndex = 0;
        isInSelection = inventorySystem.backpackVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            inventorySystem.backpackChangeVisibility();
            if (isInSelection)
            {
                isInSelection = !isInSelection;
            }
            else
            {
                isInSelection = !isInSelection;
            }
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
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                //hotkeySlots[0].setItem(inventorySlots[slotSelectedIndex].getItem());
                //hotkeySlots[0].itemStoredIndex = slotSelectedIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                //hotkeySlots[1].setItem(inventorySlots[slotSelectedIndex].getItem());
                //hotkeySlots[1].itemStoredIndex = slotSelectedIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                //hotkeySlots[2].setItem(inventorySlots[slotSelectedIndex].getItem());
                //hotkeySlots[2].itemStoredIndex = slotSelectedIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                //hotkeySlots[3].setItem(inventorySlots[slotSelectedIndex].getItem());
                //hotkeySlots[3].itemStoredIndex = slotSelectedIndex;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                inventorySystem.setSwitablePlayerItem(0);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                inventorySystem.setSwitablePlayerItem(1);
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                inventorySystem.setSwitablePlayerItem(2);
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                inventorySystem.setSwitablePlayerItem(3);
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                inventorySystem.setSwitablePlayerItem(4);
            }
        }
    }

}
