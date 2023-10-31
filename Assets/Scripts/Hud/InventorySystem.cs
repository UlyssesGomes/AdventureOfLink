using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public PlayerInventory playerInventory;                 // player inventory controller

    public BackpackInventoryGroup backpackInventoryGroup;   // backpackInventoryGroup instance to control backpack ui
    public ToolbarHotkeyGroup toolbarHotkeyGroup;           // toolbarHotkeyGroup instance to control hotkeySlots ui

    public HandleSlot handleSlot;                           // when an item is in this slot, the player can use it

    public HotkeyInventorySlot [] hotkeysSlotList;          // list of hotkey slots
    public InventorySlot[] backpackSlotList;                // list of backpack slots

    private GenericSubject<int, GameItem[]> genericSubject;

    private void Awake()
    {
        backpackInventoryGroup.awakeObject();
        toolbarHotkeyGroup.awakeObject();
    }

    // Start is called before the first frame update
    void Start()
    {
        genericSubject = new GenericSubject<int, GameItem[]>();

        hotkeysSlotList = toolbarHotkeyGroup.inventorySlotList;
        backpackSlotList = backpackInventoryGroup.inventorySlotList;

        playerInventory.addStoredItemsObservers(handleSlot);

        for(int u = 0; u < hotkeysSlotList.Length; u++)
        {
            hotkeysSlotList[u].indexId = u;
            hotkeysSlotList[u].setLabel();
            playerInventory.addStoredItemsObservers(hotkeysSlotList[u]);
        }

        for (int u = 0; u < backpackSlotList.Length; u++)
        {
            backpackSlotList[u].indexId = playerInventory.hotkeyInventorySize + u;
            playerInventory.addStoredItemsObservers(backpackSlotList[u]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Swap the item in switableItem for the item in the "index" parameter in storedItems.
    /// </summary>
    /// <param name="index"></param>
    public void setSwitablePlayerItem(int index)
    {
        if (hotkeysSlotList[index].gameItem)
        {
            handleSlot.indexId = index;
            playerInventory.switableItemIndex = index;
            genericSubject.type = index;
            genericSubject.subject = playerInventory.getStoreItems();
            handleSlot.update(genericSubject);
        }
    }

    /// <summary>
    /// Open and close backpack visibility.
    /// </summary>
    public void backpackChangeVisibility()
    {
        backpackInventoryGroup.changeVisibility();
    }

    /// <summary>
    /// Tells if BackpackInventoryGroup is active or not.
    /// </summary>
    /// <returns>backpackInventoryGroup.isVisible</returns>
    public bool backpackVisibility()
    {
        return backpackInventoryGroup.getVisibility();
    }
}
