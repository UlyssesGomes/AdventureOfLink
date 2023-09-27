using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour, Observer<InventorySubjectEnum>
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject playerToolts;

    [SerializeField]
    private GameObject inventoryBag;

    private PlayerInventory playerInventory;

    private InventorySlot [] slots;

    [SerializeField]
    private GameObject inventorySlotPrefab;

    private int slotSelectedIndex;

    private List<Observer<int>> slotListObservers;

    private bool isInSelection;

    // Start is called before the first frame update
    void Start()
    {
        slotListObservers = new List<Observer<int>>();
        slotSelectedIndex = 0;
        slots = new InventorySlot[10];
        playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.addStoredItemsObservers(this);

        InventorySlot slot1 = inventoryBag.transform.Find("Slot" + (1)).gameObject.GetComponent<InventorySlot>();
        slots[0] = slot1;
        slotListObservers.Add(slots[0]);
        RectTransform rt = slot1.GetComponent<RectTransform>();
        for (int u = 1; u < 10; u++)
        {
            GameObject gameObject = createSlots(u, rt);
            gameObject.transform.SetParent(inventoryBag.transform);
            slots[u] = gameObject.GetComponent<InventorySlot>();
            slotListObservers.Add(slots[u]);
        }

        isInSelection = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            if(isInSelection)
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
                if(slotSelectedIndex < slots.Length - 1)
                {
                    slotSelectedIndex++;
                    notifyIndex(slotSelectedIndex);
                }
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if(slotSelectedIndex > 0)
                {
                    slotSelectedIndex--;
                    notifyIndex(slotSelectedIndex);
                }
            }
        }
    }

    private void loadplayerToolsInventory()
    {

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
            slots[u].setItem(items[u]);
        }
    }

    /*
     * Allow you to create how much slots do you want dinamically from a prefab slot.
     */
    private GameObject createSlots(int u, RectTransform rt)
    {
        Vector2 position = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        position.x += 60f * (float)u;
        GameObject gameObject = Instantiate(inventorySlotPrefab, inventoryBag.transform);
        gameObject.name = "Slot" + (u+1);
        gameObject.GetComponent<RectTransform>().anchoredPosition = position;

        return gameObject;
    }

    /*
     * Send updated index to each InventorySlot make selection.
     */
    private void notifyIndex(int index)
    {
        foreach(InventorySlot i in slotListObservers)
        {
            i.update(index);
        }
    }
}
