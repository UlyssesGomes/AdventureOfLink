using UnityEngine;

public class Chest : AbstractChest
{
    [SerializeField]
    private ChestItem chest;        // chest defined data

    public override void chestStart()
    {
        chest = Instantiate(chest);
    }

    public override ChestItem getChestData()
    {
        return chest;
    }
}
