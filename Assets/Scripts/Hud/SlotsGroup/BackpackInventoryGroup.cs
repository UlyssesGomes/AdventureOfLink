public class BackpackInventoryGroup : InventoryGroupAbstract<InventorySlot>
{
    private bool isVisible;     // backpack system visibility flag

    private void Start()
    {
        isVisible = false;
        gameObject.SetActive(isVisible);
    }

    /// <summary>
    /// Call this method to switch between active and inactive and vice versa.
    /// </summary>
    public void changeVisibility()
    {
        isVisible = !isVisible;
        gameObject.SetActive(isVisible);
    }

    /// <summary>
    /// Set amount of slots visible compatible with playerInventory.inventoryCurrentSize
    /// </summary>
    /// <param name="amount"></param>
    public void setAmountSlotsVisible(int amount)
    {
        if(amount > inventorySlotList.Length)
        {
            amount = inventorySlotList.Length;
        }

        for(int u = 0; u < inventorySlotList.Length; u++)
        {
            if(u < amount)
            {
                inventorySlotList[u].gameObject.SetActive(true);
            }
            else
            {
                inventorySlotList[u].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Get total size of backpack slots
    /// </summary>
    /// <returns></returns>
    protected override int getInventoryGroupSize()
    {
        return playerInventory.TOTAL_INVENTORY_SIZE - playerInventory.hotkeyInventorySize;
    }
}
