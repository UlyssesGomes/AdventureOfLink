public class ToolbarHotkeyGroup : InventoryGroupAbstract<HotkeyInventorySlot>
{
    /// <summary>
    /// Get total size of hoketyInventorySlots.
    /// </summary>
    /// <returns></returns>
    protected override int getInventoryGroupSize()
    {
        return playerInventory.hotkeyInventorySize;
    }
}
