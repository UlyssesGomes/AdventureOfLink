using UnityEngine;

public class Chest : AbstractChest
{
    public override void furnitureStart()
    {
        chest = Instantiate(chest);
    }

    public override void furnitureUpdate()
    { }
}
