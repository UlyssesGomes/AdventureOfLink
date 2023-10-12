using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventotySystem : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public BackpackInventoryGroup backpackInventoryGroup;
    public HotkeyInventorySlot [] hotkeysList;

    // Start is called before the first frame update
    void Start()
    {
        hotkeysList = new HotkeyInventorySlot[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
