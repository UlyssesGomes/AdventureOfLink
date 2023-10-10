using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour, Observer<InventorySubjectEnum>
{
    [SerializeField]
    private Player player;

    private PlayerInventory playerInventory;

    [SerializeField]
    private BackpackInventory backpackInventory;

    //private InventorySlot [] inventorySlots;

    //private HotkeySlot [] hotkeySlots;

    private int slotSelectedIndex;

    private List<Observer<int>> slotListObservers;

    private bool isInSelection;

    // Start is called before the first frame update
    void Start()
    {
        slotListObservers = new List<Observer<int>>();
        slotSelectedIndex = 0;
        //inventorySlots = new InventorySlot[10];
        playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.addStoredItemsObservers(this);

        loadInventoySlotsArray();
        loadHotkeySlots();

        isInSelection = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            backpackInventory.changeVisibility();
            if (isInSelection)
            {
                notifyIndex(-1);
                isInSelection = !isInSelection;
            }
            else
            {
                notifyIndex(slotSelectedIndex);
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
                    notifyIndex(slotSelectedIndex);
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
                //playerInventory.switableItemIndex = hotkeySlots[0].itemStoredIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                //playerInventory.switableItemIndex = hotkeySlots[1].itemStoredIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                //playerInventory.switableItemIndex = hotkeySlots[2].itemStoredIndex;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                //playerInventory.switableItemIndex = hotkeySlots[3].itemStoredIndex;
            }
        }
    }

    /*
     * Update observer with subject event.
     */
    public void update(InventorySubjectEnum subjectEvent)
    {
        if (subjectEvent == InventorySubjectEnum.ADD_STORE_ITEMS_EVENT || subjectEvent == InventorySubjectEnum.REMOVE_STORE_ITEMS_EVENT )
        {
            updateSlots();
        }
    }

    /*
     * Run update in all inventory slot.
     */
    private void updateSlots()
    {

        GameItem[] items = playerInventory.getStoreItems();
        for(int u = 0; u < items.Length; u++)
        {
            //inventorySlots[u].setItem(items[u]);
        }
    }

    /*
     * Allow you to create how much slots do you want dinamically from a prefab slot.
     */
    private GameObject createSlots(int u, RectTransform rt)
    {
        Vector2 position = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        position.x += 60f * (float)u;
        //GameObject gameObject = Instantiate(inventorySlotPrefab, inventoryBag.transform);
        gameObject.name = "Slot" + (u+1);
        gameObject.GetComponent<RectTransform>().anchoredPosition = position;

        return gameObject;
    }

    /*
     * Send updated index to each InventorySlot make selection.
     */
    private void notifyIndex(int index)
    {
        //foreach(InventorySlot i in slotListObservers)
        //{
        //    i.update(index);
        //}
    }

    /*
     * Create and load all inventory slots array.
     */
    private void loadInventoySlotsArray()
    {
        //InventorySlot slot1 = inventoryBag.transform.Find("Slot" + (1)).gameObject.GetComponent<InventorySlot>();
        //inventorySlots[0] = slot1;
        //slotListObservers.Add(inventorySlots[0]);
        //RectTransform rt = slot1.GetComponent<RectTransform>();
        for (int u = 1; u < 10; u++)
        {
            //GameObject gameObject = createSlots(u, rt);
            //gameObject.transform.SetParent(inventoryBag.transform);
            //inventorySlots[u] = gameObject.GetComponent<InventorySlot>();
            //slotListObservers.Add(inventorySlots[u]);
        }
    }

    /*
     * Load all hotkeys slots from hotkeySlotObject and put 
     * in hotkeySlots array.
     */
    private void loadHotkeySlots()
    {
        //hotkeySlots = new HotkeySlot[4];

        //for(int u = 0; u < hotkeySlots.Length; u++)
        //{
        //    hotkeySlots[u] = hotkeySlotObject.transform.Find("Slot" + (u+1)).gameObject.GetComponent<HotkeySlot>();
        //}
    }
}
