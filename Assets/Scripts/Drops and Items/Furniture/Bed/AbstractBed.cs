using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBed : Furniture
{
    [SerializeField]
    protected DrawableItem bed;             // bed defined data

    protected AgentExecutor executor;       // executor to run chest bar agent

    protected AssetFactory assetfactory;    // Manager of assets available in memory.

    public override void furnitureStart()
    {
        assetfactory = AssetFactory.getInstance();
        bed = Instantiate(bed);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;

        if (buildingAmount < 100f)
        {
            filledBar.fillAmount = buildingAmount / 100f;
            sprite.color = buildingColor;
        }

        executor = new AgentExecutor();
    }

    public override DrawableItem getFurnitureData()
    {
        return bed;
    }

    protected override void buildingImpact(float value)
    {
        executor.addAgent(new FurnitureBarAgent(this));
        buildingAmount += value;

        if (buildingAmount < 100f)
        {
            filledBar.fillAmount = buildingAmount / 100f;
        }
        else
        {
            buildingAmount = 100f;
            buildingBar.SetActive(false);
            sprite.color = defaultColor;
            GameObject puff = assetfactory.instanceFxGameObjectByType((int)FxEnum.PUFF_SMOKE);
            puff.transform.position = transform.position;
        }
    }

    protected override void getAway()
    { }

    protected override void interact()
    { }
}
