using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject playerToolts;

    [SerializeField]
    private GameObject inventoryBag;

    private PlayerInventory playerInventory;

    private InventorySlot [] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = new InventorySlot[10];
        playerInventory = player.GetComponent<PlayerInventory>();

        string slotName = "Slot";
        for(int u = 0; u < 10; u++)
        {
            slots[u] = inventoryBag.transform.Find(slotName + (u + 1)).gameObject.GetComponent<InventorySlot>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int u = 0;
        for(u = 0; u < 10; u++)
        {
            slots[u].setItem(null);
        }

        u = 0;
        foreach (KeyValuePair<int, GameItem> entry in playerInventory.getStoreItems())
        {
            slots[u++].setItem(entry.Value);
        }

    }
}
