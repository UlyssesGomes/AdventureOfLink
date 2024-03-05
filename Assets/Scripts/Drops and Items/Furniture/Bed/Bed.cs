using UnityEngine;

public class Bed : AbstractBed
{
    private AssetFactory assetfactory;      // Manager of assets available in memory.

    public override void furnitureUpdate()
    {
        executor.update();
    }
}
