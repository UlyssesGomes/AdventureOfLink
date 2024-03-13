using UnityEngine;

public class Bed : AbstractBed
{
    public override void furnitureUpdate()
    {
        executor.update();
    }
}
