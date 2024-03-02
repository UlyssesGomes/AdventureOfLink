using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBed : Furniture
{
    [SerializeField]
    protected DrawableItem bed;             // bed defined data

    private Color defaultColor;             // chest default color to be desplayed when chest was set on the ground
    private Color buildingColor;            // color to be shown when chest is under building

    private AgentExecutor executor;         // executor to run chest bar agent

    private AssetFactory assetfactory;      // Manager of assets available in memory.

    public override void furnitureStart()
    {
        assetfactory = AssetFactory.getInstance();
        bed = Instantiate(bed);
        buildingColor = new Color(1f, 1f, 1f, 0.2549f);
        defaultColor = sprite.color;
    }

    public override DrawableItem getFurnitureData()
    {
        return bed;
    }

    protected override void buildingImpact(float value)
    {
        //executor.addAgent(new BedBarAgent(this));
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
            puff.transform.position = transform.parent.position;
        }
    }

    protected override void getAway()
    { }

    protected override void interact()
    { }
}
